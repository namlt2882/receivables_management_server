using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCM.ViewModels
{

    public class CollectionProgressVM : CollectionProgressCM
    {
        public int Id { get; set; }
        public int Status { get; set; }
    }
    public class CollectionProgressCM
    {
        public int ReceivableId { get; set; }
        public int ProfileId { get; set; }
    }
    public class CollectionProgressUM : CollectionProgressVM
    {
    }

    public class CollectionProgressDM : CollectionProgressVM
    {
        public IEnumerable<ProgressStageDM> Stages;
    }
}
