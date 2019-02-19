using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Data.Repositories;
using RCM.Model;

namespace RCM.Service
{
    public interface ICollectionProgressService
    {
        IEnumerable<CollectionProgress> GetCollectionProgresss();
        IEnumerable<CollectionProgress> GetCollectionProgresss(Expression<Func<CollectionProgress, bool>> where);
        CollectionProgress GetCollectionProgress(int id);
        void CreateCollectionProgress(CollectionProgress collectionProgress);
        void EditCollectionProgress(CollectionProgress collectionProgress);
        void RemoveCollectionProgress(int id);
        void RemoveCollectionProgress(CollectionProgress collectionProgress);
        void SaveCollectionProgress();
    }

    public class CollectionProgressService : ICollectionProgressService
    {
        private readonly ICollectionProgressRepository _collectionProgressRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CollectionProgressService(ICollectionProgressRepository collectionProgressRepository, IUnitOfWork unitOfWork)
        {
            this._collectionProgressRepository = collectionProgressRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateCollectionProgress(CollectionProgress collectionProgress)
        {
            collectionProgress.CreatedDate = DateTime.Now;
            collectionProgress.IsDeleted = false;
            collectionProgress.Status = 0;
            _collectionProgressRepository.Add(collectionProgress);
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
