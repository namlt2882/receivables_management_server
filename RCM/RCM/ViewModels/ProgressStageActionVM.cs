using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCM.ViewModels
{
  
    public class ProgressStageActionVM : ProgressStageActionCM
    {
        public int Id { get; set; }
        public int? DoneAt { get; set; }
        public int Status { get; set; }
    }
    public class ProgressStageActionCM
    {
        public string Name { get; set; }
        public int StartTime { get; set; }
        public int Type { get; set; }
        public int ExcutionDay { get; set; }
        public int ProgressStageId { get; set; }
        public int? ProgressMessageFormId { get; set; }

    }
    public class ProgressStageActionUM : ProgressStageActionVM
    {
    }

    public class ProgressStageActionDM : ProgressStageActionVM
    {

    }
}
