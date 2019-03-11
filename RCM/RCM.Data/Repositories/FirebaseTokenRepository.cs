using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Model;
namespace RCM.Data.Repositories
{
    public interface IFirebaseTokenRepository : IRepository<FirebaseToken>
    {
    }

    public class FirebaseTokenRepository : RepositoryBase<FirebaseToken>, IFirebaseTokenRepository
    {
        public FirebaseTokenRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
