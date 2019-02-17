using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Data.Repositories;
using RCM.Model;

namespace RCM.Service
{
    public interface IProgressMessageFormService
    {
        IEnumerable<ProgressMessageForm> GetProgressMessageForms();
        IEnumerable<ProgressMessageForm> GetProgressMessageForms(Expression<Func<ProgressMessageForm, bool>> where);
        ProgressMessageForm GetProgressMessageForm(int id);
        void CreateProgressMessageForm(ProgressMessageForm progressMessageForm);
        void EditProgressMessageForm(ProgressMessageForm progressMessageForm);
        void RemoveProgressMessageForm(int id);
        void RemoveProgressMessageForm(ProgressMessageForm progressMessageForm);
        void SaveProgressMessageForm();
    }

    public class ProgressMessageFormService : IProgressMessageFormService
    {
        private readonly IProgressMessageFormRepository _progressMessageFormRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProgressMessageFormService(IProgressMessageFormRepository progressMessageFormRepository, IUnitOfWork unitOfWork)
        {
            this._progressMessageFormRepository = progressMessageFormRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateProgressMessageForm(ProgressMessageForm progressMessageForm)
        {
            progressMessageForm.CreatedDate = DateTime.Now;
            _progressMessageFormRepository.Add(progressMessageForm);
        }

        public void EditProgressMessageForm(ProgressMessageForm progressMessageForm)
        {
            var entity = _progressMessageFormRepository.GetById(progressMessageForm.Id);
            entity = progressMessageForm;
            entity.UpdatedDate = DateTime.Now;
            _progressMessageFormRepository.Update(entity);
        }

        public ProgressMessageForm GetProgressMessageForm(int id)
        {
            return _progressMessageFormRepository.GetById(id);
        }

        public IEnumerable<ProgressMessageForm> GetProgressMessageForms()
        {
            return _progressMessageFormRepository.GetAll();
        }

        public IEnumerable<ProgressMessageForm> GetProgressMessageForms(Expression<Func<ProgressMessageForm, bool>> where)
        {
            return _progressMessageFormRepository.GetMany(where);
        }

        public void RemoveProgressMessageForm(int id)
        {
            var entity = _progressMessageFormRepository.GetById(id);
            _progressMessageFormRepository.Delete(entity);
        }

        public void RemoveProgressMessageForm(ProgressMessageForm progressMessageForm)
        {
            progressMessageForm.IsDeleted = true;
            progressMessageForm.UpdatedDate = DateTime.Now;
            _progressMessageFormRepository.Delete(progressMessageForm);
        }

        public void SaveProgressMessageForm()
        {
            _unitOfWork.Commit();
        }
    }
}
