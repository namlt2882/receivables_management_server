using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
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
        private IProgressStageActionService _progressStageActionService;

        public NotifyJob(IProgressStageActionService progressStageActionService)
        {
            _progressStageActionService = progressStageActionService;
        }

        public void SendNotify()
        {
            //Example: 07:00 => 0700.
            int time = Int32.Parse(DateTime.Now.ToString(Constant.TIME_FORMAT));

            //Example 14/02/1997 => 19970214.
            long date = Int64.Parse(Utility.ConvertDatetimeToString(DateTime.Now.Date));

            var actions = _progressStageActionService.GetProgressStageActions()
                .Where(x => (
                x.IsDeleted == false
                && x.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                && x.ExcutionDay == date
                && x.StartTime < time
                && x.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                && (x.Type == Constant.ACTION_PHONECALL_CODE || x.Type == Constant.ACTION_SMS_CODE)
                ));

            //Execute action.
            //ExecuteAction(actions);
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
                        MakePhoneCallAsync(action);
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

        private async void MakePhoneCallAsync(ProgressStageAction progressStageAction)
        {
            //Get information
            var phoneNo = progressStageAction.ProgressStage.CollectionProgress
                .Receivable
                .Contacts.Where(x => x.Type == Constant.CONTACT_DEBTOR_CODE).FirstOrDefault().Phone;
            var messageContent = progressStageAction.ProgressMessageForm.Content;

            //Make phone call
            var task = await Utility.MakePhoneCallAsync(phoneNo, messageContent);

            //Check phone call
            var result = JsonConvert.DeserializeObject<StringeeResponseModel>(task);
            if (result.r == 0)
            {
                _progressStageActionService.MarkAsDone(progressStageAction);
            }
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

            string response = Utility.SendSMS(phoneNo, messageContent);

            var result = JsonConvert.DeserializeObject<SpeedSMSResponseModel>(response);

            if (result.status == 0)
            {
                _progressStageActionService.MarkAsDone(progressStageAction);
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

    class SpeedSMSResponseModel
    {
        public string type { get; set; }
        public int tranId { get; set; }
        public string phone { get; set; }
        public int status { get; set; }
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
