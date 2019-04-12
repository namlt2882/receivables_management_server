using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCM.Model.Algorithm
{
    public class CollectorCppVM
    {
        public string CollectorId { get; set; }
        public double CPP { get; set; }
        public int CurrentReceivable { get; set; }
        public int TotalReceivableCount { get; set; }
    }

    public class CollectorCppFM : CollectorCppVM
    {
        public IEnumerable<Pppr> PPPRs { get; set; }
    }

    public class Pppr
    {
        public int ReceivableId { get; set; }
        public double Weight { get; set; }
        public double TimeRate { get; set; }
        public double ExpectedTimeRate { get; set; }
        public double PPPR { get; set; }
        public bool IsFail { get; set; }
        public bool IsCancel { get; set; }
    }

}
