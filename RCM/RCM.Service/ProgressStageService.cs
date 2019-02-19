using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Data.Repositories;
using RCM.Model;

namespace RCM.Service
{
    public interface IProgressStageService
    {
        IEnumerable<ProgressStage> GetProgressStages();
        IEnumerable<ProgressStage> GetProgressStages(Expression<Func<ProgressStage, bool>> where);
        ProgressStage GetProgressStage(int id);
        void CreateProgressStage(ProgressStage progressStage);
        void EditProgressStage(ProgressStage progressStage);
        void RemoveProgressStage(int id);
        void RemoveProgressStage(ProgressStage progressStage);
        void SaveProgressStage();
    }

    public class ProgressStageService : IProgressStageService
    {
        private readonly IProgressStageRepository _progressStageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProgressStageService(IProgressStageRepository progressStageRepository, IUnitOfWork unitOfWork)
        {
            this._progressStageRepository = progressStageRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateProgressStage(ProgressStage progressStage)
        {
            progressStage.CreatedDate = DateTime.Now;
            progressStage.IsDeleted = false;
            progressStage.Status = 0;
            _progressStageRepository.Add(progressStage);
        }

        public void EditProgressStage(ProgressStage progressStage)
        {
            var entity = _progressStageRepository.GetById(progressStage.Id);
            entity = progressStage;
            entity.UpdatedDate = DateTime.Now;
            _progressStageRepository.Update(entity);
        }

        public ProgressStage GetProgressStage(int id)
        {
            return _progressStageRepository.GetById(id);
        }

        public IEnumerable<ProgressStage> GetProgressStages()
        {
            return _progressStageRepository.GetAll();
        }

        public IEnumerable<ProgressStage> GetProgressStages(Expression<Func<ProgressStage, bool>> where)
        {
            return _progressStageRepository.GetMany(where);
        }

        public void RemoveProgressStage(int id)
        {
            var entity = _progressStageRepository.GetById(id);
            _progressStageRepository.Delete(entity);
        }

        public void RemoveProgressStage(ProgressStage progressStage)
        {
            progressStage.IsDeleted = true;
            progressStage.UpdatedDate = DateTime.Now;
            _progressStageRepository.Delete(progressStage);
        }

        public void SaveProgressStage()
        {
            _unitOfWork.Commit();
        }
    }
}
