using Ninject;
using Quartz;
using Quartz.Simpl;
using Quartz.Spi;
using System;
using RCM.Service;
using System.ComponentModel;

namespace RCM.Helpers
{
    public class NinjectJobFactory : IJobFactory
    {
        private readonly IKernel _kernel;

        public NinjectJobFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return (IJob)_kernel.Get(bundle.JobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {
            //this.resolutionRoot.Release(job);
        }
    }
}
