using System.Collections.Generic;

namespace RCM.ViewModels
{
    public class ProfileVM : ProfileIM
    {
        public int Id { get; set; }
    }

    public class ProfileIM
    {
        public string Name { get; set; }
        public long DebtAmountFrom { get; set; }
        public long DebtAmountTo { get; set; }
        public IEnumerable<ProfileStageVM> Stages { get; set; }
    }

    public class ProfileUM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long DebtAmountFrom { get; set; }
        public long DebtAmountTo { get; set; }
    }
}
