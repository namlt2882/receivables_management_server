using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Model;

namespace RCM.Data.Repositories
{
    public interface IProgressStageActionRepository : IRepository<ProgressStageAction>
    {
    }

    public class ProgressStageActionRepository : RepositoryBase<ProgressStageAction>, IProgressStageActionRepository
    {
        public ProgressStageActionRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
