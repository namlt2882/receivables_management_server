﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Data.Repositories;
using RCM.Model;

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
    }

    public class ReceivableService : IReceivableService
    {
        private readonly IReceivableRepository _receivableRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReceivableService(IReceivableRepository receivableRepository, IUnitOfWork unitOfWork)
        {
            this._receivableRepository = receivableRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateReceivable(Receivable receivable)
        {
            receivable.CreatedDate = DateTime.Now;
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