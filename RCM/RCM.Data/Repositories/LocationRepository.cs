using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Model;

namespace RCM.Data.Repositories
{
    public interface ILocationRepository : IRepository<Location>
    {
    }

    public class LocationRepository : RepositoryBase<Location>, ILocationRepository
    {
        public LocationRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
