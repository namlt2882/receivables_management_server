using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RCM.Helper;
using RCM.Model;
using RCM.Service;

namespace RCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IReceivableService _receivableService;
        private readonly IAssignedCollectorService _assignedCollectorService;
        private readonly ICustomerService _customerService;
        private readonly IProfileService _profileService;
        private readonly UserManager<User> _userManager;

        public DashboardController(IReceivableService receivableService, IAssignedCollectorService assignedCollectorService, ICustomerService customerService, IProfileService profileService, UserManager<User> userManager)
        {
            _receivableService = receivableService;
            _assignedCollectorService = assignedCollectorService;
            _customerService = customerService;
            _profileService = profileService;
            _userManager = userManager;
        }

        #region Manager

        [HttpGet("GetTotalReceivable/{startDay}/{endDay}")]
        public IActionResult GetTotalReceivable(int startDay, int endDay)
        {
            var receivables = _receivableService.GetReceivables(r => r.ClosedDay.HasValue && r.ClosedDay.Value >= startDay && r.ClosedDay.Value <= endDay).ToList();
            return Ok(GetTotalMoneyByMonth(receivables, GetMonth(receivables)));
        }
        [HttpGet("GetPendingReceivable")]
        public IActionResult GetPendingReceivable()
        {
            return Ok(_receivableService.GetReceivables(r => r.CollectionProgress.Status == Constant.COLLECTION_STATUS_WAIT_CODE).Select(x => x.Id));
        }

        [HttpGet("GetReportByProfileId/{profileId}")]
        public IActionResult GetReportByProfileId(int profileId)
        {
            var report = new ReportByProfile();
            var profile = _profileService.GetProfile(profileId);
            report.ProfileId = profile.Id;
            report.ProfileName = profile.Name;
            var receivables = _receivableService.GetReceivables(r => r.CollectionProgress.ProfileId == profileId);
            if (receivables.Any())
            {
                receivables.ToList().ForEach(_ =>
                {
                    switch (_.CollectionProgress.Status)
                    {
                        case Constant.COLLECTION_STATUS_CANCEL_CODE: report.Cancel++; break;
                        case Constant.COLLECTION_STATUS_COLLECTION_CODE: report.Collecting++; break;
                        case Constant.COLLECTION_STATUS_DONE_CODE: report.Done++; break;
                        case Constant.COLLECTION_STATUS_CLOSED_CODE: report.Close++; break;
                    }
                });
            }

            return Ok(report);
        }

        [HttpGet("GetReportReceivableStatusByCollector/{collectorId}")]
        public async Task<IActionResult> GetReportCollectorsAsync(string collectorId)
        {
            var report = new CollectorStatus();
            var user = await _userManager.FindByIdAsync(collectorId);
            report.CollectorId = user.Id;
            report.CollectorName = $"{user.FirstName} {user.LastName}";
            var receivableList = _assignedCollectorService.GetAssignedCollectors(_ => _.UserId == user.Id && _.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE).Select(ac => ac.ReceivableId);
            var receivables = _receivableService.GetReceivables(r => receivableList.Contains(r.Id));
            if (receivables.Any())
            {
                receivables.ToList().ForEach(_ =>
                {
                    switch (_.CollectionProgress.Status)
                    {
                        case Constant.COLLECTION_STATUS_CANCEL_CODE: report.Cancel++; break;
                        case Constant.COLLECTION_STATUS_COLLECTION_CODE: report.Collecting++; break;
                        case Constant.COLLECTION_STATUS_DONE_CODE: report.Done++; break;
                        case Constant.COLLECTION_STATUS_CLOSED_CODE: report.Close++; break;
                    }
                });
            }
            return Ok(report);
        }

        private List<DateTime> GetMonth(List<Receivable> receivables)
        {
            List<DateTime> result = new List<DateTime>();
            receivables.ForEach(r =>
            {
                var date = Utility.ConvertIntToDatetime((int)r.ClosedDay);
                if (!result.Exists(_ => _.Month == date.Month))
                {
                    result.Add(date);
                }
            });
            return result;
        }
        private List<TotalMoneyByMonth> GetTotalMoneyByMonth(List<Receivable> receivables, List<DateTime> dates)
        {
            List<TotalMoneyByMonth> result = new List<TotalMoneyByMonth>();
            foreach (var date in dates)
            {
                var total = new TotalMoneyByMonth();
                total.Time = date;

                foreach (var receivable in receivables)
                {
                    var time = Utility.ConvertIntToDatetime((int)receivable.ClosedDay);

                    if (time.Month == date.Month)
                    {
                        total.Money += receivable.DebtAmount - receivable.PrepaidAmount;
                    }
                }
                result.Add(total);
            }
            return result;
        }

        #endregion



        #region Collector

        [Authorize]
        [HttpGet("GetReportReceivableStatusByCollector")]
        public async Task<IActionResult> GetReportCollectorAsync()
        {
            var report = new CollectorStatus();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var receivableList = _assignedCollectorService.GetAssignedCollectors(_ => _.UserId == user.Id && _.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE).Select(ac => ac.ReceivableId);
            var receivables = _receivableService.GetReceivables(r => receivableList.Contains(r.Id));
            if (receivables.Any())
            {
                receivables.ToList().ForEach(_ =>
                {
                    switch (_.CollectionProgress.Status)
                    {
                        case Constant.COLLECTION_STATUS_CANCEL_CODE: report.Cancel++; break;
                        case Constant.COLLECTION_STATUS_COLLECTION_CODE: report.Collecting++; break;
                        case Constant.COLLECTION_STATUS_DONE_CODE: report.Done++; break;
                        case Constant.COLLECTION_STATUS_CLOSED_CODE: report.Close++; break;
                    }
                });
            }
            return Ok(report);
        }

        #endregion
    }
    public class TotalMoneyByMonth
    {
        public DateTime Time { get; set; }
        public long Money { get; set; } = 0;
    }

    public class ReportByProfile
    {
        public int ProfileId { get; set; }
        public string ProfileName { get; set; }
        public int Collecting { get; set; }
        public int Cancel { get; set; }
        public int Close { get; set; }
        public int Done { get; set; }
    }

    public class CollectorStatus
    {
        public string CollectorId { get; set; }
        public string CollectorName { get; set; }
        public int Collecting { get; set; }
        public int Cancel { get; set; }
        public int Close { get; set; }
        public int Done { get; set; }
    }

}
