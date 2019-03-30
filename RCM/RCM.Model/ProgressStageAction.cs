using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RCM.Model
{
    public class ProgressStageAction : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public int StartTime { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public int ExcutionDay { get; set; }
        public string Evidence { get; set; }
        public string Note { get; set; }
        public string NData { get; set; }
        public int? DoneAt { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int ProgressStageId { get; set; }
        [ForeignKey("ProgressStageId")]
        public virtual ProgressStage ProgressStage { get; set; }
        public int? ProgressMessageFormId { get; set; }
        [ForeignKey("ProgressMessageFormId")]
        public virtual ProgressMessageForm ProgressMessageForm { get; set; }
    }
}
