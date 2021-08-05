using System;
using System.Threading;
using Module.Logging.Core;
using Reports.Notifications.Host.Jobs;
using Service.Scheduler.Client;
using Module.Tracing.BasicTracer;
using Module.Tracing.Logging;

namespace Reports.Notifications.Host
{
    public sealed class JobBootstrapper : IDisposable
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly JobProcessor _processor;

        public JobBootstrapper(
            DailyDigestJob dailyDigestJob,
            ILoggerFactory loggerFactory,
            string schedulerRabbitMq,
            string dbConnectionString)
        {
            var tracer = new TracerFactory().CreateTracer(cfg => cfg.UseLogging(loggerFactory.GetLogger(typeof(JobBootstrapper))));
            _cancellationTokenSource = new CancellationTokenSource();

            _processor = JobProcessorFactory.Create(
                "Reports.Notifications",
                new Uri(schedulerRabbitMq),
                cfg =>
                {
                    cfg.UseLoggerFactory(() => loggerFactory);
                    cfg.UseTracer(() => tracer);
                });

            void Configurator(JobConfigurator cfg)
            {
                cfg.DontUseRetry().DisableConcurrentExecution(c => c.UseSqlSharedLock(dbConnectionString));
            }

            _processor.RegisterJob<DailyDigestJob>(
                dailyDigestJob,
                Configurator,
                _cancellationTokenSource.Token
            );
        }

        public void Dispose()
        {
            _processor?.Dispose();
            _cancellationTokenSource?.Dispose();
        }

        public void Start()
        {
            _processor.StartProcessing();
        }

        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
            _processor?.StopProcessing();
        }
    }
}