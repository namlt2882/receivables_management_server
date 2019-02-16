using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RCM.Model
{
    public class ProfileStageAction : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public short Frequency { get; set; }
        public int StartTime { get; set; }

        public int ProfileStageId { get; set; }
        [ForeignKey("ProfileStageId")]
        public virtual ProfileStage ProfileStage { get; set; }

        public int? ProfileMessageFormId { get; set; }
        [ForeignKey("ProfileMessageFormId")]
        public virtual ProfileMessageForm ProfileMessageForm { get; set; }
    }
}
