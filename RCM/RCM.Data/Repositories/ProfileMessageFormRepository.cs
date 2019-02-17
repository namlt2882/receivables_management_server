using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Model;
namespace RCM.Data.Repositories
{
   
    public interface IProfileMessageFormRepository : IRepository<ProfileMessageForm>
    {
    }

    public class ProfileMessageFormRepository : RepositoryBase<ProfileMessageForm>, IProfileMessageFormRepository
    {
        public ProfileMessageFormRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
