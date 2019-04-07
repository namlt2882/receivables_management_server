using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Data.Repositories;
using RCM.Model;
using System.Linq;

namespace RCM.Service
{
    public interface IProfileService
    {
        IEnumerable<Profile> GetProfiles();
        IEnumerable<Profile> GetProfiles(Expression<Func<Profile, bool>> where);
        Profile GetProfile(int id);
        Profile CreateProfile(Profile profile);
        void EditProfile(Profile profile);
        void RemoveProfile(int id);
        void RemoveProfile(Profile profile);
        void SaveProfile();
    }

    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProfileStageService _profileStageService;
        private readonly IProfileStageActionService _profileStageActionService;

        public ProfileService(IProfileRepository profileRepository, IUnitOfWork unitOfWork, IProfileStageService profileStageService, IProfileStageActionService profileStageActionService)
        {
            _profileRepository = profileRepository;
            _unitOfWork = unitOfWork;
            _profileStageService = profileStageService;
            _profileStageActionService = profileStageActionService;
        }

        public Profile CreateProfile(Profile profile)
        {
            profile.CreatedDate = DateTime.Now;
            profile.IsDeleted = false;
            return _profileRepository.Add(profile);
        }

        public void EditProfile(Profile profile)
        {
            Dictionary<int, ProfileStage> mapStage = new Dictionary<int, ProfileStage>();
            Dictionary<int, ProfileStageAction> mapAction = new Dictionary<int, ProfileStageAction>();

            var origin = _profileRepository.GetById(profile.Id);
            origin.Name = profile.Name;
            origin.UpdatedDate = DateTime.Now;
            // create new stage and new action
            profile.ProfileStages.ToList().ForEach(ps =>
            {
                if (ps.Id == 0)
                {
                    origin.ProfileStages.Add(ps);
                }
                else
                {
                    // add data to map
                    mapStage.Add(ps.Id, ps);
                    ps.ProfileStageActions.ToList().ForEach(psa =>
                    {
                        if (psa.Id != 0)
                        {
                            mapAction.Add(psa.Id, psa);
                        }
                        else
                        {
                            // add action for origin stage to create
                            var originStage = origin.ProfileStages.Where(ops => ops.Id == ps.Id).First();
                            if (originStage != null)
                            {
                                originStage.ProfileStageActions.Add(psa);
                            }
                        }
                    });
                }
            });
            // update and delete stage
            origin.ProfileStages.Where(ps => ps.Id > 0 && !ps.IsDeleted).ToList().ForEach(ps =>
            {
                ProfileStage stage;
                mapStage.TryGetValue(ps.Id, out stage);
                if (stage == null)
                {
                    // delete stage
                    // detele all action of this stage
                    ps.ProfileStageActions.ToList().ForEach(psa =>
                    {
                        _profileStageActionService.RemoveProfileStageAction(psa);
                    });
                    _profileStageService.RemoveProfileStage(ps);
                }
                else
                {
                    // update stage
                    ps.Name = stage.Name;
                    ps.Sequence = stage.Sequence;
                    ps.Duration = stage.Duration;
                    ps.ProfileStageActions.Where(psa => psa.Id > 0 && !psa.IsDeleted).ToList()
                    .ForEach(psa =>
                    {
                        ProfileStageAction action;
                        mapAction.TryGetValue(psa.Id, out action);
                        if (action == null)
                        {
                            // delete action
                            _profileStageActionService.RemoveProfileStageAction(psa);
                        }
                        else
                        {
                            // update action
                            psa.Name = action.Name;
                            psa.Type = action.Type;
                            psa.StartTime = action.StartTime;
                            psa.ProfileMessageFormId = action.ProfileMessageFormId;
                            psa.Frequency = action.Frequency;
                            _profileStageActionService.EditProfileStageAction(psa);
                        }
                    });
                    _profileStageService.EditProfileStage(ps);
                }
            });
            // then create
            _profileRepository.Update(origin);

        }

        public Profile GetProfile(int id)
        {
            return _profileRepository.GetById(id);
        }

        public IEnumerable<Profile> GetProfiles()
        {
            return _profileRepository.GetAll();
        }

        public IEnumerable<Profile> GetProfiles(Expression<Func<Profile, bool>> where)
        {
            return _profileRepository.GetMany(where);
        }

        public void RemoveProfile(int id)
        {
            Profile profile = _profileRepository.GetById(id);
            profile.IsDeleted = true;
            EditProfile(profile);
        }

        public void RemoveProfile(Profile profile)
        {
            profile.IsDeleted = true;
            profile.UpdatedDate = DateTime.Now;
            _profileRepository.Delete(profile);
        }

        public void SaveProfile()
        {
            _unitOfWork.Commit();
        }
    }
}
