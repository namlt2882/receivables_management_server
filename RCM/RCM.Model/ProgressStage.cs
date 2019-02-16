using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RCM.Model
{
    public class ProgressStage : BaseEntity
    {
        [MaxLength(100)]
        public int Stage{ get; set; }
        public int Status { get; set; }
        public string CollectorComment { get; set; }
        public int CollectionProgressId { get; set; }
        [ForeignKey("CollectionProgressId")]
        public virtual CollectionProgress CollectionProgress { get; set; }
        public virtual ICollection<ProgressStageAction> ProgressStageAction { get; set; }
    }
}
