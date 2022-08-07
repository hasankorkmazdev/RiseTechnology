using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RiseTechnology.Report.API.Services;
using System.Threading.Tasks;

namespace RiseTechnology.Report.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IReportService reportService;

        public ReportController(ILogger<ReportController> logger, IReportService reportService)
        {
            _logger = logger;
            this.reportService = reportService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetReport()
        {
          var data=  await reportService.GetReports();

            return Ok(data);
        }
        [HttpPost()]
        public async Task<IActionResult> CreateReport()
        {
            await reportService.CreateReport();
            return Ok();
        }


    }
}
