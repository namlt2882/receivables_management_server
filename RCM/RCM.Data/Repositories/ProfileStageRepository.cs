using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Model;

namespace RCM.Data.Repositories
{
    public interface IProfileStageRepository : IRepository<ProfileStage>
    {
    }

    public class ProfileStageRepository : RepositoryBase<ProfileStage>, IProfileStageRepository
    {
        public ProfileStageRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
