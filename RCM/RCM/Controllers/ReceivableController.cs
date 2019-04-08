using Mapster;
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
        private readonly IProfileService _profileService;
        private readonly INotificationService _notificationService;
        private readonly IAssignedCollectorService _assignedCollectorService;
        private readonly IProfileMessageFormService _profileMessageFormService;
        private readonly UserManager<User> _userManager;

        public ReceivableController(IHubContext<CenterHub> hubContext, IHubUserConnectionService hubService, IFirebaseTokenService firebaseTokenService, IReceivableService receivableService, IProfileService profileService, INotificationService notificationService, IAssignedCollectorService assignedCollectorService, IProfileMessageFormService profileMessageFormService, UserManager<User> userManager)
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
        }

        [HttpGet]
        public IActionResult GetAll()
        {
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
                DebtorName = x.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).SingleOrDefault().Name,
                DebtorId = x.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).SingleOrDefault().Id,
                ProgressPercent = GetProgressReached(x),
                HaveLateAction = HaveLateActions(x),
                IsConfirmed = x.IsConfirmed
            });
            return Ok(result);
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
                    if (receivable.CollectionProgress.Status != Constant.COLLECTION_STATUS_COLLECTION_CODE)
                    {
                        result.Add(ParseReceivableMobile(receivable));
                    }
                }
                else
                {
                    if (receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_COLLECTION_CODE)
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
            var receivable = model.Adapt<ReceivableMobileLM>();
            receivable.CustomerName = model.Customer.Name;
            receivable.DebtorId = model.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE && !contact.IsDeleted).SingleOrDefault().Id;
            receivable.DebtorName = model.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE && !contact.IsDeleted).SingleOrDefault().Name;
            receivable.Contacts = GetReceivableContactsForDetailView(model.Contacts.Where(con => !con.IsDeleted));
            receivable.CollectionProgressStatus = model.CollectionProgress.Status;
            receivable.IsConfirmed = model.IsConfirmed;
            receivable.AssignDate = Utility.ConvertDatimeToInt(model.AssignedCollectors.SingleOrDefault(ac => ac.ReceivableId == receivable.Id && ac.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE && !ac.IsDeleted).CreatedDate);
            return receivable;
        }
        #endregion


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

        [HttpGet("GetReceivaleByCollectorId")]
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
                DebtorName = x.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).SingleOrDefault().Name,
                DebtorId = x.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).SingleOrDefault().Id,
                ProgressPercent = GetProgressReached(x),
                HaveLateAction = HaveLateActions(x),
                IsConfirmed = x.IsConfirmed
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
            }
            else
            {
                receivable.CollectionProgress.Status = Constant.COLLECTION_STATUS_CANCEL_CODE;
            }

            if (receivable == null)
            {
                return NotFound();
            }

            _receivableService.CloseReceivable(receivable);
            _receivableService.SaveReceivable();
            await SendManagerConfirmReceivableNotification(receivable);

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
                _receivableService.CloseReceivable(receivable);
                _receivableService.SaveReceivable();
            }
            else
            {
                return BadRequest(new { Message = "Receivable is not closed or canceled so cannot be confirmed." });
            }

            return Ok(new { ClosedTime = receivable.ClosedDay, DebtAmount = receivable.DebtAmount, Status = receivable.CollectionProgress.Status, IsConfirmed = receivable.IsConfirmed });
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
        public IActionResult AddReceivables([FromBody] IEnumerable<ReceivableIM> receivableIMs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var receivablesDBM = TransformReceivablesToDBM(receivableIMs);
            if (receivablesDBM.Any())
            {
                foreach (var receivableDBM in receivablesDBM)
                {
                    _receivableService.CreateReceivable(receivableDBM);
                    _receivableService.SaveReceivable();
                }

                var importedReceivables = _receivableService.GetReceivables().OrderByDescending(x => x.Id).Take(receivablesDBM.Count());
                SendNewReceivableNotification(importedReceivables.ToList());

                return Ok(importedReceivables);
            }

            return BadRequest(new { Message = "Error when trying to import" });
        }

        private void SendNewReceivableNotification(List<Receivable> receivables)
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
            userNotifications.ForEach(_ =>
            {
                notifications.Add(new Notification()
                {
                    Title = Constant.NOTIFICATION_TYPE_NEW_RECEIVABLE,
                    Type = Constant.NOTIFICATION_TYPE_NEW_RECEIVABLE_CODE,
                    Body = $"You were assgined to {_.ReceivableList.Count}{JsonConvert.SerializeObject(_.ReceivableList)} reiceivable",
                    UserId = _.UserId,
                    NData = JsonConvert.SerializeObject(_.ReceivableList),
                    IsSeen = false,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                });
            });
            _notificationService.CreateNotification(notifications);
            _notificationService.SaveNotification();
            #endregion
            //Send
            SendNotificationToClient(notifications);
        }

        private async Task SendManagerConfirmReceivableNotification(Receivable receivable)
        {
            #region Create New Receivable Notification
            var user = await _userManager.FindByNameAsync("manager");
            //Create New Receivable Notification
            Notification notification = new Notification()
            {
                Title = Constant.NOTIFICATION_TYPE_CLOSE_RECEIVABLE,
                Type = Constant.NOTIFICATION_TYPE_CLOSE_RECEIVABLE_CODE,
                Body = $"You have reiceivable-{receivable.Id} need to confirm!",
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
        private void SendNotificationToClient(List<Notification> notifications)
        {
            NotificationUtility.NotificationUtility.SendNotificationToCurrentMobileClient(notifications, _firebaseTokenService);
            NotificationUtility.NotificationUtility.SendNotificationToCurrentWebClient(notifications, _hubService, _hubContext);
        }

        private async Task SendNotificationToClient(Notification notification)
        {
            NotificationUtility.NotificationUtility.SendNotificationToCurrentMobileClient(notification, _firebaseTokenService);
            await NotificationUtility.NotificationUtility.SendNotificationToCurrentWebClient(notification, _hubService, _hubContext);
        }

        [HttpPost("ChangeAsignedCollector")]
        public IActionResult ChangeAssignedCollector([FromBody]AssignedCollectorUM assignedCollectorUM)
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

            return Ok();
        }

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
        public IActionResult AssignReceivable([FromBody] IEnumerable<ReceivableAssignModel> receivableAMs)
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
                    if (receivable.CollectionProgress.Status == Constant.COLLECTION_STATUS_WAIT_CODE)
                    {
                        receivable.CollectionProgress.Status = Constant.COLLECTION_STATUS_COLLECTION_CODE;
                        receivable.PayableDay = receivableAM.PayableDay;
                        receivable.AssignedCollectors.Add(TransformAssignedCollectorToDBM(receivableAM.CollectorId));
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
                            if (stages.Any())
                            {
                                receivable.CollectionProgress.ProgressStages = stages.ToList();
                            }

                            _receivableService.EditReceivable(receivable);
                            _receivableService.SaveReceivable();
                            result.Add(GetReceivable(receivable.Id));
                        }
                        //End if contacts.Any()
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
                DebtorName = x.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).SingleOrDefault().Name,
                DebtorId = x.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).SingleOrDefault().Id,
                ProgressPercent = GetProgressReached(x),
                HaveLateAction = HaveLateActions(x),
                IsConfirmed = x.IsConfirmed
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
                ProgressMessageFormId = x.ProgressMessageFormId
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
            result = (int)((DateTime.Now - Utility.ConvertIntToDatetime((int)receivable.PayableDay)).TotalMilliseconds);
            result = (int)((double)result * 100 / totalDayInMiliSecond);

            return (int) result;
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
