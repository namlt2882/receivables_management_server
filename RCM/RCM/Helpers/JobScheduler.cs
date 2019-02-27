using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using RCM.Helper;
using RCM.Model;
using RCM.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCM.Helpers
{
    public class JobScheduler
    {
        public JobScheduler()
        {

        }

        public async void Start()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = await schedulerFactory.GetScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<Notify>().Build();


            ITrigger triggerForNotifyJob = TriggerBuilder.Create()

                .WithIdentity("Notify ", "Group")
                .StartNow()
                .WithPriority(1)
                //This line is for testing purpose
                //.WithSimpleSchedule(x => x.WithIntervalInSeconds(30))
                //Main time line is here
                .WithCronSchedule(Constant.SCHEDULER_CRON, x => x.InTimeZone(TimeZoneInfo.Local))
                .Build();

            await scheduler.ScheduleJob(job, triggerForNotifyJob);

        }

    }
    public class Notify : IJob
    {
        private IProgressStageActionService _progressStageActionService;

        public void SendNotify()
        {

            _progressStageActionService = ServiceLocator.Instance.GetService<IProgressStageActionService>();

            //Example: 07:00 => 0700.
            int time = Int32.Parse(DateTime.Now.ToString("HHmm"));

            //Example 14/02/1997 => 19970214.
            long date = Int64.Parse(Utility.ConvertDatetimeToString(DateTime.Now.Date));

            var actions = _progressStageActionService.GetProgressStageActions()
                .Where(x => (
                x.IsDeleted == false
                && x.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                && x.ExcutionDay <= date
                && x.StartTime < time));

            //Execute action.
            ExecuteAction(actions);
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
}
