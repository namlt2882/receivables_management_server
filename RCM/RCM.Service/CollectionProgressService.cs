﻿using CRM.Data.Infrastructure;
using RCM.Data.Repositories;
using RCM.Helper;
using RCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RCM.Service
{
    public interface ICollectionProgressService
    {
        IEnumerable<CollectionProgress> GetCollectionProgresss();
        IEnumerable<CollectionProgress> GetCollectionProgresss(Expression<Func<CollectionProgress, bool>> where);
        CollectionProgress GetCollectionProgress(int id);
        CollectionProgress CreateCollectionProgress(CollectionProgress collectionProgress);
        void EditCollectionProgress(CollectionProgress collectionProgress);
        void RemoveCollectionProgress(int id);
        void RemoveCollectionProgress(CollectionProgress collectionProgress);
        void SaveCollectionProgress();
        void MarkAsDone(CollectionProgress collectionProgress);
    }

    public class CollectionProgressService : ICollectionProgressService
    {
        private readonly ICollectionProgressRepository _collectionProgressRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CollectionProgressService(ICollectionProgressRepository collectionProgressRepository, IUnitOfWork unitOfWork)
        {
            _collectionProgressRepository = collectionProgressRepository;
            _unitOfWork = unitOfWork;
        }

        public CollectionProgress CreateCollectionProgress(CollectionProgress collectionProgress)
        {
            collectionProgress.CreatedDate = DateTime.Now;
            collectionProgress.IsDeleted = false;
            collectionProgress.Status = Constant.COLLECTION_STATUS_COLLECTION_CODE;
            return _collectionProgressRepository.Add(collectionProgress);
        }

        public void EditCollectionProgress(CollectionProgress collectionProgress)
        {
            var entity = _collectionProgressRepository.GetById(collectionProgress.Id);
            entity = collectionProgress;
            entity.UpdatedDate = DateTime.Now;
            _collectionProgressRepository.Update(entity);
        }

        public CollectionProgress GetCollectionProgress(int id)
        {
            return _collectionProgressRepository.GetById(id);
        }

        public IEnumerable<CollectionProgress> GetCollectionProgresss()
        {
            return _collectionProgressRepository.GetAll();
        }

        public IEnumerable<CollectionProgress> GetCollectionProgresss(Expression<Func<CollectionProgress, bool>> where)
        {
            return _collectionProgressRepository.GetMany(where);
        }

        public void MarkAsDone(CollectionProgress collectionProgress)
        {
            if (!collectionProgress.ProgressStages.Any(x => x.Status != Constant.COLLECTION_STATUS_DONE_CODE))
            {
                collectionProgress.Status = Constant.COLLECTION_STATUS_DONE_CODE;
                EditCollectionProgress(collectionProgress);
            }
        }

        public void RemoveCollectionProgress(int id)
        {
            var entity = _collectionProgressRepository.GetById(id);
            _collectionProgressRepository.Delete(entity);
        }

        public void RemoveCollectionProgress(CollectionProgress collectionProgress)
        {
            collectionProgress.IsDeleted = true;
            collectionProgress.UpdatedDate = DateTime.Now;
            _collectionProgressRepository.Delete(collectionProgress);
        }

        public void SaveCollectionProgress()
        {
            _unitOfWork.Commit();
        }
    }
}
