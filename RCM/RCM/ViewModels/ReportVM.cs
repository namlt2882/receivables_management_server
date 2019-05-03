using RCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCM.ViewModels
{
    public class ReportVM
    {
        public string CreatedTime { get; set; }
    }

    public class ManagerReportVM : ReportVM
    {
        public ManagerOverallReport OverallReport { get; set; }
    }

    public class ManagerOverallReport
    {
        public int NumberOfReceivables { get; set; }
        public int NumberOfRecoveredReceivables { get; set; }
        public int NumberOfCanceledReceivables { get; set; }
        public int NumberOfPendingReceivables { get; set; }
        public int NumberOfCollectingReceivables { get; set; }
        public int NumberOfDoneReceivables { get; set; }

        public int NumberOfRecentClosedOrCanceledReceivables { get; set; }
        public int NumberOfRecentChangedTask { get; set; }

        public IEnumerable<DayUpdatedTaskReportModel> RecentUpdatedTaskByCollectorReport { get; set; }
        public IEnumerable<RecentClosedOrCanceldReceivableModel> RecentCanceledReceivable { get; set; }
        public IEnumerable<CollectorReportModel> CollectorReports { get; set; }
        public ReceivableReportModel ReceivableReports { get; set; }

    }

    public class DayUpdatedTaskReportModel
    {
        public int Id { get; set; }
        public int ReceivableId { get; set; }
        public string CollectorName { get; set; }
        public string TaskName { get; set; }
        public string UpdatedTime { get; set; }
        public int Status { get; set; }
    }

    public class RecentClosedOrCanceldReceivableModel
    {
        public int Id { get; set; }
        public string CollectorName { get; set; }
        public string PartnerName { get; set; }
        public string DebtorName { get; set; }
        public int Status { get; set; }
        public string UpdatedTime { get; set; }
    }

    public class MonthlyReportModel
    {
        public DateTime Milestone { get; set; }
    }

    public class CollectorReportModel
    {
        public string CollectorId { get; set; }
        public int CurrentAssignedReceivable { get; set; }
        public int NumberOfAssignedReceivableInHistory { get; set; }
        public IEnumerable<CollectorMonthlyReportModel> MonthlyReport { get; set; }
    }

    public class CollectorMonthlyReportModel: MonthlyReportModel
    {
        public int NumberOfClosedReceivable { get; set; }
        public int NumberOfCanceledReceivable { get; set; }
    }

    public class ReceivableReportModel
    {
        public IEnumerable<Receivable> ReceivableWillEndInMonth { get; set; }
        public IEnumerable<ReceivableMonthlyReportModel> MonthlyReport { get; set; }
    }

    public class ReceivableMonthlyReportModel: MonthlyReportModel
    {
        public int NumberOfCreatedReceivable { get; set; }
        public int NumberOfCanceledReceivable { get; set; }
        public int NumberofClosedReceivable { get; set; }
        public int NumberOfDoneReceivable { get; set; }
    }
}
