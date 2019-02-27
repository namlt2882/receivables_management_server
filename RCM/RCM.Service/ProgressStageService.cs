using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using CRM.Data.Infrastructure;
using RCM.Data.Repositories;
using RCM.Helper;
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
        void MarkAsDone(ProgressStage progressStage);
    }

    public class ProgressStageService : IProgressStageService
    {
        private readonly IProgressStageRepository _progressStageRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICollectionProgressService _collectionProgressService;

        public ProgressStageService(IProgressStageRepository progressStageRepository, IUnitOfWork unitOfWork, ICollectionProgressService collectionProgressService)
        {
            this._progressStageRepository = progressStageRepository;
            this._unitOfWork = unitOfWork;
            _collectionProgressService = collectionProgressService;
        }

        public void CreateProgressStage(ProgressStage progressStage)
        {
            progressStage.CreatedDate = DateTime.Now;
            progressStage.IsDeleted = false;
            progressStage.Status = Constant.COLLECTION_STATUS_COLLECTION_CODE;
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

        public void MarkAsDone(ProgressStage progressStage)
        {
            progressStage.Status = Constant.COLLECTION_STATUS_DONE_CODE;
            EditProgressStage(progressStage);

            if (!progressStage.CollectionProgress.ProgressStages.Any(x => x.Status != Constant.COLLECTION_STATUS_DONE_CODE))
            {
                _collectionProgressService.MarkAsDone(progressStage.CollectionProgress);
            }
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
