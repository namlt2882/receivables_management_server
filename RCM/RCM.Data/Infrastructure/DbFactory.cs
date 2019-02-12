using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using RCM.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CRM.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        RCMContext dbContext;

        public RCMContext Init()
        {
            return dbContext ?? (dbContext = new RCMContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
