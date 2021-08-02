using Hangfire;
using Notifications.DataProvider;

namespace Notifications.Host
{
    public static class Host
    {
        public static int EnqueueDailyDigest()
        {
            //Fire-and-Forget
            BackgroundJob.Enqueue<DigestDailyDataProvider>(d => d.GetData());
            return 0;
        }
    }
}