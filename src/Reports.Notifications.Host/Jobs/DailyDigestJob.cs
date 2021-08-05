using System;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Reports.Notifications.Host.DataProvider;
using Service.Scheduler.Client;

namespace Reports.Notifications.Host.Jobs
{
    public class DailyDigestJob : IJob
    {
        public DailyDigestJob()
        {
            Console.WriteLine("vvv");
        }

        public async Task Execute(IJobExecutionContext context, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue<DigestDailyDataProvider>(d => d.GetData());
        }
    }
}