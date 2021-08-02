using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using Module.AspNetCore.Metrics;
using Module.AspNetCore;
using Module.Logging.Core;
using Module.Tracing.BasicTracer;
using Module.Tracing.Contract.Tracer;
using Module.Tracing.Logging;
using Medallion.Threading.Postgres;
using Medallion.Threading;
using Notifications.Host.Extensions;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using ILogger = Module.Logging.Core.ILogger;
using Microsoft.Extensions.DependencyInjection;
using LightInject;

namespace Notifications.Host
{
    public class Startup
    {
        private const string ServiceName = "Notifications.Host";
        private const string ConnectionStringKey = "db.reports";
        private const string ConnectionPgStringKey = "db.postgres";
        private readonly IConfiguration Configuration;
        private ILogger _logger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static void ConfigureContainer(IServiceContainer container)
        {
            container.RegisterFrom<ContainerCompositionRoot>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString(ConnectionStringKey);
            var connectionPgString = Configuration.GetConnectionString(ConnectionPgStringKey);

            #region Logging, tracing, metrics

            var loggerFactory = LoggingManager.Factory;
            _logger = loggerFactory.ForCurrentClass<Startup>();
            services.AddSingleton(loggerFactory);
            services.AddSingleton(_logger);

            var tracer = Tracer.Factory.CreateTracer(configurator => configurator.UseLogging(_logger))
                .ForComponent(Settings.ComponentName);
            services.AddSingleton(tracer);

            #endregion

            #region Database
            services.AddSingleton<IDistributedLockProvider>(_ =>
                new PostgresDistributedSynchronizationProvider(connectionString)
            );
            #endregion

            services.AddHealthChecks();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notifications.Host", Version = "v1" });
            });

            #region Hangfire
            services.AddHangfire(x => x.UsePostgreSqlStorage(connectionPgString));
            services.AddHangfireServer();
            services.AddEntityFrameworkNpgsql().AddDbContext<DefaultDbContext>(options =>
            {
                options.UseNpgsql(connectionPgString);
            });
            #endregion

            var settings = new Settings(Configuration);
            services.AddSingleton(factory => settings);
        }

        private static string GetServiceVersion()
        {
            return FileVersionInfo.GetVersionInfo(typeof(Startup).Assembly.Location).FileVersion;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpMetrics(new HttpRequestMetricsParameters(ServiceName, GetServiceVersion()))
                .UseGlobalExceptionHandler();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notifications.Host v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            app.UseHangfireDashboard(); //Will be available under http://localhost:9229/hangfire"

            var options = new BackgroundJobServerOptions
            {
                Queues = new[] { "daily", "weekly" }
            };
            app.UseHangfireServer(options);
        }
    }
}