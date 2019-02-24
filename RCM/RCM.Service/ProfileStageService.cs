using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Data.Repositories;
using RCM.Model;

namespace RCM.Service
{
    public interface IProfileStageService
    {
        IEnumerable<ProfileStage> GetProfileStages();
        IEnumerable<ProfileStage> GetProfileStages(Expression<Func<ProfileStage, bool>> where);
        ProfileStage GetProfileStage(int id);
        ProfileStage CreateProfileStage(ProfileStage profileStage);
        void EditProfileStage(ProfileStage profileStage);
        void RemoveProfileStage(int id);
        void RemoveProfileStage(ProfileStage profileStage);
        void SaveProfileStage();
    }

    public class ProfileStageService : IProfileStageService
    {
        private readonly IProfileStageRepository _profileStageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProfileStageService(IProfileStageRepository profileStageRepository, IUnitOfWork unitOfWork)
        {
            this._profileStageRepository = profileStageRepository;
            this._unitOfWork = unitOfWork;
        }

        public ProfileStage CreateProfileStage(ProfileStage profileStage)
        {
            profileStage.CreatedDate = DateTime.Now;
            profileStage.IsDeleted = false;
            return _profileStageRepository.Add(profileStage);
        }

        public void EditProfileStage(ProfileStage profileStage)
        {
            var entity = _profileStageRepository.GetById(profileStage.Id);
            entity = profileStage;
            entity.UpdatedDate = DateTime.Now;
            _profileStageRepository.Update(entity);
        }

        public ProfileStage GetProfileStage(int id)
        {
            return _profileStageRepository.GetById(id);
        }

        public IEnumerable<ProfileStage> GetProfileStages()
        {
            return _profileStageRepository.GetAll();
        }

        public IEnumerable<ProfileStage> GetProfileStages(Expression<Func<ProfileStage, bool>> where)
        {
            return _profileStageRepository.GetMany(where);
        }

        public void RemoveProfileStage(int id)
        {
            var entity = _profileStageRepository.GetById(id);
            _profileStageRepository.Delete(entity);
        }

        public void RemoveProfileStage(ProfileStage profileStage)
        {
            profileStage.IsDeleted = true;
            profileStage.UpdatedDate = DateTime.Now;
            _profileStageRepository.Delete(profileStage);
        }

        public void SaveProfileStage()
        {
            _unitOfWork.Commit();
        }
    }
}
