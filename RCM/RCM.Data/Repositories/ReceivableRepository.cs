using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Model;

namespace RCM.Data.Repositories
{
    public interface IReceivableRepository : IRepository<Receivable>
    {
    }

    public class ReceivableRepository : RepositoryBase<Receivable>, IReceivableRepository
    {
        public ReceivableRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
