using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Data.Repositories;
using RCM.Model;

namespace RCM.Service
{
    public interface ILocationService
    {
        IEnumerable<Location> GetLocations();
        IEnumerable<Location> GetLocations(Expression<Func<Location, bool>> where);
        Location GetLocation(int id);
        void CreateLocation(Location location);
        void EditLocation(Location location);
        void RemoveLocation(int id);
        void RemoveLocation(Location location);
        void SaveLocation();
    }

    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LocationService(ILocationRepository locationRepository, IUnitOfWork unitOfWork)
        {
            this._locationRepository = locationRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateLocation(Location entity)
        {
            entity.CreatedDate = DateTime.Now;
            _locationRepository.Add(entity);
        }

        public void EditLocation(Location location)
        {
            var entity = _locationRepository.GetById(location.Id);
            entity = location;
            location.UpdatedDate= DateTime.Now;
            _locationRepository.Update(entity);
        }

        public Location GetLocation(int id)
        {
            return _locationRepository.GetById(id);
        }

        public IEnumerable<Location> GetLocations()
        {
            return _locationRepository.GetAll();
        }

        public IEnumerable<Location> GetLocations(Expression<Func<Location, bool>> where)
        {
            return _locationRepository.GetMany(where);
        }

        public void RemoveLocation(int id)
        {
            var entity = _locationRepository.GetById(id);
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.Now;
            _locationRepository.Update(entity);
        }

        public void RemoveLocation(Location entity)
        {
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.Now;
            _locationRepository.Update(entity);
        }

        public void SaveLocation()
        {
            _unitOfWork.Commit();
        }
    }
}
