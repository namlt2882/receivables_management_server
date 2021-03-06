﻿using Microsoft.AspNetCore.Mvc;
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
            var stages = TransformStagesToDBM(profileUM.Stages).ToList();
            var profile = new Profile()
            {
                Id = profileUM.Id,
                Name = profileUM.Name,
                DebtAmountFrom = profileUM.DebtAmountFrom,
                DebtAmountTo = profileUM.DebtAmountTo,
                ProfileStages = stages
            };
            _profileService.EditProfile(profile);
            _profileService.SaveProfile();

            return Ok(profile);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var profiles = GetProfiles();
            if (profiles.Any())
            {
                return Ok(profiles);
            }

            return Ok(new List<ProfileUM>());
        }

        [HttpGet("GetAllWithDetail")]
        public IActionResult GetAllWithDetail()
        {
            var profiles = GetAllProfilesWithDetail();
            if (profiles.Any())
            {
                return Ok(profiles);
            }

            return Ok(new List<ProfileVM>());
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
            return new List<ProfileUM>();
        }

        private IEnumerable<ProfileVM> GetAllProfilesWithDetail()
        {
            var profiles = _profileService.GetProfiles();
            if (profiles.Any())
            {
                var result = profiles.Select(x => GetProfileDetail(x));
                return result;
            }
            return new List<ProfileVM>();
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
            if (stages.Any())
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
            var originProfile = _profileService.GetProfile(id);
            return GetProfileDetail(originProfile);
        }

        private ProfileVM GetProfileDetail(Profile originProfile)
        {
            if (originProfile != null)
            {
                ProfileVM profile = new ProfileVM()
                {
                    Id = originProfile.Id,
                    DebtAmountFrom = originProfile.DebtAmountFrom,
                    DebtAmountTo = originProfile.DebtAmountTo,
                    Name = originProfile.Name,
                    Stages = originProfile.ProfileStages.Where(x => x.IsDeleted == false)
                    .Select(x => new ProfileStageVM()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Duration = x.Duration,
                        Sequence = x.Sequence,
                        Actions = x.ProfileStageActions.Where(a => a.IsDeleted == false)
                        .Select(a => new ProfileStageActionVM()
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Frequency = a.Frequency,
                            ProfileMessageFormId = a.ProfileMessageFormId,
                            StartTime = a.StartTime,
                            Type = a.Type
                        })
                    })
                };
                return profile;
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
                { Id = x.Id, Name = x.Name, Frequency = x.Frequency, ProfileMessageFormId = x.ProfileMessageFormId, StartTime = x.StartTime, Type = x.Type }
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

        private IEnumerable<ProfileStage> TransformStagesToDBM(IEnumerable<ProfileStageVM> stages)
        {
            if (stages.Any())
            {
                var result = stages.Select(x =>
                {
                    var stage = new ProfileStage()
                    {
                        Name = x.Name,
                        Duration = x.Duration,
                        Sequence = x.Sequence,
                        ProfileStageActions = TransformActionToDBM(x.Actions).ToList(),
                        CreatedDate = DateTime.Now,
                        IsDeleted = false
                    };
                    if (x.Id != null)
                    {
                        stage.Id = x.Id.Value;
                    }
                    return stage;
                });
                return result;
            }
            return new List<ProfileStage>();
        }

        private IEnumerable<ProfileStageAction> TransformActionToDBM(IEnumerable<ProfileStageActionVM> actions)
        {
            if (actions != null)
            {
                var result = actions.Select(x =>
                {
                    var action = new ProfileStageAction()
                    {
                        Name = x.Name,
                        Frequency = x.Frequency,
                        StartTime = x.StartTime,
                        Type = x.Type,
                        ProfileMessageFormId = x.ProfileMessageFormId,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false
                    };
                    if (x.Id != null)
                    {
                        action.Id = x.Id.Value;
                    }
                    return action;
                });
                return result;
            }
            return null;
        }

    }
}
