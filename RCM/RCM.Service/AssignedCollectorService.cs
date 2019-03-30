using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Data.Repositories;
using RCM.Helper;
using RCM.Model;

namespace RCM.Service
{
    public interface IAssignedCollectorService
    {
        IEnumerable<AssignedCollector> GetAssignedCollectors();
        IEnumerable<AssignedCollector> GetAssignedCollectors(Expression<Func<AssignedCollector, bool>> where);
        AssignedCollector GetAssignedCollector(int id);
        AssignedCollector GetAssignedCollector(Expression<Func<AssignedCollector, bool>> where);
        AssignedCollector CreateAssignedCollector(AssignedCollector assignedCollector);
        void EditAssignedCollector(AssignedCollector assignedCollector);
        void RemoveAssignedCollector(int id);
        void RemoveAssignedCollector(AssignedCollector assignedCollector);
        void SaveAssignedCollector();
    }

    public class AssignedCollectorService : IAssignedCollectorService
    {
        private readonly IAssignedCollectorRepository _assignedCollectorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssignedCollectorService(IAssignedCollectorRepository assignedCollectorRepository, IUnitOfWork unitOfWork)
        {
            this._assignedCollectorRepository = assignedCollectorRepository;
            this._unitOfWork = unitOfWork;
        }

        public AssignedCollector CreateAssignedCollector(AssignedCollector assignedCollector)
        {
            assignedCollector.CreatedDate = DateTime.Now;
            assignedCollector.Status = Constant.ASSIGNED_STATUS_ACTIVE_CODE;
            assignedCollector.IsDeleted = false;
            return _assignedCollectorRepository.Add(assignedCollector);
        }

        public void EditAssignedCollector(AssignedCollector assignedCollector)
        {
            var entity = _assignedCollectorRepository.GetById(assignedCollector.Id);
            entity = assignedCollector;
            entity.UpdatedDate = DateTime.Now;
            _assignedCollectorRepository.Update(entity);
        }

        public AssignedCollector GetAssignedCollector(int id)
        {
            return _assignedCollectorRepository.GetById(id);
        }

        public AssignedCollector GetAssignedCollector(Expression<Func<AssignedCollector, bool>> where)
        {
            return _assignedCollectorRepository.Get(where);
        }

        public IEnumerable<AssignedCollector> GetAssignedCollectors()
        {
            return _assignedCollectorRepository.GetAll();
        }

        public IEnumerable<AssignedCollector> GetAssignedCollectors(Expression<Func<AssignedCollector, bool>> where)
        {
            return _assignedCollectorRepository.GetMany(where);
        }

        public void RemoveAssignedCollector(int id)
        {
            var entity = _assignedCollectorRepository.GetById(id);
            _assignedCollectorRepository.Delete(entity);
        }

        public void RemoveAssignedCollector(AssignedCollector assignedCollector)
        {
            assignedCollector.IsDeleted = true;
            assignedCollector.UpdatedDate = DateTime.Now;
            _assignedCollectorRepository.Delete(assignedCollector);
        }

        public void SaveAssignedCollector()
        {
            _unitOfWork.Commit();
        }
    }
}
