using CRM.Data.Infrastructure;
using RCM.Data.Repositories;
using RCM.Helper;
using RCM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RCM.Service
{
    public interface IReceivableService
    {
        IEnumerable<Receivable> GetReceivables();
        IEnumerable<Receivable> GetReceivables(Expression<Func<Receivable, bool>> where);
        Receivable GetReceivable(int id);
        void CreateReceivable(Receivable receivable);
        void EditReceivable(Receivable receivable);
        void RemoveReceivable(int id);
        void RemoveReceivable(Receivable receivable);
        void SaveReceivable();
        void CloseReceivable(Receivable receivable);
    }

    public class ReceivableService : IReceivableService
    {
        private readonly IReceivableRepository _receivableRepository;
        private readonly ICollectionProgressService _collectionProgressService;
        private readonly IUnitOfWork _unitOfWork;

        public ReceivableService(IReceivableRepository receivableRepository, ICollectionProgressService collectionProgressService, IUnitOfWork unitOfWork)
        {
            _receivableRepository = receivableRepository;
            _collectionProgressService = collectionProgressService;
            _unitOfWork = unitOfWork;
        }

        public void CreateReceivable(Receivable receivable)
        {
            receivable.CreatedDate = DateTime.Now;
            receivable.IsDeleted = false;
            _receivableRepository.Add(receivable);
        }

        public void EditReceivable(Receivable receivable)
        {
            var entity = _receivableRepository.GetById(receivable.Id);
            entity = receivable;
            entity.UpdatedDate = DateTime.Now;
            _receivableRepository.Update(entity);
        }

        public Receivable GetReceivable(int id)
        {
            return _receivableRepository.GetById(id);
        }

        public IEnumerable<Receivable> GetReceivables()
        {
            return _receivableRepository.GetAll();
        }

        public IEnumerable<Receivable> GetReceivables(Expression<Func<Receivable, bool>> where)
        {
            return _receivableRepository.GetMany(where);
        }

        public void CloseReceivable(Receivable receivable)
        {
            receivable.ClosedDay = Int32.Parse(Utility.ConvertDatetimeToString(DateTime.Now));
            receivable.PrepaidAmount = receivable.DebtAmount;
            _collectionProgressService.CloseReceivable(receivable.CollectionProgress);
        }

        public void RemoveReceivable(int id)
        {
            var entity = _receivableRepository.GetById(id);
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.Now;
            _receivableRepository.Update(entity);
        }

        public void RemoveReceivable(Receivable receivable)
        {
            receivable.IsDeleted = true;
            receivable.UpdatedDate = DateTime.Now;
            _receivableRepository.Update(receivable);
        }

        public void SaveReceivable()
        {
            _unitOfWork.Commit();
        }
    }
}
