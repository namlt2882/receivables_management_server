using CRM.Data.Infrastructure;
using RCM.Data.Repositories;
using RCM.Helper;
using RCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RCM.Service
{
    public interface IProgressStageActionService
    {
        IEnumerable<ProgressStageAction> GetProgressStageActions();
        IEnumerable<ProgressStageAction> GetProgressStageActions(Expression<Func<ProgressStageAction, bool>> where);
        ProgressStageAction GetProgressStageAction(int id);
        ProgressStageAction CreateProgressStageAction(ProgressStageAction progressStageAction);
        void EditProgressStageAction(ProgressStageAction progressStageAction);
        void RemoveProgressStageAction(int id);
        void RemoveProgressStageAction(ProgressStageAction progressStageAction);
        void SaveProgressStageAction();
        void MarkAsDone(ProgressStageAction progressStageAction);
    }

    public class ProgressStageActionService : IProgressStageActionService
    {
        private readonly IProgressStageActionRepository _progressStageActionRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IProgressStageService _progressStageService;

        public ProgressStageActionService(IProgressStageActionRepository progressStageActionRepository, IUnitOfWork unitOfWork, IProgressStageService progressStageService)
        {
            _progressStageActionRepository = progressStageActionRepository;
            _unitOfWork = unitOfWork;
            _progressStageService = progressStageService;
        }

        public ProgressStageAction CreateProgressStageAction(ProgressStageAction progressStageAction)
        {
            progressStageAction.CreatedDate = DateTime.Now;
            progressStageAction.IsDeleted = false;
            progressStageAction.Status = 0;
            return _progressStageActionRepository.Add(progressStageAction);
        }

        public void EditProgressStageAction(ProgressStageAction progressStageAction)
        {
            var entity = _progressStageActionRepository.GetById(progressStageAction.Id);
            entity = progressStageAction;
            entity.UpdatedDate = DateTime.Now;
            _progressStageActionRepository.Update(entity);
        }

        public ProgressStageAction GetProgressStageAction(int id)
        {
            return _progressStageActionRepository.GetById(id);
        }

        public IEnumerable<ProgressStageAction> GetProgressStageActions()
        {
            return _progressStageActionRepository.GetAll();
        }

        public IEnumerable<ProgressStageAction> GetProgressStageActions(Expression<Func<ProgressStageAction, bool>> where)
        {
            return _progressStageActionRepository.GetMany(where);
        }

        public void MarkAsDone(ProgressStageAction progressStageAction)
        {
            progressStageAction.DoneAt = Int32.Parse(DateTime.Now.ToString("HHmm"));
            progressStageAction.Status = Constant.COLLECTION_STATUS_DONE_CODE;
            EditProgressStageAction(progressStageAction);

        }

        public void RemoveProgressStageAction(int id)
        {
            var entity = _progressStageActionRepository.GetById(id);
            _progressStageActionRepository.Delete(entity);
        }

        public void RemoveProgressStageAction(ProgressStageAction progressStageAction)
        {
            progressStageAction.IsDeleted = true;
            progressStageAction.UpdatedDate = DateTime.Now;
            _progressStageActionRepository.Delete(progressStageAction);
        }

        public void SaveProgressStageAction()
        {
            _unitOfWork.Commit();
        }
    }
}
