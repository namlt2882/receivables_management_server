using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Model;

namespace RCM.Data.Repositories
{
    public interface IProgressStageRepository : IRepository<ProgressStage>
    {
    }

    public class ProgressStageRepository : RepositoryBase<ProgressStage>, IProgressStageRepository
    {
        public ProgressStageRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
