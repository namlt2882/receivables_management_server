using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using System.Linq;
using RCM.Data.Utilities;
using RCM.Model;
using RCM.Model.Algorithm;

namespace RCM.Data.Repositories
{
    public interface IPointRepository : IRepository<Receivable>
    {
        int CountCurrentReceivable(string collectorId);
        List<Pppr> GetAllPpprOfCollector(string collectorId);
    }
    public class PointRepository : RepositoryBase<Receivable>, IPointRepository
    {
        public PointRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public int CountCurrentReceivable(string collectorId)
        {
            int count = 0;
            var tmp = (from r in DbContext.Receivables
                       where r.CollectionProgress.Status == 1
                       select new
                       {
                           Collector = r.AssignedCollectors.Where(ac => ac.Status == 1 && ac.UserId == collectorId).SingleOrDefault()
                       });
            if (tmp.Count() > 0)
            {
                count = (from c in tmp
                             //check if collecting receivable has no collector
                         where c.Collector != null
                         && c.Collector.UserId == collectorId
                         select c).Count();
            }
            return count;
        }

        public List<Pppr> GetAllPpprOfCollector(string collectorId)
        {
            var ppprs = (from r in DbContext.Receivables
                         where r.IsConfirmed
                         && r.CollectionProgress.Status != 1 && r.CollectionProgress.Status != 4
                         && r.ClosedDay != null
                         && r.AssignedCollectors.Where(ac => ac.Status == 1 && ac.UserId == collectorId).SingleOrDefault() != null
                         select new Pppr
                         {
                             ReceivableId = r.Id,
                             Weight = PerformancePointUtility.GetWeight(r.DebtAmount - r.PrepaidAmount),
                             ExpectedTimeRate = PerformancePointUtility.GetExpectedTime(r.DebtAmount - r.PrepaidAmount),
                             TimeRate = PerformancePointUtility.GetReceivableTimeRate(r.PayableDay.Value, r.ClosedDay.Value, 
                             r.CollectionProgress.ProgressStages.Select(stage=>stage.Duration).Sum()),
                             IsCancel = r.CollectionProgress.Status == 0,
                             IsFail = r.CollectionProgress.Status == 2
                         }).ToList();
            ppprs.ForEach(model =>
            {
                if (model.IsCancel)
                {
                    model.PPPR = 0.5;
                }
                else if (model.IsFail)
                {
                    model.PPPR = 0;
                }
                else
                {
                    //PPPR = 5 *[w / (1 + (t – t’)* 2 / 100)]
                    double pppr = 5 * (model.Weight / (1 + (model.TimeRate - model.ExpectedTimeRate) * 2 / 100));
                    if (pppr > 100)
                    {
                        pppr = 100;
                    }
                    model.PPPR = pppr;
                }
            });
            return ppprs;
        }
    }
}
