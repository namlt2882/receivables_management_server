using Microsoft.AspNetCore.Mvc;
using RCM.Model;
using RCM.Service;
using RCM.ViewModels;
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
        public IActionResult GetProfile(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id cannot be empty");
            }

            //Get raw profile from DB, doesnt include Stages and Action.
            var rawProfile = _profileService.GetProfile((int)id);

            //Get Profile including Stages and Actions.
            var profile = GetProfileForViewModel(rawProfile);

            if (profile == null)
            {
                return BadRequest("Not found");
            }

            return Ok(profile);
        }

        private ProfileVM GetProfileForViewModel(Profile rawProfile)
        {

            ProfileVM profile = new ProfileVM()
            {
                Id = rawProfile.Id,
                DebtAmountFrom = rawProfile.DebtAmountFrom,
                DebtAmountTo = rawProfile.DebtAmountTo,
                Name = rawProfile.Name,
                Stages = GetProfileStageForViewModel(rawProfile.Id)
            };
            return profile;
        }

        private IEnumerable<ProfileStageVM> GetProfileStageForViewModel(int profileId)
        {
            //Get raw data from DB
            var stagesFromDB = _profileStageService.GetProfileStages().Where(x => x.ProfileId == profileId);

            //Tranform to view model
            IEnumerable<ProfileStageVM> result = stagesFromDB
                .Select(x => new ProfileStageVM()
                { Id = x.Id, Name = x.Name, Duration = x.Duration, Sequence = x.Sequence, Actions = GetProfileStageActionsForViewModel(x.Id) }
                );

            return result;
        }

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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetProfiles()
        {
            var profiles = _profileService.GetProfiles();
            return Ok(profiles);
        }

        [HttpPost]
        public IActionResult Create([FromBody]ProfileIM profileVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Add Profile to DB
            var profile = new Profile()
            {
                Name = profileVM.Name,
                DebtAmountFrom = profileVM.DebtAmountFrom,
                DebtAmountTo = profileVM.DebtAmountTo,
            };

            _profileService.CreateProfile(profile);
            _profileService.SaveProfile();

            //Add Profile Stage to corresponding Profile.
            int profileId = _profileService.GetProfiles().LastOrDefault().Id;
            IEnumerable<ProfileStageVM> Stages = profileVM.Stages;
            AddStage(profileId, Stages);


            return Ok();
        }

        private void AddStage(int profileId, IEnumerable<ProfileStageIM> stages)
        {
            foreach (var stage in stages)
            {
                ProfileStage _stage = new ProfileStage(profileId, stage.Name, stage.Duration, stage.Sequence);
                _profileStageService.CreateProfileStage(_stage);
                _profileStageService.SaveProfileStage();


                //Add Action to corresponding Stage.
                int stageId = _profileStageService.GetProfileStages().LastOrDefault().Id;
                AddAction(stageId, stage.Actions);
            }
        }

        private void AddAction(int stageId, IEnumerable<ProfileStageActionIM> actions)
        {
            foreach (var action in actions)
            {
                ProfileStageAction _action = new ProfileStageAction(stageId, action.Name, action.Frequency, action.StartTime, action.Type, action.ProfileMessageFormId);
                _profileStageActionService.CreateProfileStageAction(_action);
                _profileStageActionService.SaveProfileStageAction();
            }
        }
    }
}
