using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RCM.Model
{
    public class ProfileStageAction : BaseEntity
    {

        public ProfileStageAction()
        {

        }
        public ProfileStageAction(int _profileStageId, string _name, short _frequency, int _startTime, int _type, int _profileMessageFormId)
        {
            ProfileStageId = _profileStageId;
            Name = _name;
            Frequency = _frequency;
            StartTime = _startTime;
            Type = _type;
            ProfileMessageFormId = _profileMessageFormId;
        }

        [MaxLength(100)]
        public string Name { get; set; }
        public int StartTime { get; set; }
        public int Type { get; set; }
        public short Frequency { get; set; }
        public int ProfileStageId { get; set; }
        [ForeignKey("ProfileStageId")]
        public virtual ProfileStage ProfileStage { get; set; }
        public int? ProfileMessageFormId { get; set; }
        [ForeignKey("ProfileMessageFormId")]
        public virtual ProfileMessageForm ProfileMessageForm { get; set; }
    }
}
