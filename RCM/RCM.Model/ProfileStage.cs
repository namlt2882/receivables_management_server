using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RCM.Model
{
    public class ProfileStage : BaseEntity
    {

        public ProfileStage()
        {

        }
        public ProfileStage(int _profileId, string _name, int _duration, int _sequence)
        {
            ProfileId = _profileId;
            Name = _name;
            Duration = _duration;
            Sequence = _sequence;
        }

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
