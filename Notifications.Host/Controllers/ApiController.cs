using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Notifications.Host.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;

        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("DailyDigest")]
        public async Task<IActionResult> DailyDigest()
        {
            return new OkResult();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("WeeklyDigest")]
        public async Task<IActionResult> WeeklyDigest()
        {
            Host.EnqueueDailyDigest();
            return new OkResult();
        }
    }
}