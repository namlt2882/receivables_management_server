using RCM.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        RCMContext Init();
    }
}
