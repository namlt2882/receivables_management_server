using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RCM.Model
{
    public class Profile : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public long DebtAmountTo { get; set; }
        public long DebtAmountFrom { get; set; }
        public bool IsDisable { get; set; }
        public virtual ICollection<ProfileStage> ProfileStages { get; set; }
    }
}
