using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCM.ViewModels
{
  
    public class ProgressMessageFormVM : ProgressMessageFormCM
    {
        public int Id { get; set; }
    }
    public class ProgressMessageFormCM
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public int Type { get; set; }

    }
    public class ProgressMessageFormUM : ProgressMessageFormVM
    {
    }
}
