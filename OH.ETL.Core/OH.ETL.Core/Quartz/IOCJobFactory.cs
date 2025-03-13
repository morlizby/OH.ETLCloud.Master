using System;
using Quartz;
using Quartz.Spi;

namespace OH.ETL.Core.Quartz;

public class IOCJobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;
    public IOCJobFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        return _serviceProvider.GetService(bundle.JobDetail.JobType) as IJob;

    }

    public void ReturnJob(IJob job)
    {
        (job as IDisposable)?.Dispose();
    }
}
