using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Module.Reporting.Providers.Contracts;

namespace Notifications.DataProvider
{
    public class DigestDailyDataProvider
    {
        private readonly IDigestService _digestService;

        public DigestDailyDataProvider(IDigestService digestService)
        {
            _digestService = digestService;
        }

        public async Task GetData()
        {
            var data = await GetDigestDailyReport();
            Console.Write(data.Count);

           // BackgroundJob.Enqueue<DigestDailyEmailBuilder>(d => d.BuildEmail(data));
        }

        private async Task<IReadOnlyCollection<DigestDailyReport>> GetDigestDailyReport()
        {
            var token = new CancellationToken();
            return await _digestService.GetDigestDailyReport(null, token, -125);
        }
    }
}