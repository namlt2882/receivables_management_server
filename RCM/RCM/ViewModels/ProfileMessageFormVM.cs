using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCM.ViewModels
{
    public class ProfileMessageFormVM : ProfileMessageFormIM
    {
    }

    public class ProfileMessageFormIM
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public int Type { get; set; }
    }
}
