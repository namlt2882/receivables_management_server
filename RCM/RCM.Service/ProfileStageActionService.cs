using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Data.Repositories;
using RCM.Model;

namespace RCM.Service
{
    public interface IProfileStageActionService
    {
        IEnumerable<ProfileStageAction> GetProfileStageActions();
        IEnumerable<ProfileStageAction> GetProfileStageActions(Expression<Func<ProfileStageAction, bool>> where);
        ProfileStageAction GetProfileStageAction(int id);
        void CreateProfileStageAction(ProfileStageAction profileStageAction);
        void EditProfileStageAction(ProfileStageAction profileStageAction);
        void RemoveProfileStageAction(int id);
        void RemoveProfileStageAction(ProfileStageAction profileStageAction);
        void SaveProfileStageAction();
    }

    public class ProfileStageActionService : IProfileStageActionService
    {
        private readonly IProfileStageActionRepository _profileStageActionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProfileStageActionService(IProfileStageActionRepository profileStageActionRepository, IUnitOfWork unitOfWork)
        {
            this._profileStageActionRepository = profileStageActionRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateProfileStageAction(ProfileStageAction profileStageAction)
        {
            profileStageAction.CreatedDate = DateTime.Now;
            _profileStageActionRepository.Add(profileStageAction);
        }

        public void EditProfileStageAction(ProfileStageAction profileStageAction)
        {
            var entity = _profileStageActionRepository.GetById(profileStageAction.Id);
            entity = profileStageAction;
            entity.UpdatedDate = DateTime.Now;
            _profileStageActionRepository.Update(entity);
        }

        public ProfileStageAction GetProfileStageAction(int id)
        {
            return _profileStageActionRepository.GetById(id);
        }

        public IEnumerable<ProfileStageAction> GetProfileStageActions()
        {
            return _profileStageActionRepository.GetAll();
        }

        public IEnumerable<ProfileStageAction> GetProfileStageActions(Expression<Func<ProfileStageAction, bool>> where)
        {
            return _profileStageActionRepository.GetMany(where);
        }

        public void RemoveProfileStageAction(int id)
        {
            var entity = _profileStageActionRepository.GetById(id);
            _profileStageActionRepository.Delete(entity);
        }

        public void RemoveProfileStageAction(ProfileStageAction profileStageAction)
        {
            profileStageAction.IsDeleted = true;
            profileStageAction.UpdatedDate = DateTime.Now;
            _profileStageActionRepository.Delete(profileStageAction);
        }

        public void SaveProfileStageAction()
        {
            _unitOfWork.Commit();
        }
    }
}
