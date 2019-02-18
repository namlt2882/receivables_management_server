using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Model;

namespace RCM.Data.Repositories
{
    public interface IContactRepository : IRepository<Contact>
    {
    }

    public class ContactRepository : RepositoryBase<Contact>, IContactRepository
    {
        public ContactRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
