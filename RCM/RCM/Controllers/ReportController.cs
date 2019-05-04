using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RCM.Helper;
using RCM.Model;
using RCM.Service;
using RCM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IProgressStageActionService _progressStageActionService;
        private readonly IReceivableService _receivableService;
        private readonly ICustomerService _customerService;
        private readonly UserManager<User> _userManager;

        public ReportController(IProgressStageActionService progressStageActionService, IReceivableService receivableService, ICustomerService customerService, UserManager<User> userManager)
        {
            _progressStageActionService = progressStageActionService;
            _receivableService = receivableService;
            _userManager = userManager;
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult GetManagerOverallReport()
        {
            var result = GetOverallReportAsync().Result;
            return Ok(result);
        }

        private async System.Threading.Tasks.Task<ManagerOverallReport> GetOverallReportAsync()
        {
            DateTime date = DateTime.Today;
            var result = new ManagerOverallReport();
            var receivableList = _receivableService.GetReceivables();
            var collectorList = await _userManager.GetUsersInRoleAsync("Collector");
            var CollectorReports = new List<CollectorReportModel>();
            foreach (var collector in collectorList)
            {
                CollectorReports.Add(GetCollectorReport(receivableList, date, collector.Id));

            }

            result.ReceivableReports = new ReceivableReportModel();

            result.ReceivableReports.MonthlyReport = GetReceivableMonthlyReports(receivableList, date);
            //result.ReceivableReports.ReceivableWillEndInMonth = GetReceivableWillEndInMonth(receivableList, date);

            result.CollectorReports = CollectorReports;

            result.RecentUpdatedTaskByCollectorReport = GetRecentUpdatedTaskReports();
            result.RecentCanceledReceivable = GetClosedOrCanceledReceivables(receivableList);

            result.NumberOfRecentClosedOrCanceledReceivables = CountRecentCanceleReceivable(receivableList, date);
            result.NumberOfRecentChangedTask = CountRecentCanceledTask(date);

            result.NumberOfReceivables = receivableList.Count();
            result.NumberOfPendingReceivables = CountType(Constant.COLLECTION_STATUS_WAIT_CODE, receivableList);
            result.NumberOfCollectingReceivables = CountType(Constant.COLLECTION_STATUS_COLLECTION_CODE, receivableList);
            result.NumberOfCanceledReceivables = CountType(Constant.COLLECTION_STATUS_CANCEL_CODE, receivableList);
            result.NumberOfRecoveredReceivables = CountType(Constant.COLLECTION_STATUS_CLOSED_CODE, receivableList);
            result.NumberOfDoneReceivables = CountType(Constant.COLLECTION_STATUS_DONE_CODE, receivableList);

            return result;
        }

        private int CountType(int type, IEnumerable<Receivable> list)
        {
            return list
               .Where(x =>
               x.IsDeleted == false
               && x.CollectionProgress.Status == type).Count();
        }

        private IEnumerable<DayUpdatedTaskReportModel> GetRecentUpdatedTaskReports()
        {
            var result = new List<DayUpdatedTaskReportModel>();
            var recentUpdatedTaskReports = _progressStageActionService.GetProgressStageActions()
                                            .Where(x =>
                                            x.IsDeleted == false
                                            && (x.Type == Constant.ACTION_VISIT_CODE || x.Type == Constant.ACTION_REPORT_CODE)
                                            && (x.Status == Constant.COLLECTION_STATUS_LATE_CODE || x.Status == Constant.COLLECTION_STATUS_CANCEL_CODE)
                                            && (x.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE)
                                            && x.UpdatedDate.HasValue
                                            ).OrderByDescending(x => x.UpdatedDate.Value);

            if (recentUpdatedTaskReports.Any())
            {
                foreach (var task in recentUpdatedTaskReports)
                {
                    result.Add(new DayUpdatedTaskReportModel()
                    {
                        Id = task.Id,
                        CollectorName = task.User != null ? GetName(task.User) : GetName(task.ProgressStage.CollectionProgress.Receivable.AssignedCollectors
                                                                                                 .First(x =>
                                                                                                    x.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE).User),
                        UpdatedTime = task.UpdatedDate.Value.ToString("HH:mm dd/MM/yyyy"),
                        ReceivableId = task.ProgressStage.CollectionProgress.ReceivableId,
                        TaskName = task.Name + " " + task.ProgressStage.CollectionProgress.Receivable.Contacts.Where(x => x.Type == Constant.CONTACT_DEBTOR_CODE).FirstOrDefault().Name,
                        Status = task.Status
                    });
                }
            }

            return result;
        }

        private IEnumerable<RecentClosedOrCanceldReceivableModel> GetClosedOrCanceledReceivables(IEnumerable<Receivable> receivables)
        {
            var result = new List<RecentClosedOrCanceldReceivableModel>();

            var recentUpdatedReceivables = receivables
                                            .Where(x =>
                                            x.IsDeleted == false
                                            && (x.CollectionProgress.Status == Constant.COLLECTION_STATUS_DONE_CODE || x.CollectionProgress.Status == Constant.COLLECTION_STATUS_CANCEL_CODE)
                                            && x.CollectionProgress.UpdatedDate.HasValue
                                            && x.IsConfirmed == false
                                            ).OrderByDescending(x => x.CollectionProgress.UpdatedDate.Value);

            if (recentUpdatedReceivables.Any())
            {
                foreach (var receivable in recentUpdatedReceivables)
                {
                    result.Add(new RecentClosedOrCanceldReceivableModel()
                    {
                        Id = receivable.Id,
                        Status = receivable.CollectionProgress.Status,
                        PartnerName = receivable.Customer.Name,
                        DebtorName = receivable.Contacts.FirstOrDefault(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).Name,
                        CollectorName = receivable.AssignedCollectors.Any() ?
                                        GetName(receivable.AssignedCollectors.FirstOrDefault(assignedCollector => assignedCollector.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE).User)
                                        : "",
                        UpdatedTime = receivable.CollectionProgress.UpdatedDate.Value.ToString("HH:mm dd/MM/yyyy")
                    });
                }
            }

            return result;
        }

        private string GetName(Model.User user)
        {
            return user.FirstName + " " + user.LastName;
        }

        private int CountRecentCanceleReceivable(IEnumerable<Receivable> receivables, DateTime date)
        {
            var recentUpdatedReceivables = receivables
                                            .Where(x =>
                                            x.IsDeleted == false
                                            && (x.CollectionProgress.Status == Constant.COLLECTION_STATUS_DONE_CODE || x.CollectionProgress.Status == Constant.COLLECTION_STATUS_CANCEL_CODE)
                                            && x.CollectionProgress.UpdatedDate.HasValue
                                            && x.CollectionProgress.UpdatedDate.Value.Date == date
                                            && x.IsConfirmed == false
                                            ).Count();
            return recentUpdatedReceivables;
        }

        private int CountRecentCanceledTask(DateTime date)
        {
            var recentUpdatedTaskReports = _progressStageActionService.GetProgressStageActions()
                                            .Where(x =>
                                            x.IsDeleted == false
                                            && (x.Type == Constant.ACTION_VISIT_CODE || x.Type == Constant.ACTION_REPORT_CODE)
                                            && (x.Status == Constant.COLLECTION_STATUS_CANCEL_CODE || x.Status == Constant.COLLECTION_STATUS_LATE_CODE)
                                            && (x.ProgressStage.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE)
                                            && x.UpdatedDate.HasValue
                                            && x.UpdatedDate.Value.Date == date
                                            ).Count();
            return recentUpdatedTaskReports;
        }

        private CollectorReportModel GetCollectorReport(IEnumerable<Receivable> receivables, DateTime date, string collectorId)
        {
            //Get all receivable assigned to collector
            var assignedHistory = receivables
               .Where(receivable =>
               receivable.AssignedCollectors
                .Any(assignedCollector =>
                assignedCollector.UserId == collectorId
                && assignedCollector.IsDeleted == false));

            //Get all receivable are in collection progress and assigned to Collector.
            var currentAssgined = assignedHistory
                 .Where(receivable =>
                receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                && receivable.AssignedCollectors.Any(assignedCollector => assignedCollector.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE));

            //Get monthly report
            var monthlyReportModels = assignedHistory
                .Where(receivable =>
                receivable.AssignedCollectors.Any(assignedCollector => assignedCollector.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE)
                && (receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_CLOSED_CODE || receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_CANCEL_CODE)
                )
                .GroupBy(
                receivable => new { receivable.CollectionProgress.UpdatedDate.Value.Month, receivable.CollectionProgress.UpdatedDate.Value.Year },
                (key, g) => new CollectorMonthlyReportModel()
                {
                    Milestone = new DateTime(g.FirstOrDefault().CollectionProgress.UpdatedDate.Value.Year, g.FirstOrDefault().CollectionProgress.UpdatedDate.Value.Month, g.FirstOrDefault().CollectionProgress.UpdatedDate.Value.Day),
                    NumberOfClosedReceivable = g.ToList().Where(receivable => receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_CLOSED_CODE).Count(),
                    NumberOfCanceledReceivable = g.ToList().Where(receivable => receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_CANCEL_CODE).Count()
                });

            var result = new CollectorReportModel();
            result.CollectorId = collectorId;
            result.CurrentAssignedReceivable = currentAssgined.Count();
            result.NumberOfAssignedReceivableInHistory = assignedHistory.Count();
            result.MonthlyReport = monthlyReportModels;

            return result;
        }

        private IEnumerable<Receivable> GetReceivableWillEndInMonth(IEnumerable<Receivable> receivables, DateTime date)
        {
            var result = receivables
                .Where(receivable =>
                receivable.ExpectationClosedDay.HasValue
                && receivable.ExpectationClosedDay.Value.Date == date.Date
                && receivable.IsConfirmed == false);
            return result;
        }

        private IEnumerable<ReceivableMonthlyReportModel> GetReceivableMonthlyReports(IEnumerable<Receivable> receivables, DateTime date)
        {
            var reportCreatedReceivable = receivables.
                Where(receivable => receivable.IsDeleted == false)
                .GroupBy(
                receivable => new { receivable.CreatedDate.Year, receivable.CreatedDate.Month },
                (key, g) => new ReceivableMonthlyReportModel()
                {
                    Milestone = new DateTime(key.Year, key.Month, 01),
                    NumberOfCreatedReceivable = g.Count(),
                });

            var reportCanceledOrClosedReceivable = receivables.
                Where(receivable =>
                receivable.IsDeleted == false
                && receivable.IsConfirmed == true
                && (receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_CLOSED_CODE || receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_CANCEL_CODE)
                && receivable.CollectionProgress.UpdatedDate.HasValue
                )
                .GroupBy(
                receivable => new { receivable.CollectionProgress.UpdatedDate.Value.Year, receivable.CollectionProgress.UpdatedDate.Value.Month },
                (key, g) => new ReceivableMonthlyReportModel()
                {
                    Milestone = new DateTime(key.Year, key.Month, 01),
                    NumberofClosedReceivable = g.Where(x => x.CollectionProgress.Status == Constant.COLLECTION_STATUS_CLOSED_CODE).Count(),
                    NumberOfCanceledReceivable = g.Where(x => x.CollectionProgress.Status == Constant.COLLECTION_STATUS_CANCEL_CODE).Count(),
                });

            var reportDoneReceivable = receivables
                .Where(receivable =>
                    receivable.IsDeleted == false
                    && receivable.ExpectationClosedDay.HasValue
                ).GroupBy(
                receivable => new { receivable.ExpectationClosedDay.Value.Year, receivable.ExpectationClosedDay.Value.Month },
                (key, g) => new ReceivableMonthlyReportModel()
                {
                    Milestone = new DateTime(key.Year, key.Month, 01),
                    NumberOfDoneReceivable = g.Count(),
                });
            var result = MappingMonthlyReport(reportCreatedReceivable, reportCanceledOrClosedReceivable, reportDoneReceivable);

            return result.OrderBy(x => x.Milestone);
        }

        private IEnumerable<ReceivableMonthlyReportModel> MappingMonthlyReport(IEnumerable<ReceivableMonthlyReportModel> created, IEnumerable<ReceivableMonthlyReportModel> closedOrCanceled, IEnumerable<ReceivableMonthlyReportModel> done)
        {

            List<ReceivableMonthlyReportModel> milestones = new List<ReceivableMonthlyReportModel>();

            created.ToList()
                .ForEach(x =>
                milestones
                    .Add(new ReceivableMonthlyReportModel()
                    {
                        Milestone = x.Milestone,
                        NumberOfCreatedReceivable = x.NumberOfCreatedReceivable
                    })
                );

            foreach (var closedOrCanceledReceivable in closedOrCanceled)
            {
                if (milestones.Any(x => x.Milestone.Month == closedOrCanceledReceivable.Milestone.Month && x.Milestone.Year == closedOrCanceledReceivable.Milestone.Year))
                {
                    milestones.First(x => x.Milestone.Month == closedOrCanceledReceivable.Milestone.Month && x.Milestone.Year == closedOrCanceledReceivable.Milestone.Year).NumberofClosedReceivable = closedOrCanceledReceivable.NumberofClosedReceivable;
                } else
                {
                    milestones.Add(new ReceivableMonthlyReportModel()
                    {
                        Milestone = closedOrCanceledReceivable.Milestone,
                        NumberofClosedReceivable = closedOrCanceledReceivable.NumberofClosedReceivable,
                        NumberOfCanceledReceivable = closedOrCanceledReceivable.NumberOfCanceledReceivable
                    });
                }
            }

            foreach (var doneReceivable in done)
            {
                if (milestones.Any(x => x.Milestone.Month == doneReceivable.Milestone.Month && x.Milestone.Year == doneReceivable.Milestone.Year))
                {
                    milestones.First(x => x.Milestone.Month == doneReceivable.Milestone.Month && x.Milestone.Year == doneReceivable.Milestone.Year).NumberOfDoneReceivable = doneReceivable.NumberOfDoneReceivable;
                } else
                {
                    milestones.Add(new ReceivableMonthlyReportModel()
                    {
                        Milestone = doneReceivable.Milestone,
                        NumberOfDoneReceivable = doneReceivable.NumberOfDoneReceivable
                    });
                }
            }

            return milestones;
        }
    }
}
