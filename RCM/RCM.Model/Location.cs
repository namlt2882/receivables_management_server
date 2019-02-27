using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RCM.Model
{
    public class Location : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Receivable> Receivables { get; set; }

    }
}
