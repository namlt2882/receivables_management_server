using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RCM.Model
{
    public class AssignedCollector : BaseEntity
    {

        public int Status { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int ReceivableId { get; set; }
        [ForeignKey("ReceivableId")]
        public virtual Receivable Receivable { get; set; }
    }
}
