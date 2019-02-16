using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RCM.Model
{
    public class Receivable : BaseEntity
    {
        public int ClosedDay { get; set; }
        public int PayableDay { get; set; }
        public long PrepaidAmount { get; set; }
        public long DebtAmount { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        public int? LocationId { get; set; }
        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
        
        //public int CollectionProgressId { get; set; }
        //[ForeignKey("CollectionProgressId")]
        public virtual CollectionProgress CollectionProgress { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<AssignedCollector> AssignedCollectors { get; set; }
    }
}
