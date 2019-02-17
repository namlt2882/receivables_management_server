using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Model;

namespace RCM.Data.Repositories
{
    public interface IProfileRepository : IRepository<Profile>
    {
    }

    public class ProfileRepository : RepositoryBase<Profile>, IProfileRepository
    {
        public ProfileRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
