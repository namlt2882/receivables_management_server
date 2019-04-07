using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCM.ViewModels
{
    public class ProfileStageVM : ProfileStageIM
    {
        public int? Id { get; set; }
    }

    public class ProfileStageIM
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public int Sequence { get; set; }
        public IEnumerable<ProfileStageActionVM> Actions { get; set; }
    }


}
