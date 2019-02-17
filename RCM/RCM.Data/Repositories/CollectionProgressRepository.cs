using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Model;

namespace RCM.Data.Repositories
{
    public interface ICollectionProgressRepository : IRepository<CollectionProgress>
    {
    }

    public class CollectionProgressRepository : RepositoryBase<CollectionProgress>, ICollectionProgressRepository
    {
        public CollectionProgressRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
