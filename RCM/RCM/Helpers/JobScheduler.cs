using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using RCM.CenterHubs;
using RCM.Helper;
using RCM.Model;
using RCM.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCM.Helpers
{
    public class NotifyJobFactory : IJobFactory, IDisposable
    {
        private readonly IServiceScope serviceScope;

        public NotifyJobFactory(IServiceProvider serviceProvider)
        {
            serviceScope = serviceProvider.CreateScope();
        }

        public void Dispose()
        {
            serviceScope.Dispose();
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle.JobDetail;

            var job = (IJob)serviceScope.ServiceProvider.GetService(jobDetail.JobType);
            return job;
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }

    public class NotifyJob : IJob
    {
        private readonly IProgressStageActionService _progressStageActionService;
        private readonly IHubContext<CenterHub> _hubContext;
        private readonly IHubUserConnectionService _hubService;
        private readonly IFirebaseTokenService _firebaseTokenService;
        private readonly IReceivableService _receivableService;
        private readonly INotificationService _notificationService;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public NotifyJob(IConfiguration configuration, IProgressStageActionService progressStageActionService, IHubContext<CenterHub> hubContext, IHubUserConnectionService hubService, IFirebaseTokenService firebaseTokenService, IReceivableService receivableService, INotificationService notificationService, UserManager<User> userManager)
        {
            _configuration = configuration;
            _progressStageActionService = progressStageActionService;
            _hubContext = hubContext;
            _hubService = hubService;
            _firebaseTokenService = firebaseTokenService;
            _receivableService = receivableService;
            _notificationService = notificationService;
            _userManager = userManager;
        }

        public void SendNotify()
        {
            //Example: 07:00 => 0700.
            int time = Int32.Parse(DateTime.Now.ToString(Constant.TIME_FORMAT));

            //Example 14/02/1997 => 19970214.
            long date = Int64.Parse(Utility.ConvertDatetimeToString(DateTime.Now.Date));

            #region Phone/SMS
            Task notification = Task.Factory.StartNew(async () => await SendNotificationToDebtor(date, time));
            #endregion

            #region Close Receivable
            Task closeRecivable = notification.ContinueWith((task) => CloseReceivable(date, time));
            #endregion

            #region Late Action
            Task markAction = closeRecivable.ContinueWith((task) => MarkActionAsLate(date, time));
            #endregion



        }

        private async Task SendNotificationToDebtor(long date, int time)
        {
            var actionsToExecute = _progressStageActionService.GetProgressStageActions()
                .Where(x => (
                x.IsDeleted == false
                && x.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                && x.ExcutionDay == date
                && x.StartTime < time
                && CheckNote(x.Note)
                && x.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                && (x.Type == Constant.ACTION_PHONECALL_CODE || x.Type == Constant.ACTION_SMS_CODE)
                ));

            //Execute action.

            if (actionsToExecute.Any())
            {
                await ExecuteAction(actionsToExecute);
            }
        }
        #region process fail sms/phone call
        private bool CheckNote(string note)
        {
            try
            {
                var result = int.Parse(note);
                if (result < 3) return true;
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        private string MakeFailNote(string note)
        {
            if (string.IsNullOrEmpty(note)) return "1";
            var result = int.Parse(note);
            result = result + 1;
            return result.ToString();
        }
        #endregion

        private void MarkActionAsLate(long date, int time)
        {
            var actionsToMarkAsLate = _progressStageActionService.GetProgressStageActions()
                .Where(x => (
                x.IsDeleted == false
                && x.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                && x.ExcutionDay < date
                && x.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                ));

            if (actionsToMarkAsLate.Any())
            {
                foreach (var action in actionsToMarkAsLate)
                {
                    action.Status = Constant.COLLECTION_STATUS_LATE_CODE;
                    _progressStageActionService.EditProgressStageAction(action);
                }
                _progressStageActionService.SaveProgressStageAction();
            }
        }

        private void CloseReceivable(long date, int time)
        {
            var receivableToClose = _receivableService.GetReceivables().
               Where(x =>
               x.IsDeleted == false
               && x.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
               && x.ExpectationClosedDay.HasValue
               && x.ExpectationClosedDay.Value.Date < DateTime.Now.Date);

            if (receivableToClose.Any())
            {
                foreach (var receivable in receivableToClose)
                {
                    receivable.CollectionProgress.Status = Constant.COLLECTION_STATUS_DONE_CODE;
                    _receivableService.EditReceivable(receivable);
                }
                _receivableService.SaveReceivable();
                SendDoneReceivableNotification(receivableToClose);
            }
        }

        private async void SendDoneReceivableNotification(IEnumerable<Receivable> receivables)
        {

            #region Create New Receivable Notification

            //Create Done Receivable Notifications
            List<Notification> notifications = new List<Notification>();
            foreach (var receivable in receivables)
            {
                Notification notification = new Notification()
                {
                    Title = Constant.NOTIFICATION_TYPE_DONE_RECEIVABLE,
                    Type = Constant.NOTIFICATION_TYPE_DONE_RECEIVABLE_CODE,
                    Body = $"Collecting progress of {receivable.Contacts.First().Name} from {receivable.Customer.Name} is done!",
                    UserId = receivable.AssignedCollectors.First(ac => ac.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE && !ac.IsDeleted).UserId,
                    NData = JsonConvert.SerializeObject(receivable.Id),
                    IsSeen = false,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                };
                var result = _notificationService.CreateNotification(notification);
                _notificationService.SaveNotification();
                notifications.Add(result);
            }

            #endregion
            //Send
            await NotificationUtility.NotificationUtility.SendNotification(notifications, _hubService, _hubContext, _firebaseTokenService);
        }

        private async void SendFailAutoActionNotification(ProgressStageAction progressStageAction)
        {

            #region Create New Receivable Notification

            //Create Done Receivable Notifications
            var receivable = progressStageAction.ProgressStage.CollectionProgress.Receivable;
            Notification notification = new Notification()
            {
                Title = Constant.NOTIFICATION_TYPE_FAIL_AUTO_ACTION,
                Type = Constant.NOTIFICATION_TYPE_FAIL_AUTO_ACTION_CODE,
                Body = $"Action {Constant.GetActionType(progressStageAction.Type)} could not deliver to {receivable.Contacts.First().Name} from {receivable.Customer.Name}!",
                UserId = receivable.AssignedCollectors.First(ac => ac.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE && !ac.IsDeleted).UserId,
                NData = JsonConvert.SerializeObject(receivable.Id),
                IsSeen = false,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };
            var result = _notificationService.CreateNotification(notification);
            _notificationService.SaveNotification();

            #endregion
            //Send
            await NotificationUtility.NotificationUtility.SendNotification(notification, _hubService, _hubContext, _firebaseTokenService);
        }



        private async Task ExecuteAction(IEnumerable<ProgressStageAction> actions)
        {
            foreach (var action in actions)
            {
                switch (action.Type)
                {
                    case Constant.ACTION_PHONECALL_CODE:
                        await MakePhoneCall(action);
                        break;
                    case Constant.ACTION_SMS_CODE:
                        SendSMS(action);
                        break;
                }
            }
        }

        private void NotifyVisit()
        {

        }

        private async Task MakePhoneCall(ProgressStageAction progressStageAction)
        {
            //Get information
            var phoneNo = progressStageAction.ProgressStage.CollectionProgress
                .Receivable
                .Contacts.Where(x => x.Type == Constant.CONTACT_DEBTOR_CODE).FirstOrDefault().Phone;
            var messageContent = progressStageAction.ProgressMessageForm.Content;

            if (phoneNo != Constant.DEFAULT_PHONE_NUMBER)
            {
                System.Diagnostics.Debug.WriteLine("Tin nhan duoc gui di");
                ////Make phone call
                var stringeeMsg = await Utility.MakePhoneCallAsync(phoneNo, messageContent);

            }

            #region get CallId

            //JObject call = JObject.Parse(stringeeMsg);
            //var callId = call.SelectToken("call_id").ToString();

            #endregion
            //progressStageAction.NData = callId;

            //Check phone call Set 5 phut sau se check?

            //if (await Utility.CheckCall(callId, phoneNo))
            //{
            _progressStageActionService.MarkAsDone(progressStageAction);
            _progressStageActionService.SaveProgressStageAction();
            //}
        }

        private void NotifyReport()
        {

        }

        private void SendSMS(ProgressStageAction progressStageAction)
        {
            var phoneNo = progressStageAction.ProgressStage.CollectionProgress
                .Receivable
                .Contacts.Where(x => x.Type == Constant.CONTACT_DEBTOR_CODE).FirstOrDefault().Phone;
            var messageContent = progressStageAction.ProgressMessageForm.Content;



            if (phoneNo != Constant.DEFAULT_PHONE_NUMBER)
            {
                System.Diagnostics.Debug.WriteLine("Tin nhan duoc gui di");
                ////Make phone call
                string response = Utility.SendSMS(phoneNo, messageContent);
                //string response = Utility.SendSMS(phoneNo, messageContent);
                var result = SpeedSMS.SendSms.FromJson(response);
                if (result.Status.ToLower() != "success")
                {
                    //var error = "";
                    //switch (result.Code)
                    //{
                    //    case SmsErrorCode.ACCOUNT_LOCKED_CODE: error = SmsErrorCode.ACCOUNT_LOCKED; break;
                    //    case SmsErrorCode.ACCOUNT_NOT_ALLOW_CODE: error = SmsErrorCode.ACCOUNT_NOT_ALLOW; break;
                    //    case SmsErrorCode.ACCOUNT_NOT_ENOUGH_BALANCE_CODE: error = SmsErrorCode.ACCOUNT_NOT_ENOUGH_BALANCE; break;
                    //    case SmsErrorCode.CONTENT_NOT_SUPPORT_CODE: error = SmsErrorCode.CONTENT_NOT_SUPPORT; break;
                    //    case SmsErrorCode.CONTENT_TOO_LONG_CODE: error = SmsErrorCode.CONTENT_TOO_LONG_CODE; break;
                    //    case SmsErrorCode.INVALID_PHONE_CODE: error = SmsErrorCode.INVALID_PHONE; break;
                    //    case SmsErrorCode.IP_LOCKED_CODE: error = SmsErrorCode.IP_LOCKED; break;
                    //    case SmsErrorCode.PROVIDER_ERROR_CODE: error = SmsErrorCode.PROVIDER_ERROR; break;
                    //}
                    progressStageAction.Note = MakeFailNote(progressStageAction.Note);
                    if (int.Parse(progressStageAction.Note) == 3)
                    {
                        progressStageAction.Status = Constant.COLLECTION_STATUS_CANCEL_CODE;
                    }
                    _progressStageActionService.EditProgressStageAction(progressStageAction);
                    _progressStageActionService.SaveProgressStageAction();
                    //Notify to user
                    if (int.Parse(progressStageAction.Note) == 3)
                    {
                        SendFailAutoActionNotification(progressStageAction);
                    }
                    return;
                }
                _progressStageActionService.MarkAsDone(progressStageAction);
                _progressStageActionService.SaveProgressStageAction();
            }


        }

        public Task Execute(IJobExecutionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Task task = new Task(() => SendNotify());
            task.Start();
            return task;
        }
    }

    class StringeeResponseModel
    {
        public int r { get; set; }
        public string message { get; set; }
    }

    public class Quartz
    {
        private IScheduler _scheduler;
        public static IScheduler Scheduler { get { return Instance._scheduler; } }
        private static Quartz _instance = null;

        public static Quartz Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Quartz();
                }
                return _instance;
            }
        }

        private Quartz()
        {
            Init();
        }

        private async void Init()
        {
            _scheduler = await new StdSchedulerFactory().GetScheduler();
        }

        public IScheduler UseJobFactory(IJobFactory jobFactory)
        {
            Scheduler.JobFactory = jobFactory;
            return Scheduler;
        }

        public async void AddJob<T>(string name, string group)
            where T : IJob
        {
            IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(name, group)
                .Build();

            ITrigger jobTrigger = TriggerBuilder.Create()
                .WithIdentity(name + "Trigger", group)
                .StartNow()
                ////This line is for testing purpose
                //.WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever())
                //Main time line is here
                .WithCronSchedule(Constant.SCHEDULER_CRON, x => x.InTimeZone(TimeZoneInfo.Local))
                .Build();

            await Scheduler.ScheduleJob(job, jobTrigger);
        }

        public static async void Start()
        {
            await Scheduler.Start();
        }
    }

    public static class UseQuartzExtension
    {
        public static void UseQuartz(this IApplicationBuilder app, Action<Quartz> configuration)
        {
            var jobFactory = new NotifyJobFactory(app.ApplicationServices);
            Quartz.Instance.UseJobFactory(jobFactory);

            configuration.Invoke(Quartz.Instance);
            Quartz.Start();
        }
    }
}
