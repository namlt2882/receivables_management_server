using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RCM.Model
{
    public class CollectionProgress : BaseEntity
    {
        public int Status { get; set; }
        public int ReceivableId { get; set; }
        [ForeignKey("ReceivableId")]
        public virtual Receivable Receivable { get; set; }
        public int ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public virtual Profile Profile { get; set; }
        public virtual ICollection<ProgressStage> ProgressStages { get; set; }
    }
}
