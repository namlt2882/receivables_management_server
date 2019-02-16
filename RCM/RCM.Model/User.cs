
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace RCM.Model
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBanned { get; set; }
        public string Connection { get; set; }
        public string Address { get; set; }
        public int? LocationId { get; set; }
        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
        //public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<HubUserConnection> HubUserConnections { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<AssignedCollector> AssignedCollectors { get; set; }
    }
}
