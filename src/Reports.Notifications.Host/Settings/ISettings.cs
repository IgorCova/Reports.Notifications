namespace Reports.Notifications.Host
{
    public interface ISettings
    {
        string ReportsConnectionString { get; }
        string PostgresConnectionString { get; }
        string HangfireConnectionString { get; }
        string SchedulerRabbitMq { get; }
        int PrefetchCount { get; }
        string Get(string key);
    }
}