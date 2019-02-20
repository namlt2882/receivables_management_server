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

    public class ReceivableController : ControllerBase
    {
        private readonly IReceivableService _receivableService;
        private readonly IContactService _contactService;

        private readonly ICollectionProgressService _collectionProgressService;
        private readonly IProgressStageService _progressStageService;
        private readonly IProgressStageActionService _progressStageActionService;
        private readonly IProgressMessageFormService _progressMessageFormService;

        private readonly IProfileService _profileService;
        private readonly IProfileStageService _profileStageService;
        private readonly IProfileStageActionService _profileStageActionService;
        private readonly IProfileMessageFormService _profileMessageFormService;

        private readonly IAssignedCollectorService _assignedCollectorService;

        public ReceivableController(IReceivableService receivableService, IContactService contactService,
            ICollectionProgressService collectionProgressService, IProgressStageService progressStageService, IProgressStageActionService progressStageActionService, IProgressMessageFormService progressMessageFormService,
            IProfileService profileService, IProfileStageService profileStageService, IProfileStageActionService profileStageActionService, IProfileMessageFormService profileMessageFormService,
            IAssignedCollectorService assignedCollectorService)
        {
            _receivableService = receivableService;
            _contactService = contactService;

            _collectionProgressService = collectionProgressService;
            _progressStageService = progressStageService;
            _progressStageActionService = progressStageActionService;
            _progressMessageFormService = progressMessageFormService;

            _profileService = profileService;
            _profileStageService = profileStageService;
            _profileStageActionService = profileStageActionService;
            _profileMessageFormService = profileMessageFormService;

            _assignedCollectorService = assignedCollectorService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_receivableService.GetReceivables());
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
        public IActionResult ValidateReceivables([FromBody] IEnumerable<ReceivableIM> receivableIMs)
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

            ImportReceivablesToDB(receivableIMs);
            return Ok(receivableIMs);
        }

        //Get receivable from DB and transform to VM.
        private ReceivableVM GetReceivable(int id)
        {
            var receivableDBM = _receivableService.GetReceivable(id);
            if (receivableDBM != null)
            {
                var contacts = GetReceivableContacts(receivableDBM.Id);
                if (contacts != null)
                {
                    var receivableVM = new ReceivableVM()
                    {
                        Id = receivableDBM.Id,
                        DebtAmount = receivableDBM.DebtAmount,
                        PayableDay = receivableDBM.PayableDay,
                        PrepaidAmount = receivableDBM.PrepaidAmount,
                        CustomerId = receivableDBM.CustomerId,
                        LocationId = receivableDBM.LocationId,
                        ClosedDay = receivableDBM.ClosedDay,
                        Contacts = contacts
                    };
                    return receivableVM;
                }

            }
            return null;
        }

        //Get contacts from DB and transform to VM.
        private IEnumerable<ContactVM> GetReceivableContacts(int receivableId)
        {
            var contactsDBM = _contactService.GetContacts().Where(x => x.Id == receivableId);
            if (contactsDBM != null)
            {
                IEnumerable<ContactVM> result = contactsDBM
                    .Select(x => new ContactVM()
                    {
                        Id = x.Id,
                        Address = x.Address,
                        IdNo = x.IdNo,
                        Name = x.Name,
                        Phone = x.Phone,
                        Type = x.Type
                    });

                return result;
            }
            return null;
        }

        //Transform to DBM and import.
        private void ImportReceivablesToDB(IEnumerable<ReceivableIM> receivableIMs)
        {
            foreach (var item in receivableIMs)
            {
                var receivable = new Receivable()
                {
                    DebtAmount = item.DebtAmount,
                    PrepaidAmount = item.PrepaidAmount,
                    CustomerId = item.CustomerId,
                    PayableDay = item.PayableDay
                };

                int receivableId = ImportReceivableToDB(receivable);
                ImportContactsToDB(receivableId, item.Contacts);
                ImportCollectionProgress(receivableId, item.ProfileId, item.PayableDay);
                ImportAssginedCollector(receivableId, item.CollectorId);
            }
        }

        //Create Assigned Collector.
        private void ImportAssginedCollector(int receivableId, string collectorId)
        {
            var assignedCollector = new AssignedCollector()
            {
                UserId = collectorId,
                ReceivableId = receivableId
            };
            _assignedCollectorService.CreateAssignedCollector(assignedCollector);
            _assignedCollectorService.SaveAssignedCollector();
        }

        //Create collection progress
        private void ImportCollectionProgress(int receivableId, int profileId, int payableDay)
        {
            var Profile = GetProfileFromDB(profileId);
            if (Profile != null)
            {
                // Create collection Progress
                var CollectionProgress = new CollectionProgress()
                {
                    ReceivableId = receivableId,
                    ProfileId = profileId
                };
                _collectionProgressService.CreateCollectionProgress(CollectionProgress);
                _collectionProgressService.SaveCollectionProgress();

                //Copy Stage from Profile to Progress
                int collectionId = _collectionProgressService.GetCollectionProgresss().LastOrDefault().Id;
                var profileStages = GetProfileStagesFromDB(profileId);
                if (profileStages != null)
                {
                    ImportProgressStagesToDB(profileStages, collectionId, payableDay);
                }
                //End if Profilestages != null
            }
            else
            {
                return;
            }
            //end if Profile != null
        }

        //Create progress stages
        private void ImportProgressStagesToDB(IEnumerable<ProfileStage> profileStages, int collectionId, int payableDay)
        {
            if (profileStages != null)
            {
                foreach (var stage in profileStages)
                {
                    var progressStage = new ProgressStage()
                    {
                        Duration = stage.Duration,
                        Name = stage.Name,
                        Sequence = stage.Sequence,
                        CollectionProgressId = collectionId
                    };
                    _progressStageService.CreateProgressStage(progressStage);
                    _progressStageService.SaveProgressStage();

                    //Copy Action from Profile to Progress
                    int progressStageId = _progressStageService.GetProgressStages().LastOrDefault().Id;
                    var profileStageActions = GetProfileStageActionFromDB(stage.Id);
                    if (profileStageActions != null)
                    {
                        ImportProgressMessageToDB(profileStageActions, stage.Duration, payableDay, progressStageId);
                    }
                    //End if profileStageActions != null
                }
                //End for each
            }
            //End if profileStages != null
        }

        //Create stage actions
        private void ImportProgressMessageToDB(IEnumerable<ProfileStageAction> profileStageActions, int stageDuration, int payableDay, int stageId)
        {
            if (profileStageActions != null)
            {
                foreach (var profileAction in profileStageActions)
                {
                    int? profileMessageFormId = profileAction.ProfileMessageFormId;
                    if (profileMessageFormId != null)
                    {
                        //First, create message form.
                        var profileMessageForm = GetProfileMessageFormFromDB((int)profileMessageFormId);
                        if (profileMessageForm != null)
                        {
                            var progressMessageForm = new ProgressMessageForm()
                            {
                                Content = profileMessageForm.Content,
                                Type = profileMessageForm.Type,
                                Name = profileMessageForm.Name
                            };
                            _progressMessageFormService.CreateProgressMessageForm(progressMessageForm);
                            _progressMessageFormService.SaveProgressMessageForm();

                            //Second, create action.
                            int messageId = _progressMessageFormService.GetProgressMessageForms().LastOrDefault().Id;
                            SplitProfileStageAction(profileAction, stageDuration, payableDay, messageId, stageId);

                        }
                        //End if profilesMessageForm != null
                    }
                    //End if profileMessageFormId != null
                }
                //End for each
            }
            //End if profileStageActions != null
        }

        private void SplitProfileStageAction(ProfileStageAction profileStageAction, int stageDuration, int payableDay, int messageId, int stageId)
        {
            IEnumerable<ProgressStageAction> result = new List<ProgressStageAction>();

            //Get how many times action will be executed.
            var Frequency =  stageDuration / profileStageAction.Frequency ;

            //Convert data from DB to Datetime
            DateTime startDate = Utility.ConvertIntToDatetime(payableDay);

            for (int i = 0; i < Frequency; i++)
            {
                //Calculate execution date.
                DateTime newDate = startDate.AddDays(profileStageAction.Frequency);
                startDate = newDate;

                var progressStageAction = new ProgressStageAction()
                {
                    Name = profileStageAction.Name,
                    Type = profileStageAction.Type,
                    ProgressMessageFormId = messageId,
                    ProgressStageId = stageId,
                    ExcutionDay = Int32.Parse(Utility.ConvertDatetimeToString(startDate)),
                    StartTime = profileStageAction.StartTime,
                };

                //Add action to result list.
                _progressStageActionService.CreateProgressStageAction(progressStageAction);
                _progressStageActionService.SaveProgressStageAction();
            }


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

        private IEnumerable<ProfileStage> GetProfileStagesFromDB(int profileId)
        {
            var stages = _profileStageService.GetProfileStages().Where(x => (x.ProfileId == profileId && x.IsDeleted == false));
            if (stages != null)
            {
                return stages;
            }
            return null;
        }

        private IEnumerable<ProfileStageAction> GetProfileStageActionFromDB(int stageId)
        {
            var actions = _profileStageActionService.GetProfileStageActions().Where(x => (x.ProfileStageId == stageId && x.IsDeleted == false));
            if (actions != null)
            {
                return actions;
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

        //Import receivable to DB.
        private int ImportReceivableToDB(Receivable receivable)
        {
            _receivableService.CreateReceivable(receivable);
            _receivableService.SaveReceivable();

            var Id = _receivableService.GetReceivables().LastOrDefault().Id;

            return Id;
        }

        //Transform to DBM and import.
        private void ImportContactsToDB(int receivableId, IEnumerable<ContactIM> contacts)
        {
            foreach (var contactIM in contacts)
            {
                var contact = new Contact()
                {
                    Name = contactIM.Name,
                    IdNo = contactIM.IdNo,
                    Address = contactIM.Address,
                    Phone = contactIM.Phone,
                    Type = contactIM.Type,
                    ReceivableId = receivableId
                };

                ImportContactToDB(contact);

            }
        }

        //Import contact to DB.
        private void ImportContactToDB(Contact contact)
        {
            _contactService.CreateContact(contact);
            _contactService.SaveContact();
        }

        //Validate input receivable.
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

    }
}
