using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Data.Repositories;
using RCM.Model;

namespace RCM.Service
{
    public interface IProfileService
    {
        IEnumerable<Profile> GetProfiles();
        IEnumerable<Profile> GetProfiles(Expression<Func<Profile, bool>> where);
        Profile GetProfile(int id);
        void CreateProfile(Profile profile);
        void EditProfile(Profile profile);
        void RemoveProfile(int id);
        void RemoveProfile(Profile profile);
        void SaveProfile();
    }

    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProfileService(IProfileRepository profileRepository, IUnitOfWork unitOfWork)
        {
            this._profileRepository = profileRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateProfile(Profile profile)
        {
            profile.CreatedDate = DateTime.Now;
            profile.IsDeleted = false;
            _profileRepository.Add(profile);
        }

        public void EditProfile(Profile profile)
        {
            var entity = _profileRepository.GetById(profile.Id);
            entity = profile;
            entity.UpdatedDate = DateTime.Now;
            _profileRepository.Update(entity);
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
