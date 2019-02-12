
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace RCM.Model
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? IsEnabled { get; set; }

        //public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<HubUserConnection> HubUserConnections { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
