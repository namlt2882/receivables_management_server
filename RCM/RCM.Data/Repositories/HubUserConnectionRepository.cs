using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Model;

namespace RCM.Data.Repositories
{
    public interface IHubUserConnectionRepository : IRepository<HubUserConnection>
    {
    }

    public class HubUserConnectionRepository : RepositoryBase<HubUserConnection>, IHubUserConnectionRepository
    {
        public HubUserConnectionRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
