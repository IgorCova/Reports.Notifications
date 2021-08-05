using Hangfire;
using Reports.Notifications.Host.DataProvider;

namespace Reports.Notifications.Host
{
    public static class Host
    {
        public static string EnqueueDailyDigest()
        {
            return BackgroundJob.Enqueue<DigestDailyDataProvider>(d => d.GetData());
        }
    }
}