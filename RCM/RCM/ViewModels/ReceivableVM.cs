using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCM.ViewModels
{

    public class ReceivableVM : ReceivableCM
    {
        public int Id { get; set; }
        public int? ClosedDay { get; set; }
        public int? PayableDay { get; set; }
    }
    public class ReceivableCM
    {
        public long PrepaidAmount { get; set; }
        public long DebtAmount { get; set; }
        public int CustomerId { get; set; }
        public int? LocationId { get; set; }
    }
    public class ReceivableUM : ReceivableVM
    {
    }
}
