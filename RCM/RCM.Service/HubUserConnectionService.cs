using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Data.Repositories;
using RCM.Model;

namespace RCM.Service
{
    public interface IHubUserConnectionService
    {
        IEnumerable<HubUserConnection> GetHubUserConnections();
        IEnumerable<HubUserConnection> GetHubUserConnections(Expression<Func<HubUserConnection, bool>> where);
        HubUserConnection GetHubUserConnection(int id);
        void CreateHubUserConnection(HubUserConnection HubUserConnection);
        void EditHubUserConnection(HubUserConnection HubUserConnection);
        void RemoveHubUserConnection(int id);
        void RemoveHubUserConnection(Expression<Func<HubUserConnection, bool>> where);
        void SaveHubUserConnection();
    }

    public class HubUserConnectionService : IHubUserConnectionService
    {
        private readonly IHubUserConnectionRepository _HubUserConnectionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public HubUserConnectionService(IHubUserConnectionRepository HubUserConnectionRepository, IUnitOfWork unitOfWork)
        {
            this._HubUserConnectionRepository = HubUserConnectionRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateHubUserConnection(HubUserConnection HubUserConnection)
        {
            _HubUserConnectionRepository.Add(HubUserConnection);
        }

        public void EditHubUserConnection(HubUserConnection HubUserConnection)
        {
            var entity = _HubUserConnectionRepository.GetById(HubUserConnection.Id);
            entity = HubUserConnection;
            _HubUserConnectionRepository.Update(entity);
        }

        public HubUserConnection GetHubUserConnection(int id)
        {
            return _HubUserConnectionRepository.GetById(id);
        }

        public IEnumerable<HubUserConnection> GetHubUserConnections()
        {
            return _HubUserConnectionRepository.GetAll();
        }

        public IEnumerable<HubUserConnection> GetHubUserConnections(Expression<Func<HubUserConnection, bool>> where)
        {
            return _HubUserConnectionRepository.GetMany(where);
        }

        public void RemoveHubUserConnection(int id)
        {
            var entity = _HubUserConnectionRepository.GetById(id);
            _HubUserConnectionRepository.Delete(entity);
        }

        public void RemoveHubUserConnection(Expression<Func<HubUserConnection, bool>> where)
        {
            _HubUserConnectionRepository.Delete(where);
        }

        public void SaveHubUserConnection()
        {
            _unitOfWork.Commit();
        }
    }
}
