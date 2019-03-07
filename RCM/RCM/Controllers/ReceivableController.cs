using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IReceivableService _receivableService;
        private readonly IProfileService _profileService;
        private readonly IProfileMessageFormService _profileMessageFormService;
        private readonly UserManager<User> _userManager;
        private readonly IAssignedCollectorService _assignedCollectorService;
        public ReceivableController(
            IReceivableService receivableService,
            IProfileService profileService,
            IProfileMessageFormService profileMessageFormService,
            UserManager<User> userManager,
            IAssignedCollectorService assignedCollectorService)
        {
            _receivableService = receivableService;
            _profileService = profileService;
            _profileMessageFormService = profileMessageFormService;
            _userManager = userManager;
            _assignedCollectorService = assignedCollectorService;
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
                PayableDay = x.PayableDay,
                PrepaidAmount = x.PrepaidAmount,
                CollectionProgressStatus = x.CollectionProgress.Status,
                CollectionProgressId = x.CollectionProgress.Id,
                AssignedCollectorId = x.AssignedCollectors.Where(assignedCollector => assignedCollector.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE).FirstOrDefault().UserId,
                CustomerName = x.Customer.Name,
                DebtorName = x.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).SingleOrDefault().Name,
                DebtorId = x.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).SingleOrDefault().Id,
            });
            return Ok(result);
        }
        [Authorize]
        [HttpGet("GetAssignedReceivable")]
        public async Task<IActionResult> GetAssignedReceivableAsync()
        {
            var result = new List<ReceivableLM>();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            _assignedCollectorService.GetAssignedCollectors(_ => _.UserId == user.Id && _.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE).ToList().ForEach(_ =>
            {
                result.Add(_receivableService.GetReceivable(_.ReceivableId).Adapt<ReceivableLM>());
            });
            return Ok(result);
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
                PayableDay = x.PayableDay,
                PrepaidAmount = x.PrepaidAmount,
                CollectionProgressStatus = x.CollectionProgress.Status,
                CollectionProgressId = x.CollectionProgress.Id,
                AssignedCollectorId = x.AssignedCollectors.Where(assignedCollector => assignedCollector.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE).FirstOrDefault().UserId,
                CustomerName = x.Customer.Name,
                DebtorName = x.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).SingleOrDefault().Name,
                DebtorId = x.Contacts.Where(contact => contact.Type == Constant.CONTACT_DEBTOR_CODE).SingleOrDefault().Id,
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
        public IActionResult CloseReceivable([FromBody] ReceivableCloseModel receivableCM)
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

            return Ok(new { ClosedTime = receivable.ClosedDay, DebtAmount = receivable.DebtAmount, Status = receivable.CollectionProgress.Status });
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
                return Ok(importedReceivables);
            }

            return BadRequest(new { Message = "Error when trying to import" });
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
        public IActionResult UpdateReceivable([FromBody] ReceivableUM receivableIM)
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
                if (receivable.DebtAmount == receivable.PrepaidAmount)
                {
                    _receivableService.CloseReceivable(receivable);
                }
                else
                {
                    _receivableService.EditReceivable(receivable);
                }
                _receivableService.SaveReceivable();
                return Ok();
            }
            return NotFound();
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
                    CustomerId = receivableDBM.CustomerId,
                    LocationId = receivableDBM.LocationId,
                    ClosedDay = receivableDBM.ClosedDay,
                    Contacts = GetReceivableContactsForDetailView(receivableDBM.Contacts),
                    CollectionProgress = GetCollectionProgressForDetailView(receivableDBM.CollectionProgress),
                    assignedCollector = GetAssignedCollectorForDetailView(receivableDBM.AssignedCollectors.Where(x => x.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE).FirstOrDefault())
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
                CollectorComment = x.CollectorComment,
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

                    var assignedCollector = TransformAssignedCollectorToDBM(receivable.CollectorId);
                    if (assignedCollector == null)
                    {
                        return null;
                    }
                    //End if !assignedCollector.Any()
                    var assignedCollectors = new List<AssignedCollector>();
                    assignedCollectors.Add(assignedCollector);

                    var collectionProgress = TransformCollectionProgressToDBM(receivable.ProfileId, (int)receivable.PayableDay, receivable.DebtAmount, GetDebtorName(receivable.Contacts));
                    if (collectionProgress == null)
                    {
                        return null;
                    }
                    //End if !collectionProgress.Any()

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

        private CollectionProgress TransformCollectionProgressToDBM(int profileId, int payableDay, long debtAmount, string debtorName)
        {
            if (debtorName != null)
            {
                var profile = GetProfileFromDB(profileId);
                if (profile != null)
                {
                    var stages = TransformProgressStageToDBM(profile.ProfileStages, payableDay, debtorName, debtAmount);
                    if (stages.Any())
                    {
                        var collectionProgress = new CollectionProgress()
                        {
                            Profile = profile,
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

        private IEnumerable<ProgressStage> TransformProgressStageToDBM(IEnumerable<ProfileStage> profileStages, int payableDay, string debtorName, long debtAmount)
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
                    ProgressStageAction = TransformProgressStageActionToDBM(x.ProfileStageActions, x.Duration, GetStageStartDay(payableDay, x.Sequence, profileStages), debtorName, debtAmount).ToList()
                });
                return result.ToList();
            }
            //end if collectionProgress != null && profileStages.Any()
            return null;
        }

        private DateTime GetStageStartDay(int payableDay, int sequence, IEnumerable<ProfileStage> profileStages)
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

        private IEnumerable<ProgressStageAction> TransformProgressStageActionToDBM(IEnumerable<ProfileStageAction> profileStageActions, int stageDuration, DateTime stageStartDate, string debtorName, long debtAmount)
        {
            if (profileStageActions.Any())
            {
                var result = new List<ProgressStageAction>();
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

                if (result.Any())
                {
                    return result;
                }
                //End if result.Any()
            }
            //End if profileStageActions.Any()
            return null;
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

        private IEnumerable<ProgressStageAction> SplitProfileStageAction(ProfileStageAction profileStageAction, int stageDuration, DateTime startDate, string debtorName, long debtAmount)
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

                    //Calculate execution date.
                    DateTime newDate = startDate.AddDays(profileStageAction.Frequency);
                    startDate = newDate;

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
    }
}
