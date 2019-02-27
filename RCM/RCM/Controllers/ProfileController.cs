using Microsoft.AspNetCore.Mvc;
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
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IProfileStageService _profileStageService;
        private readonly IProfileStageActionService _profileStageActionService;

        public ProfileController(IProfileService profileService, IProfileStageService profileStageService, IProfileStageActionService profileStageActionService)
        {
            _profileService = profileService;
            _profileStageActionService = profileStageActionService;
            _profileStageService = profileStageService;
        }

        [HttpGet("{id}")]
        public IActionResult GetProfile(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profile = GetProfileDetail(id);
            if (profile == null)
            {
                return NotFound();
            }

            return Ok(profile);
        }

        [HttpPut("Disable")]
        public IActionResult DisableProfile(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profile = _profileService.GetProfile(id);
            if (profile == null)
            {
                return NotFound();
            }

            profile.IsDeleted = true;
            _profileService.EditProfile(profile);
            _profileService.SaveProfile();

            return Ok(profile);
        }

        [HttpPut("Enable")]
        public IActionResult EnableProfile(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profile = _profileService.GetProfile(id);
            if (profile == null)
            {
                return NotFound();
            }

            profile.IsDeleted = false;
            _profileService.EditProfile(profile);
            _profileService.SaveProfile();

            return Ok(profile);
        }

        [HttpPut]
        public IActionResult UpdateProfile([FromBody] ProfileUM profileUM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profile = new Profile()
            {
                Id = profileUM.Id,
                Name = profileUM.Name,
                DebtAmountFrom = profileUM.DebtAmountFrom,
                DebtAmountTo = profileUM.DebtAmountTo
            };
            _profileService.EditProfile(profile);
            _profileService.SaveProfile();

            return Ok(profile);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var profiles = GetProfiles();
            return Ok(profiles);
        }

        private IEnumerable<ProfileUM> GetProfiles()
        {
            var profiles = _profileService.GetProfiles();
            if (profiles.Any())
            {
                var result = profiles.Select(x => new ProfileUM()
                {
                    Id = x.Id,
                    DebtAmountFrom = x.DebtAmountFrom,
                    DebtAmountTo = x.DebtAmountTo,
                    Name = x.Name
                });
                return result;
            }
            return null;
        }

        //[HttpPost]
        //public IActionResult Create([FromBody]ProfileIM profileVM)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    //Add Profile to DB
        //    var profile = new Profile()
        //    {
        //        Name = profileVM.Name,
        //        DebtAmountFrom = profileVM.DebtAmountFrom,
        //        DebtAmountTo = profileVM.DebtAmountTo,

        //    };

        //    _profileService.CreateProfile(profile);
        //    _profileService.SaveProfile();

        //    //Add Profile Stage to corresponding Profile.
        //    int profileId = _profileService.GetProfiles().LastOrDefault().Id;
        //    IEnumerable<ProfileStageVM> Stages = profileVM.Stages;
        //    ImportStageToDB(profileId, Stages);


        //    return Ok(profileId);
        //}

        [HttpPost]
        public IActionResult Create([FromBody]ProfileIM profileVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Add Profile to DB
            var stages = TransformStagesToDBM(profileVM.Stages).ToList();
            if (stages != null)
            {
                var profile = new Profile()
                {
                    Name = profileVM.Name,
                    DebtAmountFrom = profileVM.DebtAmountFrom,
                    DebtAmountTo = profileVM.DebtAmountTo,
                    ProfileStages = stages.ToList()
                };

                _profileService.CreateProfile(profile);
                _profileService.SaveProfile();

                return Ok(profile);
            }


            return BadRequest();
        }

        //Get profile from DB and transform to VM.
        private ProfileVM GetProfileDetail(int id)
        {
            var profileDBM = _profileService.GetProfile(id);
            if (profileDBM != null)
            {
                var stagesVM = GetProfileStageForViewModel(profileDBM.Id);
                if (stagesVM != null)
                {
                    ProfileVM profile = new ProfileVM()
                    {
                        Id = profileDBM.Id,
                        DebtAmountFrom = profileDBM.DebtAmountFrom,
                        DebtAmountTo = profileDBM.DebtAmountTo,
                        Name = profileDBM.Name,
                        Stages = stagesVM
                    };
                    return profile;
                }

            }

            return null;
        }

        //Get profile stages from DB and transform to VM.
        private IEnumerable<ProfileStageVM> GetProfileStageForViewModel(int profileId)
        {
            //Get raw data from DB
            var stagesFromDB = _profileStageService.GetProfileStages().Where(x => x.ProfileId == profileId);

            //Tranform to view model
            IEnumerable<ProfileStageVM> result = stagesFromDB
                .Select(x => new ProfileStageVM()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Duration = x.Duration,
                    Sequence = x.Sequence,
                    Actions = GetProfileStageActionsForViewModel(x.Id)
                });

            return result;
        }

        //Get profile stage actions from DB and transform to VM
        private IEnumerable<ProfileStageActionVM> GetProfileStageActionsForViewModel(int stageId)
        {
            //Get raw data from DB
            var actionsFromDB = _profileStageActionService.GetProfileStageActions().Where(x => x.ProfileStageId == stageId);

            //Transform to view model
            IEnumerable<ProfileStageActionVM> result = actionsFromDB
                .Select(x => new ProfileStageActionVM()
                { Id = x.Id, Name = x.Name, Frequency = x.Frequency, ProfileMessageFormId = (int)x.ProfileMessageFormId, StartTime = x.StartTime, Type = x.Type }
                );

            return result;
        }

        //private void ImportStageToDB(int profileId, IEnumerable<ProfileStageIM> stages)
        //{
        //    foreach (var stage in stages)
        //    {
        //        ProfileStage _stage = new ProfileStage(profileId, stage.Name, stage.Duration, stage.Sequence);
        //        _profileStageService.CreateProfileStage(_stage);
        //        _profileStageService.SaveProfileStage();


        //        //Add Action to corresponding Stage.
        //        int stageId = _profileStageService.GetProfileStages().LastOrDefault().Id;
        //        ImportActionToDB(stageId, stage.Actions);
        //    }
        //}

        //private void ImportActionToDB(int stageId, IEnumerable<ProfileStageActionIM> actions)
        //{
        //    foreach (var action in actions)
        //    {
        //        ProfileStageAction _action = new ProfileStageAction(stageId, action.Name, action.Frequency, action.StartTime, action.Type, action.ProfileMessageFormId);
        //        _profileStageActionService.CreateProfileStageAction(_action);
        //        _profileStageActionService.SaveProfileStageAction();
        //    }
        //}

        private IEnumerable<ProfileStage> TransformStagesToDBM(IEnumerable<ProfileStageIM> stages)
        {
            if (stages != null)
            {
                var result = stages.Select(x => new ProfileStage()
                {
                    Name = x.Name,
                    Duration = x.Duration,
                    Sequence = x.Sequence,
                    ProfileStageActions = TransformActionToDBM(x.Actions).ToList(),
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                });
                return result;
            }
            return null;
        }

        private IEnumerable<ProfileStageAction> TransformActionToDBM(IEnumerable<ProfileStageActionVM> actions)
        {
            if (actions != null)
            {
                var result = actions.Select(x => new ProfileStageAction()
                {
                    Name = x.Name,
                    Frequency = x.Frequency,
                    StartTime = x.StartTime,
                    Type = x.Type,
                    ProfileMessageFormId = x.ProfileMessageFormId,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                });
                return result;
            }
            return null;
        }

    }
}
