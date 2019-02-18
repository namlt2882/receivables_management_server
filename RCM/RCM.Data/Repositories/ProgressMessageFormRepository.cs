using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Model;
namespace RCM.Data.Repositories
{

    public interface IProgressMessageFormRepository : IRepository<ProgressMessageForm>
    {
    }

    public class ProgressMessageFormRepository : RepositoryBase<ProgressMessageForm>, IProgressMessageFormRepository
    {
        public ProgressMessageFormRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
