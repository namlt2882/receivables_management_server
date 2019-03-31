using System;
using System.Collections.Generic;
using CRM.Data.Infrastructure;
using Microsoft.AspNetCore.Identity;
using RCM.Data.Repositories;
using RCM.Model;
using RCM.Model.Algorithm;

namespace RCM.Service
{
    public interface IPointService
    {
        List<CollectorCppFM> GetAllCollectorCpp();
    }
    public class PointService : IPointService
    {
        public static readonly double DEFAULT_CPP = 4;
        private readonly UserManager<User> _userManager;
        private readonly IPointRepository _pointRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PointService(UserManager<User> userManager, IPointRepository pointRepository, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _pointRepository = pointRepository;
            _unitOfWork = unitOfWork;
        }

        public List<CollectorCppFM> GetAllCollectorCpp()
        {
            List<CollectorCppFM> rs = new List<CollectorCppFM>();
            IList<User> collectors = _userManager.GetUsersInRoleAsync("Collector").Result;
            foreach (User collector in collectors)
            {
                var vm = new CollectorCppFM
                {
                    CollectorId = collector.Id,
                    CurrentReceivable = _pointRepository.CountCurrentReceivable(collector.Id)
                };
                var ppprs = _pointRepository.GetAllPpprOfCollector(collector.Id);
                vm.PPPRs = ppprs;
                vm.CPP = CountCPP(ppprs);
                rs.Add(vm);
            }
            return rs;
        }

        public double CountCPP(List<Pppr> ppprs)
        {
            double rs = 0;
            int size = ppprs.Count;
            if (size == 0)
            {
                return DEFAULT_CPP;
            }
            if (size == 1)
            {
                return ppprs[0].PPPR;
            }
            if (size == 2)
            {
                rs = (ppprs[0].PPPR + ppprs[1].PPPR) / 2;
                return rs;
            }
            ppprs.Sort((p1, p2) =>
            {
                double sub = p2.PPPR - p1.PPPR;
                return Convert.ToInt32(sub);
            });
            int mid = (int)Math.Ceiling(1.0 * size / 2);
            rs = (ppprs[mid - 1].PPPR + ppprs[mid].PPPR) / 2;
            return rs;
        }
    }
}
