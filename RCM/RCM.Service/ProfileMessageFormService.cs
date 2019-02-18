using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Data.Repositories;
using RCM.Model;

namespace RCM.Service
{
    public interface IProfileMessageFormService
    {
        IEnumerable<ProfileMessageForm> GetProfileMessageForms();
        IEnumerable<ProfileMessageForm> GetProfileMessageForms(Expression<Func<ProfileMessageForm, bool>> where);
        ProfileMessageForm GetProfileMessageForm(int id);
        void CreateProfileMessageForm(ProfileMessageForm profileMessageForm);
        void EditProfileMessageForm(ProfileMessageForm profileMessageForm);
        void RemoveProfileMessageForm(int id);
        void RemoveProfileMessageForm(ProfileMessageForm profileMessageForm);
        void SaveProfileMessageForm();
    }

    public class ProfileMessageFormService : IProfileMessageFormService
    {
        private readonly IProfileMessageFormRepository _profileMessageFormRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProfileMessageFormService(IProfileMessageFormRepository profileMessageFormRepository, IUnitOfWork unitOfWork)
        {
            this._profileMessageFormRepository = profileMessageFormRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateProfileMessageForm(ProfileMessageForm profileMessageForm)
        {
            profileMessageForm.CreatedDate = DateTime.Now;
            _profileMessageFormRepository.Add(profileMessageForm);
        }

        public void EditProfileMessageForm(ProfileMessageForm profileMessageForm)
        {
            var entity = _profileMessageFormRepository.GetById(profileMessageForm.Id);
            entity = profileMessageForm;
            entity.UpdatedDate = DateTime.Now;
            _profileMessageFormRepository.Update(entity);
        }

        public ProfileMessageForm GetProfileMessageForm(int id)
        {
            return _profileMessageFormRepository.GetById(id);
        }

        public IEnumerable<ProfileMessageForm> GetProfileMessageForms()
        {
            return _profileMessageFormRepository.GetAll();
        }

        public IEnumerable<ProfileMessageForm> GetProfileMessageForms(Expression<Func<ProfileMessageForm, bool>> where)
        {
            return _profileMessageFormRepository.GetMany(where);
        }

        public void RemoveProfileMessageForm(int id)
        {
            var entity = _profileMessageFormRepository.GetById(id);
            _profileMessageFormRepository.Delete(entity);
        }

        public void RemoveProfileMessageForm(ProfileMessageForm profileMessageForm)
        {
            profileMessageForm.IsDeleted = true;
            profileMessageForm.UpdatedDate = DateTime.Now;
            _profileMessageFormRepository.Delete(profileMessageForm);
        }

        public void SaveProfileMessageForm()
        {
            _unitOfWork.Commit();
        }
    }
}
