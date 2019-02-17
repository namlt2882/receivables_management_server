using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Model;

namespace RCM.Data.Repositories
{
    public interface IProfileStageActionRepository : IRepository<ProfileStageAction>
    {
    }

    public class ProfileStageActionRepository : RepositoryBase<ProfileStageAction>, IProfileStageActionRepository
    {
        public ProfileStageActionRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
