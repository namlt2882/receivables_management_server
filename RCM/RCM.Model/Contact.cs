using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RCM.Model
{
    public class Contact : BaseEntity
    {
        public int Type { get; set; }
        [MaxLength(15)]
        public string IdNo { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(15)]
        public string Phone { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
        public int ReceivableId { get; set; }
        [ForeignKey("ReceivableId")]
        public virtual Receivable Receivable { get; set; }
    }
}
