using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCM.ViewModels
{
   
    public class ProgressStageVM : ProgressStageCM
    {
        public int Id { get; set; }
        public int Sequence { get; set; }
        public int Status { get; set; }
        public string CollectorComment { get; set; }
    }
    public class ProgressStageCM
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public int CollectionProgressId { get; set; }
    }
    public class ProgressStageUM : ProgressStageVM
    {
    }
}
