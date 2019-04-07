using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCM.ViewModels
{
    public class ProfileStageActionVM : ProfileStageActionIM
    {
        public int? Id { get; set; }
    }

    public class ProfileStageActionIM
    {
        public string Name { get; set; }
        public short Frequency { get; set; }
        public int StartTime { get; set; }
        public int Type { get; set; }
        public int? ProfileMessageFormId { get; set; }
    }
}
