using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public NotifyJob(IProgressStageActionService progressStageActionService, IHubContext<CenterHub> hubContext, IHubUserConnectionService hubService, IFirebaseTokenService firebaseTokenService, IReceivableService receivableService, INotificationService notificationService, UserManager<User> userManager)
        {
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
            var actionsToExecute = _progressStageActionService.GetProgressStageActions()
                .Where(x => (
                x.IsDeleted == false
                && x.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                && x.ExcutionDay == date
                && x.StartTime < time
                && x.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                && (x.Type == Constant.ACTION_PHONECALL_CODE || x.Type == Constant.ACTION_SMS_CODE)
                ));

            //Execute action.
            if (actionsToExecute.Any())
            {
                ExecuteAction(actionsToExecute);
            }
            #endregion

            #region Late Action
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
                    _progressStageActionService.SaveProgressStageAction();
                }
            }
            #endregion

        }

        private async void SendPendingReceivableNotify()
        {
            #region Pending Receivable
            var lateReceivableList = _receivableService.GetReceivables(
                r => !r.IsDeleted
                && r.CollectionProgress.Status == Constant.COLLECTION_STATUS_WAIT_CODE
                && r.CreatedDate.AddDays(5) <= DateTime.Now
            );
            #region Create New Receivable Notification
            var user = await _userManager.FindByNameAsync("manager");
            //Create New Receivable Notification
            Notification notification = new Notification()
            {
                Title = Constant.NOTIFICATION_TYPE_CLOSE_RECEIVABLE,
                Type = Constant.NOTIFICATION_TYPE_CLOSE_RECEIVABLE_CODE,
                Body = $"{lateReceivableList.Count()} receivable(s) already pending more than 5 days!",
                UserId = user.Id,
                NData = JsonConvert.SerializeObject(lateReceivableList.Select(r => r.Id)),
                IsSeen = false,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };
            _notificationService.CreateNotification(notification);
            _notificationService.SaveNotification();
            #endregion
            //Send
            await NotificationUtility.NotificationUtility.SendNotification(notification, _hubService, _hubContext, _firebaseTokenService);
            #endregion
        }

        private void ExecuteAction(IEnumerable<ProgressStageAction> actions)
        {
            foreach (var action in actions)
            {
                switch (action.Type)
                {
                    case Constant.ACTION_VISIT_CODE:
                        NotifyVisit();
                        break;
                    case Constant.ACTION_PHONECALL_CODE:
                        MakePhoneCall(action);
                        break;
                    case Constant.ACTION_REPORT_CODE:
                        NotifyReport();
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

        private async void MakePhoneCall(ProgressStageAction progressStageAction)
        {
            //Get information
            var phoneNo = progressStageAction.ProgressStage.CollectionProgress
                .Receivable
                .Contacts.Where(x => x.Type == Constant.CONTACT_DEBTOR_CODE).FirstOrDefault().Phone;
            var messageContent = progressStageAction.ProgressMessageForm.Content;

            ////Make phone call
            //var stringeeMsg = await Utility.MakePhoneCallAsync(phoneNo, messageContent);
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
            //var messageContent = progressStageAction.ProgressMessageForm.Content;

            //string response = Utility.SendSMS(phoneNo, messageContent);
            _progressStageActionService.MarkAsDone(progressStageAction);
            _progressStageActionService.SaveProgressStageAction();
            //if (response.Contains("success"))
            //{
                //_progressStageActionService.MarkAsDone(progressStageAction);
            //}
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
