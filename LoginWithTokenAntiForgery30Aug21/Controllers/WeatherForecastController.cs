using LoginWithTokenAntiForgery30Aug21.DTOs.Test.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginWithTokenAntiForgery30Aug21.Controllers
{
    [ApiController]
    [Route("api/weather")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        //[IgnoreAntiforgeryToken]
        [ValidateAntiForgeryToken]
        public IActionResult Post(TestRequest request) => Ok(Summaries[request.Transaction%10]);
    }
}
