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
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Receivable> Receivables { get; set; }

    }
}
