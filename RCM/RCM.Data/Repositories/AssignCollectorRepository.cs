using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Model;

namespace RCM.Data.Repositories
{
    public interface IAssignedCollectorRepository : IRepository<AssignedCollector>
    {
    }

    public class AssignedCollectorRepository : RepositoryBase<AssignedCollector>, IAssignedCollectorRepository
    {
        public AssignedCollectorRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
