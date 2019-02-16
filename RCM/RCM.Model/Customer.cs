using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RCM.Model
{
    public class Customer : BaseEntity
    {
        [MaxLength(15)]
        public string Code { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(15)]
        public string Phone { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
        public virtual ICollection<Receivable> Receivables { get; set; }
    }
}
