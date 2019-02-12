using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCM.ViewModels
{
    public class NotificationVM
    {
        public Guid Id { get; set; }
        public String Type { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string NData { get; set; }
        public bool IsSeen { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class NotificationCM
    {
        public String Type { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public object NData { get; set; }
    }
}
