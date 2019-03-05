using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCM.ViewModels
{
    public class AssignedCollectorVM
    {
        public int Id { get; set; }
        public string CollectorId { get; set; }
        public int ReceivableId { get; set; }
        public int Status { get; set; }
    }

    public class AssignedCollectorHM : AssignedCollectorVM
    {
        public DateTime CreatedDate;
    }

    public class AssignedCollectorUM
    {
        public string CollectorId { get; set; }
        public int ReceivableId { get; set; }
    }
}
