using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using RCM.CenterHubs;
using RCM.Helper;
using RCM.Model;
using RCM.Service;
using RCM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ReceivableController : ControllerBase
    {

        private readonly IHubContext<CenterHub> _hubContext;
        private readonly IHubUserConnectionService _hubService;
        private readonly IFirebaseTokenService _firebaseTokenService;
        private readonly IReceivableService _receivableService;
        private readonly INotificationService _notificationService;
        private readonly UserManager<User> _userManager;
        private readonly IProfileService _profileService;
        private readonly IAssignedCollectorService _assignedCollectorService;
        private readonly IProfileMessageFormService _profileMessageFormService;
        private readonly ICustomerService _customerService;

        public ReceivableController(IHubContext<CenterHub> hubContext, IHubUserConnectionService hubService, IFirebaseTokenService firebaseTokenService, IReceivableService receivableService, IProfileService profileService, INotificationService notificationService, IAssignedCollectorService assignedCollectorService, IProfileMessageFormService profileMessageFormService, UserManager<User> userManager, ICustomerService customerService)
        {
            _hubContext = hubContext;
            _hubService = hubService;
            _firebaseTokenService = firebaseTokenService;
            _receivableService = receivableService;
            _profileService = profileService;
            _notificationService = notificationService;
            _assignedCollectorService = assignedCollectorService;
            _profileMessageFormService = profileMessageFormService;
            _userManager = userManager;
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //var result = new List<ReceivableLM>();
            //_receivableService.GetReceivables().ToList().ForEach(x =>
            //{
            //    var receivable = new ReceivableLM();
            //    receivable.Id = x.Id;
            //    receivable.ClosedDay = x.ClosedDay;
            //    receivable.CustomerId = x.CustomerId;
            //    receivable.DebtAmount = x.DebtAmount;
            //    receivable.LocationId = x.LocationId;
            //    receivable.PayableDay = x.PayableDay != null ? x.PayableDay : null;
            //    receivable.PrepaidAmount = x.PrepaidAmount;
            //    receivable.CollectionProgressStatus = x.CollectionProgress.Status;
            //    receivable.CollectionProgressId = x.CollectionProgress.Id;
            //    receivable.AssignedCollectorId = x.AssignedCollectors.FirstOrDefault(assignedCollector => assignedCollector.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE) != null ? x.AssignedCollectors.FirstOrDefault(assignedCollector => assignedCollector.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE).UserId : "";
            //    receivable.CustomerName = x.Customer.Name;
            //    receivable.DebtorName = x.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).SingleOrDefault().Name;
            //    receivable.DebtorId = x.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).SingleOrDefault().Id;
            //    receivable.ProgressPercent = GetPercent(x);
            //    receivable.HaveLateAction = HaveLateActions(x);
            //    receivable.IsConfirmed = x.IsConfirmed;
            //    receivable.Stage = GetStageName(x);
            //    result.Add(receivable);
            //});
            var result = _receivableService.GetReceivables().Select(x => new ReceivableLM()
            {
                Id = x.Id,
                ClosedDay = x.ClosedDay,
                CustomerId = x.CustomerId,
                DebtAmount = x.DebtAmount,
                LocationId = x.LocationId,
                PayableDay = x.PayableDay != null ? x.PayableDay : null,
                PrepaidAmount = x.PrepaidAmount,
                CollectionProgressStatus = x.CollectionProgress.Status,
                CollectionProgressId = x.CollectionProgress.Id,
                AssignedCollectorId = x.AssignedCollectors.FirstOrDefault(assignedCollector => assignedCollector.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE) != null ? x.AssignedCollectors.FirstOrDefault(assignedCollector => assignedCollector.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE).UserId : "",
                CustomerName = x.Customer.Name,
                DebtorName = x.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).FirstOrDefault().Name,
                DebtorId = x.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).FirstOrDefault().Id,
                ProgressPercent = GetProgressReached(x),
                HaveLateAction = HaveLateActions(x),
                IsConfirmed = x.IsConfirmed,
                Stage = GetStageName(x),
                ProfileId = x.CollectionProgress.ProfileId
            });
            return Ok(result);
        }
        /// <summary>
        /// Get receivable by user token
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetByToken")]
        public async Task<IActionResult> GetByToken()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = _receivableService.GetReceivables(r => r.AssignedCollectors.FirstOrDefault(a => a.UserId == user.Id && !a.IsDeleted && a.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE) != null).Select(x => new ReceivableLM()
            {
                Id = x.Id,
                ClosedDay = x.ClosedDay,
                CustomerId = x.CustomerId,
                DebtAmount = x.DebtAmount,
                LocationId = x.LocationId,
                PayableDay = x.PayableDay != null ? x.PayableDay : null,
                PrepaidAmount = x.PrepaidAmount,
                CollectionProgressStatus = x.CollectionProgress.Status,
                CollectionProgressId = x.CollectionProgress.Id,
                AssignedCollectorId = x.AssignedCollectors.FirstOrDefault(assignedCollector => assignedCollector.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE).UserId,
                CustomerName = x.Customer.Name,
                DebtorName = x.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).FirstOrDefault().Name,
                DebtorId = x.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).FirstOrDefault().Id,
                ProgressPercent = GetProgressReached(x),
                HaveLateAction = HaveLateActions(x),
                IsConfirmed = x.IsConfirmed,
                Stage = GetStageName(x),
            });
            return Ok(result);
        }

        private string GetStageActionName(Receivable receivable)
        {
            //Receivable not start yet
            if (receivable.CollectionProgress.Receivable.PayableDay > Utility.ConvertDatimeToInt(DateTime.Now) || receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_WAIT_CODE)
            {
                var action = receivable.CollectionProgress.ProgressStages.First().ProgressStageAction.First();
                return action.Name;
            }
            //
            var stage = receivable.CollectionProgress.ProgressStages.Where(cp => cp.ProgressStageAction.Where(psa => psa.ExcutionDay <= Utility.ConvertDatimeToInt(DateTime.Now)).FirstOrDefault() != null).Last();
            if (stage != null)
            {
                var action = stage.ProgressStageAction.OrderByDescending(_ => _.Id).FirstOrDefault(psa => psa.ExcutionDay <= Utility.ConvertDatimeToInt(DateTime.Now));
                if (action != null)
                    return action.Name;
            }
            //Receivable already close
            return null;
        }

        private string GetStageName(Receivable receivable)
        {

            var result = new ProgressStageMobileVM();
            if (receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_WAIT_CODE)
            {
                return "";
            }
            if (receivable.CollectionProgress.Receivable.PayableDay > Utility.ConvertDatimeToInt(DateTime.Now))
            {
                var progressStage = receivable.CollectionProgress.ProgressStages.FirstOrDefault();
                return progressStage.Name;
            }
            //Receivable is closed=> get Last action
            if (receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_CANCEL_CODE || receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_CLOSED_CODE)
            {

                var stages = receivable.CollectionProgress.ProgressStages.ToList();
                result.Actions = stages.Select(action => new ProgressStageActionMobileDM
                {
                    Name = action.Name
                });
                var psaList = new List<ProgressStageAction>();
                foreach (var item in stages)
                {
                    foreach (var psa in item.ProgressStageAction.ToList())
                    {
                        if (psa.ExcutionDay <= receivable.ClosedDay)
                        {
                            psaList.Add(psa);
                        }
                    }
                }
                if (psaList.Any())
                {
                    for (var i = 0; i < stages.Count; i++)
                    {
                        foreach (var psa in stages[i].ProgressStageAction.ToList())
                        {
                            if (psa.Id == psaList.LastOrDefault().Id)
                            {
                                return stages[i].Name;
                            }
                        }
                    }
                }
                //Don't excute any action
                return stages[0].Name;

            }
            //Receivable is in collecting
            else
            {
                var stages = receivable.CollectionProgress.ProgressStages.ToList();
                result.Actions = stages.Select(action => new ProgressStageActionMobileDM
                {
                    Name = action.Name
                });
                var psaList = new List<ProgressStageAction>();
                foreach (var item in stages)
                {
                    foreach (var psa in item.ProgressStageAction.ToList())
                    {
                        if (psa.ExcutionDay >= Utility.ConvertDatimeToInt(DateTime.Now))
                        {
                            psaList.Add(psa);
                        }
                    }
                }
                if (psaList.Any())
                {
                    var psa = psaList.FirstOrDefault();
                    return psa.ProgressStage.Name;
                }
                //Don't have any remaining action
                return stages.LastOrDefault().Name;
            }
        }


        //[Authorize]
        //[HttpGet("GetAssignedReceivables")]
        //public async Task<IActionResult> GetAssignedReceivablesAsync()
        //{
        //    var result = new List<ReceivableMobileLM>();
        //    var user = await _userManager.FindByNameAsync(User.Identity.Name);
        //    _assignedCollectorService.GetAssignedCollectors(_ => _.UserId == user.Id && _.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE).ToList().ForEach(_ =>
        //    {
        //        var model = _receivableService.GetReceivable(_.ReceivableId);
        //        var receivable = model.Adapt<ReceivableMobileLM>();
        //        receivable.DebtorName = model.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).SingleOrDefault().Name;
        //        receivable.DebtorId = model.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).SingleOrDefault().Id;
        //        receivable.ProgressPercent = GetProgressReached(model);
        //        receivable.AssignDate = model.AssignedCollectors.SingleOrDefault(ac => ac.ReceivableId == _.ReceivableId && ac.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE).CreatedDate;
        //        result.Add(receivable);
        //    });
        //    return Ok(result);
        //}


        #region Mobile
        [Authorize]
        [HttpPost("GetAssignedReceivables/{isHistory}")]
        public async Task<IActionResult> GetAssignedReceivables(bool isHistory)
        {
            var result = new List<ReceivableMobileLM>();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            _assignedCollectorService.GetAssignedCollectors(_ => _.UserId == user.Id && _.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE).ToList().ForEach(_ =>
            {
                //if (isHistory)
                //    result.Add(ParseReceivableMobile(_receivableService.GetReceivable(r => r.Id == _.ReceivableId && r.CollectionProgress.Status != Constant.COLLECTION_STATUS_COLLECTION_CODE)));
                //else { }
                //result.Add(ParseReceivableMobile(_receivableService.GetReceivable(r => r.Id == _.ReceivableId && r.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE)));
                var receivable = _receivableService.GetReceivable(r => r.Id == _.ReceivableId);

                if (isHistory)
                {
                    if (receivable.CollectionProgress.Status != Constant.COLLECTION_STATUS_COLLECTION_CODE && receivable.CollectionProgress.Status != Constant.COLLECTION_STATUS_DONE_CODE)
                    {
                        result.Add(ParseReceivableMobile(receivable));
                    }
                }
                else
                {
                    if (receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE || receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_DONE_CODE)
                    {
                        result.Add(ParseReceivableMobile(receivable));
                    }
                }
            });
            return Ok(result);
        }

        [Authorize]
        [HttpPost("GetAssignedReceivables")]
        public async Task<IActionResult> GetAssignedReceivables([FromBody]List<int> receivableIdList)
        {
            var result = new List<ReceivableMobileLM>();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var models = new List<AssignedCollector>();
            //Get all or get by receivable list id
            if (receivableIdList.Count > 0)
            {
                models = _assignedCollectorService.GetAssignedCollectors(_ => _.UserId == user.Id && _.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE && receivableIdList.Contains(_.ReceivableId)).ToList();
            }
            else
            {
                models = _assignedCollectorService.GetAssignedCollectors(_ => _.UserId == user.Id && _.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE).ToList();
            }
            models.ForEach(_ =>
            {
                result.Add(ParseReceivableMobile(_receivableService.GetReceivable(r => r.Id == _.ReceivableId)));
            });
            //Sorting
            //List<int> status = new List<int>() { Constant.COLLECTION_STATUS_DONE_CODE,Constant.COLLECTION_STATUS_CANCEL_CODE,Constant.COLLECTION_STATUS_COLLECTION_CODE,Constant.COLLECTION_STATUS_CLOSED_CODE };
            return Ok(result);
        }



        //public async Task<IActionResult> GetAssignedReceivables([FromBody]List<int> receivableIdList, [FromQuery] bool isHistory)
        //{
        //    var result = new List<ReceivableMobileLM>();
        //    var user = await _userManager.FindByNameAsync(User.Identity.Name);
        //    var models = new List<AssignedCollector>();
        //    //Get all or get by receivable list id
        //    if (receivableIdList.Count > 0)
        //    {
        //        models = _assignedCollectorService.GetAssignedCollectors(_ => _.UserId == user.Id && _.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE && receivableIdList.Contains(_.ReceivableId)).ToList();
        //    }
        //    else
        //    {
        //        models = _assignedCollectorService.GetAssignedCollectors(_ => _.UserId == user.Id && _.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE).ToList();
        //    }
        //    models.ForEach(_ =>
        //    {
        //        //if (isHistory)
        //        //    result.Add(ParseReceivableMobile(_receivableService.GetReceivable(r => r.Id == _.ReceivableId && r.CollectionProgress.Status != Constant.COLLECTION_STATUS_COLLECTION_CODE)));
        //        //else { }
        //        //result.Add(ParseReceivableMobile(_receivableService.GetReceivable(r => r.Id == _.ReceivableId && r.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE)));
        //        var receivable = _receivableService.GetReceivable(r => r.Id == _.ReceivableId);

        //        if (isHistory)
        //        {
        //            if (receivable.CollectionProgress.Status != Constant.COLLECTION_STATUS_COLLECTION_CODE)
        //            {
        //                result.Add(ParseReceivableMobile(receivable));
        //            }
        //        }
        //        else
        //        {
        //            if (receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE)
        //            {
        //                result.Add(ParseReceivableMobile(receivable));
        //            }
        //        }
        //    });
        //    return Ok(result);
        //}
        [Authorize]
        [HttpGet("GetAssignedReceivable/{receivableId}")]
        public async Task<IActionResult> GetAssignedReceivableAsync(int receivableId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var assignedCollector = _assignedCollectorService.GetAssignedCollector(_ => _.UserId == user.Id && _.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE && receivableId == _.ReceivableId && !_.IsDeleted);
            var model = _receivableService.GetReceivable(assignedCollector.ReceivableId);
            return Ok(ParseReceivableMobile(model));
        }

        private ReceivableMobileLM ParseReceivableMobile(Receivable model)
        {
            var receivable = new ReceivableMobileLM();
            receivable.Id = model.Id;
            receivable.PayableDay = model.PayableDay;
            receivable.PrepaidAmount = model.PrepaidAmount;
            receivable.DebtAmount = model.DebtAmount;
            receivable.ClosedDay = model.ClosedDay;
            receivable.CustomerName = model.Customer.Name;
            receivable.DebtorId = model.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE && !contact.IsDeleted).FirstOrDefault().Id;
            receivable.DebtorName = model.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE && !contact.IsDeleted).FirstOrDefault().Name;
            receivable.Contacts = GetReceivableContactsForDetailView(model.Contacts.Where(con => !con.IsDeleted));
            receivable.CollectionProgressStatus = model.CollectionProgress.Status;
            receivable.ExpectationClosedDay = Utility.ConvertDatimeToInt((DateTime)model.ExpectationClosedDay);
            receivable.IsConfirmed = model.IsConfirmed;
            receivable.AssignDate = Utility.ConvertDatimeToInt(model.AssignedCollectors.FirstOrDefault(ac => ac.ReceivableId == receivable.Id && ac.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE && !ac.IsDeleted).CreatedDate);
            receivable.Action = GetNextOrLastAction(model);
            receivable.TimeRate = GetProgressReached(model);
            receivable.ProgressStage = GetProgressStage(model);
            return receivable;
        }

        private ProgressStageMobileVM GetProgressStage(Receivable receivable)
        {
            var result = new ProgressStageMobileVM();
            if (receivable.CollectionProgress.Receivable.PayableDay > Utility.ConvertDatimeToInt(DateTime.Now))
            {
                var progressStage = receivable.CollectionProgress.ProgressStages.FirstOrDefault();
                result.CurrentStageIndex = 0;
                result.CurrentStageName = progressStage.Name;
                result.Actions = progressStage.ProgressStageAction.Select(action => new ProgressStageActionMobileDM
                {
                    Name = action.Name
                });
                return result;
            }
            //Receivable is closed=> get Last action
            if (receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_CANCEL_CODE || receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_CLOSED_CODE)
            {

                var stages = receivable.CollectionProgress.ProgressStages.ToList();
                result.Actions = stages.Select(action => new ProgressStageActionMobileDM
                {
                    Name = action.Name
                });
                var psaList = new List<ProgressStageAction>();
                foreach (var item in stages)
                {
                    foreach (var psa in item.ProgressStageAction.ToList())
                    {
                        if (psa.ExcutionDay <= receivable.ClosedDay)
                        {
                            psaList.Add(psa);
                        }
                    }
                }
                if (psaList.Any())
                {
                    for (var i = 0; i < stages.Count; i++)
                    {
                        foreach (var psa in stages[i].ProgressStageAction.ToList())
                        {
                            if (psa.Id == psaList.LastOrDefault().Id)
                            {
                                result.CurrentStageName = stages[i].Name;
                                result.CurrentStageIndex = i;
                            }
                        }
                    }

                    return result;
                }
                else
                //Don't excute any action
                {
                    result.CurrentStageName = stages[0].Name;
                    result.CurrentStageIndex = 0;
                    return result;
                }

            }
            //Receivable is in collecting
            else
            {
                var stages = receivable.CollectionProgress.ProgressStages.ToList();
                result.Actions = stages.Select(action => new ProgressStageActionMobileDM
                {
                    Name = action.Name
                });
                var psaList = new List<ProgressStageAction>();
                foreach (var item in stages)
                {
                    foreach (var psa in item.ProgressStageAction.ToList())
                    {
                        if (psa.ExcutionDay >= Utility.ConvertDatimeToInt(DateTime.Now))
                        {
                            psaList.Add(psa);
                        }
                    }
                }
                if (psaList.Any())
                {
                    var psa = psaList.FirstOrDefault();
                    result.CurrentStageName = psa.ProgressStage.Name;
                    for (int i = 0; i < stages.Count; i++)
                    {
                        if (stages[i].Id == psa.ProgressStage.Id)
                        {
                            result.CurrentStageIndex = i;
                        }
                    }
                    return result;
                }
                //Don't have any remaining action
                result.CurrentStageIndex = stages.Count;
                result.CurrentStageName = stages.LastOrDefault().Name;
                return result;
            }
        }

        /// <summary>
        /// Get next Action if status of receivable is collecting, Last action if status is Cancel or Close
        /// </summary>
        /// <param name="receivable"></param>
        /// <returns></returns>
        private NextAction GetNextOrLastAction(Receivable receivable)
        {
            var result = new NextAction();
            //Receivable not start yet
            if (receivable.CollectionProgress.Receivable.PayableDay > Utility.ConvertDatimeToInt(DateTime.Now))
            {
                var action = receivable.CollectionProgress.ProgressStages.FirstOrDefault().ProgressStageAction.FirstOrDefault();
                result.Type = action.Type;
                result.Time = Utility.ConvertIntToDatetime(action.ExcutionDay).Add(Utility.ConvertIntToTimeSpan(action.StartTime));
                return result;
            }
            //Receivable is closed=> get Last action
            if (receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_CANCEL_CODE || receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_CLOSED_CODE)
            {
                var stages = receivable.CollectionProgress.ProgressStages.ToList();
                var psaList = new List<ProgressStageAction>();
                foreach (var item in stages)
                {
                    foreach (var psa in item.ProgressStageAction.ToList())
                    {
                        if (psa.ExcutionDay <= receivable.ClosedDay)
                        {
                            psaList.Add(psa);
                        }
                    }
                }
                if (psaList.Any())
                {
                    var psa = psaList.LastOrDefault();
                    result.Type = psa.Type;
                    result.Time = Utility.ConvertIntToDatetime(psa.ExcutionDay).Add(Utility.ConvertIntToTimeSpan(psa.StartTime));
                    return result;
                }
                //Don't excute any action
                result.Type = 0;
                return result;
            }
            //Receivable is in collecting
            else
            {
                var stages = receivable.CollectionProgress.ProgressStages.ToList();
                var psaList = new List<ProgressStageAction>();
                foreach (var item in stages)
                {
                    foreach (var psa in item.ProgressStageAction.ToList())
                    {
                        if (psa.ExcutionDay >= Utility.ConvertDatimeToInt(DateTime.Now))
                        {
                            psaList.Add(psa);
                        }
                    }
                }
                if (psaList.Any())
                {
                    var psa = psaList.FirstOrDefault();
                    result.Type = psa.Type;
                    result.Time = Utility.ConvertIntToDatetime(psa.ExcutionDay).Add(Utility.ConvertIntToTimeSpan(psa.StartTime));
                    return result;
                }
                //Don't have any remaining action
                result.Type = 0;
                return result;
            }


        }
        private NextAction GetStageAction(Receivable receivable)
        {
            var result = new NextAction();
            //Receivable not start yet
            if (receivable.CollectionProgress.Receivable.PayableDay > Utility.ConvertDatimeToInt(DateTime.Now) || receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_WAIT_CODE)
            {
                var action = receivable.CollectionProgress.ProgressStages.First().ProgressStageAction.First();
                result.Type = action.Type;
                result.Time = Utility.ConvertIntToDatetime(action.ExcutionDay).Add(Utility.ConvertIntToTimeSpan(action.StartTime));
                return result;
            }
            //
            var stage = receivable.CollectionProgress.ProgressStages.Where(cp => cp.ProgressStageAction.Where(psa => psa.ExcutionDay <= Utility.ConvertDatimeToInt(DateTime.Now)).FirstOrDefault() != null).Last();
            if (stage != null)
            {
                var action = stage.ProgressStageAction.Last(psa => psa.ExcutionDay <= Utility.ConvertDatimeToInt(DateTime.Now));
                if (action != null)
                {
                    if (action.Status != Constant.COLLECTION_STATUS_DONE_CODE)
                    {
                        result.Type = action.Type;
                        result.Time = Utility.ConvertIntToDatetime(action.ExcutionDay).Add(Utility.ConvertIntToTimeSpan(action.StartTime));
                        return result;
                    }
                    result.Type = action.Type;
                    result.Time = Utility.ConvertIntToDatetime(action.ExcutionDay).Add(Utility.ConvertIntToTimeSpan(action.StartTime));
                    return result;
                }
            }
            //Receivable already close
            return null;
        }
        #endregion

        private int GetPercent(Receivable receivable)
        {

            if (receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE)
            {
                return CalculatePercent((DateTime.Now - Utility.ConvertIntToDatetime(receivable.PayableDay.Value)).TotalDays, (receivable.ExpectationClosedDay.Value - Utility.ConvertIntToDatetime(receivable.PayableDay.Value)).TotalDays);
            }
            else if (receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_WAIT_CODE || receivable.CollectionProgress.Receivable.PayableDay > Utility.ConvertDatimeToInt(DateTime.Now))
            {
                return 0;
            }
            else
            {
                return CalculatePercent((Utility.ConvertIntToDatetime(receivable.ClosedDay.Value) - Utility.ConvertIntToDatetime(receivable.PayableDay.Value)).TotalDays, (receivable.ExpectationClosedDay.Value - Utility.ConvertIntToDatetime(receivable.PayableDay.Value)).TotalDays);
            }

        }
        private int CalculatePercent(double numberOfDays, double totalDays)
        {
            var result = numberOfDays / totalDays * 100;
            if (result < 0)
                return 0;
            return (int)Math.Round(result, MidpointRounding.AwayFromZero);
        }
        [HttpPut("OpenReceivable")]
        public IActionResult OpenReceivable([FromBody] ReceivableOpenModel receivableOM)
        {
            var receivable = _receivableService.GetReceivable(receivableOM.Id);
            if (receivable != null)
            {
                receivable.CollectionProgress.Status = Constant.COLLECTION_STATUS_COLLECTION_CODE;
                _receivableService.EditReceivable(receivable);
                _receivableService.SaveReceivable();
                return Ok();
            }

            return NotFound();
        }

        [HttpGet("GetReceivaleByCollectorId/{collectorId}")]
        public IActionResult GetReceivableByCollectorId(string collectorId)
        {
            var rawList = _receivableService.GetReceivables().Select(x => new ReceivableLM()
            {
                Id = x.Id,
                ClosedDay = x.ClosedDay,
                CustomerId = x.CustomerId,
                DebtAmount = x.DebtAmount,
                LocationId = x.LocationId,
                PayableDay = x.PayableDay != null ? x.PayableDay : null,
                PrepaidAmount = x.PrepaidAmount,
                CollectionProgressStatus = x.CollectionProgress.Status,
                CollectionProgressId = x.CollectionProgress.Id,
                AssignedCollectorId = x.AssignedCollectors.FirstOrDefault(assignedCollector => assignedCollector.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE) != null ? x.AssignedCollectors.FirstOrDefault(assignedCollector => assignedCollector.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE).UserId : "",
                CustomerName = x.Customer.Name,
                DebtorName = x.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).FirstOrDefault().Name,
                DebtorId = x.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).FirstOrDefault().Id,
                ProgressPercent = GetProgressReached(x),
                HaveLateAction = HaveLateActions(x),
                IsConfirmed = x.IsConfirmed,
                Stage = GetStageName(x),
                ProfileId = x.CollectionProgress.ProfileId
            });

            if (rawList.Any())
            {
                var result = rawList.Where(x => x.AssignedCollectorId == collectorId);
                if (result.Any())
                {
                    return Ok(result);
                }
            }

            return NotFound();
        }

        [HttpPut("CloseReceivable")]
        public async Task<IActionResult> CloseReceivableAsync([FromBody] ReceivableCloseModel receivableCM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var receivable = _receivableService.GetReceivable(receivableCM.Id);

            if (receivableCM.isPayed)
            {
                receivable.CollectionProgress.Status = Constant.COLLECTION_STATUS_CLOSED_CODE;
                receivable.CollectionProgress.UpdatedDate = DateTime.Now;
            }
            else
            {
                receivable.CollectionProgress.Status = Constant.COLLECTION_STATUS_CANCEL_CODE;
                receivable.CollectionProgress.UpdatedDate = DateTime.Now;
            }

            if (receivable == null)
            {
                return NotFound();
            }

            _receivableService.CloseReceivable(receivable);
            _receivableService.SaveReceivable();
            await SendConfirmReceivableNotificationToManager(receivable);

            return Ok(new { ClosedTime = receivable.ClosedDay, DebtAmount = receivable.DebtAmount, Status = receivable.CollectionProgress.Status });
        }

        [HttpPut("Confirm")]
        public IActionResult MarkReceivableAsFinished([FromBody] ReceivableOpenModel receivableCM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var receivable = _receivableService.GetReceivable(receivableCM.Id);

            if (receivable == null)
            {
                return NotFound();
            }

            if (receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_CANCEL_CODE || receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_CLOSED_CODE)
            {
                receivable.IsConfirmed = true;
                receivable.UpdatedDate = DateTime.Now;
                _receivableService.CloseReceivable(receivable);
                _receivableService.SaveReceivable();
            }
            else
            {
                return BadRequest(new { Message = "Receivable is not closed or canceled so cannot be confirmed." });
            }

            return Ok(new { ClosedTime = receivable.ClosedDay, DebtAmount = receivable.DebtAmount, Status = receivable.CollectionProgress.Status, IsConfirmed = receivable.IsConfirmed });
        }

        [HttpGet("GetReceivableGroupByCollector")]
        public IActionResult GetReceivableGroupByCollector()
        {
            var result = new List<ReceivableGroupByCollectorModel>();
            var userList = _userManager.GetUsersInRoleAsync("Collector").Result;
            if (userList.Any())
            {
                foreach (var user in userList)
                {
                    var collectorReceivable = GetReceivablesByCollector(user.Id);
                    var collectorActiveReceivable = GetReceivablesCollectingByCollectorFilterByAssignedStatus(Constant.ASSIGNED_STATUS_ACTIVE_CODE, collectorReceivable);

                    result.Add(new ReceivableGroupByCollectorModel()
                    {
                        CollectorId = user.Id,
                        CollectorName = user.LastName + " " + user.FirstName,
                        NumberOfAssignedReceivable = collectorReceivable.Count(),
                        NumberOfReceivableInCollectingProgress = collectorActiveReceivable.Count(),
                        rate = GetRate(collectorReceivable)
                    });
                }
                return Ok(result);
            }

            return Ok();
        }

        private Rate GetRate(IEnumerable<Receivable> receivables)
        {
            Rate rate = new Rate();
            if (receivables.Any())
            {
                var closedOrCanceledAmount = receivables
                    .Where(receivable =>
                    receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_CLOSED_CODE
                    || receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_CANCEL_CODE
                ).Count();
                if (closedOrCanceledAmount > 0)
                {
                    var closedAmount = receivables
                   .Where(receivable =>
                        receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_CLOSED_CODE
                         ).Count();

                    var canceledAmount = receivables
                        .Where(receivable =>
                        receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_CANCEL_CODE
                         ).Count();

                    rate.FailRate = Math.Round(((double)canceledAmount / closedOrCanceledAmount) * 100, MidpointRounding.AwayFromZero);
                    rate.SuccessRate = Math.Round(((double)closedAmount / closedOrCanceledAmount)  * 100, MidpointRounding.AwayFromZero);
                }
            }
            return rate;
        }

        private IEnumerable<Receivable> GetReceivablesByCollector(string CollectorId)
        {
            var result = _receivableService.GetReceivables()
                .Where(receivable =>
                    receivable.AssignedCollectors
                    .Any(x =>
                    x.UserId == CollectorId
                    && x.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE)
                );
            return result;
        }

        private IEnumerable<Receivable> GetReceivablesCollectingByCollectorFilterByAssignedStatus(int assignStatus, IEnumerable<Receivable> receivablesByCollector)
        {
            if (receivablesByCollector.Any())
            {
                var result = receivablesByCollector
                    .Where(receivable =>
                        receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE
                        && receivable.AssignedCollectors
                        .Any(x =>
                            x.Status == assignStatus
                        )
                    );
                return result;
            }
            return new List<Receivable>();
        }

        [HttpGet("{id}")]
        public IActionResult GetReceivableDetail(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var receivable = GetReceivable(id);
            if (receivable == null)
            {
                return NotFound();
            }

            return Ok(receivable);
        }

        [HttpPost("Validate")]
        public IActionResult ValidateAndMakeForReceivables([FromBody] IEnumerable<ReceivableIM> receivableIMs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Validate receivable before import to DB.
            ReceivableIM tmp;
            foreach (var receivable in receivableIMs)
            {
                tmp = ValidateImportReceivable(receivable);
                if (tmp != null)
                {
                    return BadRequest(tmp);
                }
            }
            return Ok(receivableIMs);
        }

        [HttpPost]
        public async Task<IActionResult> AddReceivablesAsync([FromBody] IEnumerable<ReceivableIM> receivableIMs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var importList = new List<Receivable>();
            var receivablesDBM = TransformReceivablesToDBM(receivableIMs);
            if (receivablesDBM.Any())
            {
                foreach (var receivableDBM in receivablesDBM)
                {
                    importList.Add(_receivableService.CreateReceivable(receivableDBM));
                    _receivableService.SaveReceivable();
                }
                //var importedReceivables = _receivableService.GetReceivables().OrderByDescending(x => x.Id).Take(receivablesDBM.Count());
                await SendNewReceivableNotificationAsync(importList);
                return Ok(importList.ToList().Select(_ => _.Id));
            }
            return BadRequest(new { Message = "Error when trying to import" });
        }

        private async Task SendNewReceivableNotificationAsync(List<Receivable> receivables)
        {

            #region Create New Receivable Notification
            List<UserNotification> userNotifications = new List<UserNotification>();
            //Get user need to notify
            _assignedCollectorService.GetAssignedCollectors().ToList().ForEach(_ =>
            {
                receivables.ForEach(receivable =>
                {
                    if (_.ReceivableId == receivable.Id)
                    {
                        if (userNotifications.Count < 1 || userNotifications.FirstOrDefault(un => un.UserId == _.UserId) == null)
                        {
                            userNotifications.Add(new UserNotification()
                            {
                                UserId = _.UserId,
                                ReceivableList = new List<int>() { receivable.Id }
                            });

                        }
                        else
                        {
                            userNotifications.First(un => un.UserId == _.UserId).ReceivableList.Add(receivable.Id);
                        }
                    }
                });
            });
            //Create New Receivable Notification
            List<Notification> notifications = new List<Notification>();
            foreach (var _ in userNotifications)
            {
                notifications.Add(new Notification()
                {
                    Title = Constant.NOTIFICATION_TYPE_NEW_RECEIVABLE,
                    Type = Constant.NOTIFICATION_TYPE_NEW_RECEIVABLE_CODE,
                    Body = $"You were assgined to {_.ReceivableList.Count} reiceivable from {_customerService.GetCustomer(receivables.First().CustomerId).Name}",
                    UserId = _.UserId,
                    NData = JsonConvert.SerializeObject(_.ReceivableList),
                    IsSeen = false,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                });
            }
            //userNotifications.ForEach(_ =>
            //{
            //    notifications.Add(new Notification()
            //    {
            //        Title = Constant.NOTIFICATION_TYPE_NEW_RECEIVABLE,
            //        Type = Constant.NOTIFICATION_TYPE_NEW_RECEIVABLE_CODE,
            //        Body = $"You were assgined to {_.ReceivableList.Count}{JsonConvert.SerializeObject(_.ReceivableList)} reiceivable from {receivables.First().Customer.Name}",
            //        UserId = _.UserId,
            //        NData = JsonConvert.SerializeObject(_.ReceivableList),
            //        IsSeen = false,
            //        CreatedDate = DateTime.Now,
            //        IsDeleted = false,
            //    });
            //});
            _notificationService.CreateNotification(notifications);
            _notificationService.SaveNotification();
            #endregion
            //Send
            await SendNotificationToClientAsync(notifications);
        }


        private async Task SendConfirmReceivableNotificationToManager(Receivable receivable)
        {
            #region Create New Receivable Notification
            var user = await _userManager.FindByNameAsync("manager");
            //Create New Receivable Notification
            Notification notification = new Notification()
            {
                Title = Constant.NOTIFICATION_TYPE_CLOSE_RECEIVABLE,
                Type = Constant.NOTIFICATION_TYPE_CLOSE_RECEIVABLE_CODE,
                Body = $"You have reiceivable of {receivable.Contacts.FirstOrDefault().Name} from {receivable.Customer.Name} need to confirm!",
                UserId = user.Id,
                NData = JsonConvert.SerializeObject(receivable.Id),
                IsSeen = false,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };
            _notificationService.CreateNotification(notification);
            _notificationService.SaveNotification();
            #endregion
            //Send
            await SendNotificationToClient(notification);
        }
        private async Task SendNotificationToClientAsync(List<Notification> notifications)
        {
            await NotificationUtility.NotificationUtility.SendNotification(notifications, _hubService, _hubContext, _firebaseTokenService);
        }

        private async Task SendNotificationToClient(Notification notification)
        {
            await NotificationUtility.NotificationUtility.SendNotification(notification, _hubService, _hubContext, _firebaseTokenService);
        }
        #region ChangeAsignedCollector
        [HttpPost("ChangeAsignedCollector")]
        public async Task<IActionResult> ChangeAssignedCollectorAsync([FromBody]AssignedCollectorUM assignedCollectorUM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var receivable = _receivableService.GetReceivable(assignedCollectorUM.ReceivableId);
            receivable.AssignedCollectors.Select(x => { x.Status = Constant.ASSIGNED_STATUS_DEACTIVE_CODE; return x; }).ToList();

            receivable.AssignedCollectors.Add(new AssignedCollector()
            {
                Status = Constant.ASSIGNED_STATUS_ACTIVE_CODE,
                UserId = assignedCollectorUM.CollectorId,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            });
            _receivableService.EditReceivable(receivable);
            _receivableService.SaveReceivable();
            await SendAsignedCollectorReceivableNotification(receivable, assignedCollectorUM.CollectorId);
            return Ok();
        }

        private async Task SendAsignedCollectorReceivableNotification(Receivable receivable, string collectorId)
        {
            #region Create New Receivable Notification
            var user = await _userManager.FindByNameAsync("manager");
            //Create New Receivable Notification
            Notification notification = new Notification()
            {
                Title = Constant.NOTIFICATION_TYPE_ASSIGN_RECEIVABLE,
                Type = Constant.NOTIFICATION_TYPE_ASSIGN_RECEIVABLE_CODE,
                Body = $"You were assigned to receivable of {receivable.Contacts.FirstOrDefault().Name} from {receivable.Customer.Name}!",
                UserId = collectorId,
                NData = JsonConvert.SerializeObject(receivable.Id),
                IsSeen = false,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };
            _notificationService.CreateNotification(notification);
            _notificationService.SaveNotification();
            #endregion
            //Send
            await SendNotificationToClient(notification);
        }
        #endregion

        [HttpGet("GetAssignedCollectorHistory")]
        public IActionResult GetReceivableAssignedCollectorHisory(int receivableId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var receivable = _receivableService.GetReceivable(receivableId);
            if (receivable != null)
            {
                IEnumerable<AssignedCollectorHM> result = receivable.AssignedCollectors.Select(x => new AssignedCollectorHM()
                {
                    Id = x.Id,
                    CollectorId = x.UserId,
                    ReceivableId = x.ReceivableId,
                    Status = x.Status,
                    CreatedDate = x.CreatedDate

                });
                if (result.Any())
                {
                    return Ok(result);
                }
            }

            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReceivableAsync([FromBody] ReceivableUM receivableIM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var receivable = _receivableService.GetReceivable(receivableIM.Id);
            if (receivable != null)
            {
                receivable.DebtAmount = receivableIM.DebtAmount;
                receivable.PrepaidAmount = receivableIM.PrepaidAmount;
                //bool closed = false;
                if (receivable.DebtAmount == receivable.PrepaidAmount)
                {
                    _receivableService.CloseReceivable(receivable);
                    //closed = true;
                }
                else
                {
                    _receivableService.EditReceivable(receivable);
                }
                _receivableService.SaveReceivable();
                //if (closed)
                //    await SendCloseReceivableNotification(receivable);
                return Ok();
            }
            return NotFound();
        }

        private async Task SendCloseReceivableNotification(Receivable receivable)
        {
            #region Create New Receivable Notification
            CloseReceivable userNotification = new CloseReceivable()
            {
                ReceivableId = receivable.Id,
                UserId = _assignedCollectorService.GetAssignedCollectors(_ => _.ReceivableId == receivable.Id).Last().UserId
            };
            //Create New Receivable Notification
            Notification notification = new Notification()
            {
                Title = Constant.NOTIFICATION_TYPE_CLOSE_RECEIVABLE,
                Type = Constant.NOTIFICATION_TYPE_CLOSE_RECEIVABLE_CODE,
                Body = $"You assgined receivable is closed!",
                UserId = userNotification.UserId,
                NData = JsonConvert.SerializeObject(userNotification),
                IsSeen = false,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };
            _notificationService.CreateNotification(notification);
            _notificationService.SaveNotification();
            #endregion
            //Send
            await SendNotificationToClient(notification);
        }

        [HttpPut("AssignReceivable")]
        public async Task<IActionResult> AssignReceivableAsync([FromBody] IEnumerable<ReceivableAssignModel> receivableAMs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = new List<ReceivableDM>();
            foreach (var receivableAM in receivableAMs)
            {
                var receivable = _receivableService.GetReceivable(receivableAM.Id);
                if (receivable != null)
                {
                    DateTime? endDay = null;
                    if (receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_WAIT_CODE)
                    {
                        receivable.CollectionProgress.Status = Constant.COLLECTION_STATUS_COLLECTION_CODE;
                        receivable.PayableDay = receivableAM.PayableDay;
                        receivable.AssignedCollectors.Add(TransformAssignedCollectorToDBM(receivableAM.CollectorId));
                        receivable.ExpectationClosedDay = CalculateEndDay(receivable.CollectionProgress.ProgressStages, (int)receivable.PayableDay);
                        //Convert contact to contactIM
                        var contacts = new List<ContactIM>();
                        foreach (var contact in receivable.Contacts)
                        {
                            contacts.Add(new ContactIM()
                            {
                                Address = contact.Address,
                                IdNo = contact.IdNo,
                                Name = contact.Name,
                                Phone = contact.Phone,
                                Type = contact.Type,
                            });
                        }

                        if (contacts.Any())
                        {
                            var stages = TransformProgressStageToDBM(receivableAM.Profile.Stages, receivableAM.PayableDay, GetDebtorName(contacts), receivable.DebtAmount);
                            endDay = CalculateEndDay(stages, (int)receivable.PayableDay);

                            if (stages.Any())
                            {
                                receivable.CollectionProgress.ProgressStages = stages.ToList();
                                receivable.ExpectationClosedDay = endDay;
                            }

                            _receivableService.EditReceivable(receivable);
                            _receivableService.SaveReceivable();
                            result.Add(GetReceivable(receivable.Id));
                        }
                        //End if contacts.Any()
                        await SendAsignedCollectorReceivableNotification(receivable, receivableAM.CollectorId);
                    }
                    else
                    {
                        return BadRequest(new { Message = "One or more receivable is in collection progress and cannot be change." });
                    }
                    //End if receivable.CollectionProgress.Status != Constant.COLLECTION_STATUS_COLLECTION_CODE
                }
                //End if receivable != null
            }

            return Ok(result);
        }

        [HttpGet("GetReceivablesById")]
        public IActionResult GetReceivablesById([FromQuery] int[] receivableId)
        {

            var result = _receivableService.GetReceivables(_ => receivableId.Contains(_.Id)).Select(x => new ReceivableLM()
            {
                Id = x.Id,
                ClosedDay = x.ClosedDay,
                CustomerId = x.CustomerId,
                DebtAmount = x.DebtAmount,
                LocationId = x.LocationId,
                PayableDay = x.PayableDay != null ? x.PayableDay : null,
                PrepaidAmount = x.PrepaidAmount,
                CollectionProgressStatus = x.CollectionProgress.Status,
                CollectionProgressId = x.CollectionProgress.Id,
                AssignedCollectorId = x.AssignedCollectors.FirstOrDefault(assignedCollector => assignedCollector.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE) != null ? x.AssignedCollectors.FirstOrDefault(assignedCollector => assignedCollector.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE).UserId : "",
                CustomerName = x.Customer.Name,
                DebtorName = x.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).FirstOrDefault().Name,
                DebtorId = x.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).FirstOrDefault().Id,
                ProgressPercent = GetProgressReached(x),
                HaveLateAction = HaveLateActions(x),
                IsConfirmed = x.IsConfirmed,
                ProfileId = x.CollectionProgress.ProfileId
            });

            return Ok(result);


        }

        private ReceivableDM GetReceivable(int id)
        {
            var receivableDBM = _receivableService.GetReceivable(id);
            if (receivableDBM != null)
            {
                var receivableVM = new ReceivableDM()
                {
                    Id = receivableDBM.Id,
                    DebtAmount = receivableDBM.DebtAmount,
                    PayableDay = receivableDBM.PayableDay,
                    PrepaidAmount = receivableDBM.PrepaidAmount,
                    IsConfirmed = receivableDBM.IsConfirmed,
                    CustomerId = receivableDBM.CustomerId,
                    LocationId = receivableDBM.LocationId,
                    ClosedDay = receivableDBM.ClosedDay,
                    Contacts = GetReceivableContactsForDetailView(receivableDBM.Contacts),
                    CollectionProgress = GetCollectionProgressForDetailView(receivableDBM.CollectionProgress),
                    assignedCollector = receivableDBM.AssignedCollectors.FirstOrDefault(x => x.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE) != null ? GetAssignedCollectorForDetailView(receivableDBM.AssignedCollectors.FirstOrDefault(x => x.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE)) : null
                };
                return receivableVM;

            }
            return null;
        }

        private AssignedCollectorVM GetAssignedCollectorForDetailView(AssignedCollector assignedCollector)
        {
            return new AssignedCollectorVM()
            {
                Id = assignedCollector.Id,
                ReceivableId = assignedCollector.ReceivableId,
                CollectorId = assignedCollector.UserId,
                Status = assignedCollector.Status
            };
        }

        private IEnumerable<ContactVM> GetReceivableContactsForDetailView(IEnumerable<Contact> contacts)
        {
            return contacts
                .Select(x => new ContactVM()
                {
                    Id = x.Id,
                    Address = x.Address,
                    IdNo = x.IdNo,
                    Name = x.Name,
                    Phone = x.Phone,
                    Type = x.Type
                });
        }

        private CollectionProgressDM GetCollectionProgressForDetailView(CollectionProgress collectionProgress)
        {
            return new CollectionProgressDM()
            {
                Id = collectionProgress.Id,
                ProfileId = collectionProgress.ProfileId,
                ReceivableId = collectionProgress.Receivable.Id,
                Status = collectionProgress.Status,
                Stages = GetProgressStagesForDetailView(collectionProgress),
            };
        }

        private IEnumerable<ProgressStageDM> GetProgressStagesForDetailView(CollectionProgress collectionProgress)
        {
            return collectionProgress.ProgressStages.Select(x => new ProgressStageDM()
            {
                Id = x.Id,
                Actions = GetProgressStageActionsForDetailView(x.ProgressStageAction),
                CollectionProgressId = x.CollectionProgressId,
                Duration = x.Duration,
                Name = x.Name,
                Sequence = x.Sequence,
                Status = x.Status
            });
        }

        private IEnumerable<ProgressStageActionDM> GetProgressStageActionsForDetailView(IEnumerable<ProgressStageAction> progressStageActions)
        {
            return progressStageActions.Select(x => new ProgressStageActionDM()
            {
                Id = x.Id,
                Name = x.Name,
                StartTime = x.StartTime,
                DoneAt = x.DoneAt,
                ExcutionDay = x.ExcutionDay,
                Status = x.Status,
                Type = x.Type,
                ProgressStageId = x.ProgressStageId,
                ProgressMessageFormId = x.ProgressMessageFormId,
                Evidence = x.Evidence,
                Note = x.Note
            });
        }

        private Profile GetProfileFromDB(int profileId)
        {
            var Profile = _profileService.GetProfile(profileId);
            if (Profile != null && Profile.IsDeleted == false)
            {
                return Profile;
            }
            return null;
        }

        private ProfileMessageForm GetProfileMessageFormFromDB(int messageId)
        {
            var messages = _profileMessageFormService.GetProfileMessageForms().Where(x => (x.Id == messageId && x.IsDeleted == false)).LastOrDefault();
            if (messages != null)
            {
                return messages;
            }
            return null;
        }

        //Validate receivable
        //===================
        private ReceivableIM ValidateImportReceivable(ReceivableIM receivableIM)
        {
            //First, check debt amount
            if (receivableIM.DebtAmount == 0 || receivableIM.PrepaidAmount < 0 || receivableIM.PrepaidAmount >= receivableIM.DebtAmount)
            {
                return receivableIM;
            }

            //Then validate contacts.
            if (!ValidateContacts(receivableIM.Contacts))
            {
                return receivableIM;
            }
            return null;
        }

        //Validate contact.
        private bool ValidateContacts(IEnumerable<ContactIM> contactIMs)
        {
            foreach (var contact in contactIMs)
            {
                //First, check the name of the contact
                if (string.IsNullOrEmpty(contact.Name))
                {
                    return false;
                }

                //Check other fields.
                //If contact is a Debtor, all field must not be empty.
                //If contact is a Debtor's relatives, one field are required.

                //If all field is empty.
                if (string.IsNullOrEmpty(contact.Address) && string.IsNullOrEmpty(contact.IdNo) && string.IsNullOrEmpty(contact.Phone))
                {
                    return false;
                }

                //If there is at least 1 empty field
                if (string.IsNullOrEmpty(contact.Address) || string.IsNullOrEmpty(contact.IdNo) || string.IsNullOrEmpty(contact.Phone))
                {
                    if (contact.Type == Constant.CONTACT_DEBTOR_CODE)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //Suggest profile for receivable
        private int SuggestProfile(Receivable receivable)
        {
            var debt = receivable.DebtAmount;
            var sugguestedProfiles = _profileService.GetProfiles().Where(x => (x.DebtAmountFrom <= debt && x.DebtAmountTo >= debt));

            //If there is one matched profile
            if (sugguestedProfiles.Count() == 1)
            {
                return sugguestedProfiles.LastOrDefault().Id;
            }

            //If there is more than one matched profile
            if (sugguestedProfiles.Count() > 1)
            {
                return sugguestedProfiles.OrderByDescending(x => x.DebtAmountFrom).First().Id;
            }

            //No profile matched.
            return -1;
        }

        //Import to receivable to Database
        //===============================
        private IEnumerable<Receivable> TransformReceivablesToDBM(IEnumerable<ReceivableIM> receivableIMs)
        {
            if (receivableIMs.Any())
            {
                var result = new List<Receivable>();
                foreach (var receivable in receivableIMs)
                {
                    var contacts = TransformContactToDBM(receivable.Contacts);
                    if (!contacts.Any())
                    {
                        return null;
                    }
                    //End if !contacts.Any()

                    var assignedCollectors = new List<AssignedCollector>();
                    CollectionProgress collectionProgress = null;
                    DateTime? endDay = null;

                    //Receivable is assigned to Collector
                    if (receivable.CollectorId != null)
                    {
                        //Generate assigned collector
                        var assignedCollector = TransformAssignedCollectorToDBM(receivable.CollectorId);
                        if (assignedCollector == null)
                        {
                            return null;
                        }
                        //End if !assignedCollector.Any()
                        assignedCollectors.Add(assignedCollector);

                        //Generate collectionProgress with full stage and action.
                        collectionProgress = TransformCollectionProgressToDBM(receivable.Profile, (int)receivable.PayableDay, receivable.DebtAmount, GetDebtorName(receivable.Contacts), receivable.ProfileId);
                        if (collectionProgress == null)
                        {
                            return null;
                        }

                        endDay = CalculateEndDay(collectionProgress.ProgressStages, (int)receivable.PayableDay);
                        //End if !collectionProgress.Any()
                    }
                    else
                    //Receivable is not assigned to Collector
                    {
                        collectionProgress = TransfromCollectionProgressToDBMWithoutStage(receivable.ProfileId);
                        if (collectionProgress == null)
                        {
                            return null;
                        }
                    }
                    //End if receivable.CollectorId != null

                    var receivableDBM = new Receivable()
                    {
                        AssignedCollectors = assignedCollectors,
                        CollectionProgress = collectionProgress,
                        Contacts = contacts.ToList(),
                        CreatedDate = DateTime.Now,
                        CustomerId = receivable.CustomerId,
                        DebtAmount = receivable.DebtAmount,
                        IsDeleted = false,
                        PrepaidAmount = receivable.PrepaidAmount,
                        PayableDay = receivable.PayableDay,
                        ExpectationClosedDay = endDay
                    };
                    result.Add(receivableDBM);
                }

                if (result.Any())
                {
                    return result;
                }
                //End if result.Any()
            }
            return null;
        }

        private IEnumerable<Contact> TransformContactToDBM(IEnumerable<ContactIM> contacts)
        {
            if (contacts.Any())
            {
                var result = contacts.Select(x => new Contact()
                {
                    Name = x.Name,
                    IdNo = x.IdNo,
                    Address = x.Address,
                    Phone = x.Phone,
                    Type = x.Type,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now
                });
                return result;

            }
            return null;
        }

        private string GetDebtorName(IEnumerable<ContactIM> contacts)
        {
            var result = contacts.Where(x => x.Type == Constant.CONTACT_DEBTOR_CODE).FirstOrDefault().Name;
            return result;
        }

        private AssignedCollector TransformAssignedCollectorToDBM(string collectorId)
        {
            if (collectorId != null)
            {
                var assignedCollector = new AssignedCollector()
                {
                    UserId = collectorId,
                    Status = Constant.ASSIGNED_STATUS_ACTIVE_CODE,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                };
                return assignedCollector;
            }
            //End if collectorId != null
            return null;
        }

        private CollectionProgress TransformCollectionProgressToDBM(ProfileIM profile, int payableDay, long debtAmount, string debtorName, int profileId)
        {
            if (debtorName != null)
            {
                if (profile != null)
                {
                    var stages = TransformProgressStageToDBM(profile.Stages, payableDay, debtorName, debtAmount);
                    if (stages.Any())
                    {
                        var collectionProgress = new CollectionProgress()
                        {
                            ProfileId = profileId,
                            Status = Constant.COLLECTION_STATUS_COLLECTION_CODE,
                            IsDeleted = false,
                            CreatedDate = DateTime.Now,
                            ProgressStages = stages.ToList()
                        };
                        return collectionProgress;
                    }
                    // End if stages.Any()
                }
                //end if Profile != null
            }
            //end if receivable != null
            return null;
        }

        private CollectionProgress TransfromCollectionProgressToDBMWithoutStage(int profileId)
        {
            var profile = GetProfileFromDB(profileId);
            if (profile != null)
            {
                var collectionProgress = new CollectionProgress()
                {
                    Profile = profile,
                    Status = Constant.COLLECTION_STATUS_WAIT_CODE,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    ProgressStages = null
                };
                return collectionProgress;
            }
            return null;
        }

        private IEnumerable<ProgressStage> TransformProgressStageToDBM(IEnumerable<ProfileStageVM> profileStages, int payableDay, string debtorName, long debtAmount)
        {
            if (profileStages.Any())
            {
                var result = profileStages.Select(x => new ProgressStage()
                {
                    Duration = x.Duration,
                    Name = x.Name,
                    Sequence = x.Sequence,
                    Status = Constant.COLLECTION_STATUS_COLLECTION_CODE,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    ProgressStageAction = TransformProgressStageActionToDBM(x.Actions, x.Duration, GetStageStartDay(payableDay, x.Sequence, profileStages), debtorName, debtAmount).ToList()
                });
                return result.ToList();
            }
            //end if collectionProgress != null && profileStages.Any()
            return null;
        }

        private DateTime GetStageStartDay(int payableDay, int sequence, IEnumerable<ProfileStageVM> profileStages)
        {
            int day = 0;
            for (int i = 0; i < sequence - 1; i++)
            {
                day += profileStages.ElementAt(i).Duration;
            }

            DateTime startDate = Utility.ConvertIntToDatetime(payableDay);
            startDate = startDate.AddDays(day);

            return startDate;
        }

        private IEnumerable<ProgressStageAction> TransformProgressStageActionToDBM(IEnumerable<ProfileStageActionVM> profileStageActions, int stageDuration, DateTime stageStartDate, string debtorName, long debtAmount)
        {
            List<ProgressStageAction> result = new List<ProgressStageAction>();
            if (profileStageActions.Any())
            {
                foreach (var action in profileStageActions)
                {
                    var tmp = SplitProfileStageAction(action, stageDuration, stageStartDate, debtorName, debtAmount);
                    if (tmp.Any())
                    {
                        foreach (var item in tmp)
                        {
                            result.Add(item);
                        }
                    }
                }

            }
            //End if profileStageActions.Any()
            return result;
        }

        //Change 2 paramater [NAME] and [AMOUNT] to curernt context
        private string FillData(string content, string debtorName, long debtAmount)
        {
            StringBuilder builder = new StringBuilder(content.Trim());
            builder.Replace(Constant.MESSAGE_PARAMETER_NAME, debtorName);
            builder.Replace(Constant.MESSAGE_PARAMETER_DEBTAMOUNT, debtAmount.ToString());

            return builder.ToString();
        }

        private ProgressMessageForm TransformProfileMessageFormToDBM(int? profileMessageFormId, string debtorName, long debtAmount)
        {
            if (profileMessageFormId != null)
            {
                var profileMessageForm = GetProfileMessageFormFromDB((int)profileMessageFormId);
                if (profileMessageForm != null)
                {
                    var progressMessageForm = new ProgressMessageForm()
                    {
                        Content = FillData(profileMessageForm.Content, debtorName, debtAmount),
                        Type = profileMessageForm.Type,
                        Name = profileMessageForm.Name
                    };
                    return progressMessageForm;
                }
            }
            return null;
        }

        private IEnumerable<ProgressStageAction> SplitProfileStageAction(ProfileStageActionVM profileStageAction, int stageDuration, DateTime startDate, string debtorName, long debtAmount)
        {
            if (profileStageAction != null)
            {
                var result = new List<ProgressStageAction>();

                //Get how many times action will be executed.
                var Frequency = stageDuration / profileStageAction.Frequency;

                //Get profile message form
                var progressMessageForm = TransformProfileMessageFormToDBM(profileStageAction.ProfileMessageFormId, debtorName, debtAmount);

                for (int i = 0; i < Frequency; i++)
                {
                    //Calculate execution date.
                    DateTime newDate = startDate.AddDays(profileStageAction.Frequency);
                    startDate = newDate;

                    var progressStageAction = new ProgressStageAction()
                    {
                        Name = profileStageAction.Name,
                        Type = profileStageAction.Type,
                        ProgressMessageForm = progressMessageForm,
                        ExcutionDay = Int32.Parse(Utility.ConvertDatetimeToString(startDate)),
                        StartTime = profileStageAction.StartTime,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now,
                        Status = Constant.COLLECTION_STATUS_COLLECTION_CODE
                    };

                    //Add action to result list.
                    result.Add(progressStageAction);
                }
                //End for
                if (result.Any())
                {
                    return result;
                }
                //End if result.Any()
            }
            //End if profileStageAction != null
            return null;
        }

        private long GetTotalProgressDay(Receivable receivable)
        {
            long result = 0;
            foreach (var stage in receivable.CollectionProgress.ProgressStages)
            {
                result += stage.Duration;
            }
            return result;
        }

        private int GetProgressReached(Receivable receivable)
        {

            //Receivable is not assigned.
            if (receivable.PayableDay == null)
            {
                return 0;
            }

            if (DateTime.Today < Utility.ConvertIntToDatetime((int)receivable.PayableDay))
            {
                return 0;
            }

            if (DateTime.Today == receivable.ExpectationClosedDay.Value.Date || receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_DONE_CODE)
            {
                return 100;
            }

            double result = 0;
            long totalDayInMiliSecond = GetTotalProgressDay(receivable) * 24 * 60 * 60 * 1000;

            //Receivable is closed.
            if (receivable.ClosedDay != null)
            {
                result = ((Utility.ConvertIntToDatetime((int)receivable.ClosedDay) - Utility.ConvertIntToDatetime((int)receivable.PayableDay)).TotalMilliseconds);
                result = (int)((double)result * 100 / totalDayInMiliSecond);
                return (int)result;
            }

            //Receivable is in collection progress.
            TimeSpan tmp = DateTime.Now - Utility.ConvertIntToDatetime((int)receivable.PayableDay);
            double tmpResult = tmp.TotalMilliseconds;
            result = (int)(tmpResult * 100 / totalDayInMiliSecond);

            return (int)result;
        }

        private bool HaveLateActions(Receivable receivable)
        {
            if (receivable.PayableDay == null)
            {
                return false;
            }

            foreach (var stage in receivable.CollectionProgress.ProgressStages)
            {
                foreach (var action in stage.ProgressStageAction)
                {
                    if (action.Status == Constant.COLLECTION_STATUS_LATE_CODE)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private DateTime? CalculateEndDay(IEnumerable<ProgressStage> stages, int payableDay)
        {
            DateTime? result = null;
            if (stages.Any())
            {
                int totalDay = stages.Sum(x => x.Duration);
                DateTime payable = Utility.ConvertIntToDatetime(payableDay);
                result = payable.AddDays(totalDay);
            }

            return result;
        }
    }
}
