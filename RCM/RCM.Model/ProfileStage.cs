using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RCM.Model
{
    public class ProfileStage : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public int Duration { get; set; }
        public int Sequence { get; set; }
        public int ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public virtual Profile Profile { get; set; }
        public virtual ICollection<ProfileStageAction> ProfileStageActions { get; set; }
    }
}
