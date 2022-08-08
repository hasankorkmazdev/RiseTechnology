using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RiseTechnology.Common;
using RiseTechnology.Common.Models.Request;
using RiseTechnology.Report.API.Services;
using System;
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
            var data = await reportService.GetReports();

            return Ok(data);
        }
        [HttpPost()]
        public async Task<IActionResult> CreateReport()
        {
            await reportService.CreateReport();
            return Ok();
        }
        [HttpPost("{reportuuid}/changeStatus/{status}")]
        public async Task<IActionResult> ChangeStatus(Guid reportuuid, Enums.ReportStatus status)
        {
            await reportService.ChangeStatus(reportuuid, status);
            return Ok();
        }
        [HttpPost("{reportuuid}/UpdateReportPath")]
        public async Task<IActionResult> UpdateReportPath(Guid reportuuid, [FromBody] UpdateReportPathRequestModel model)
        {
           await reportService.UpdateReportPath(reportuuid, model);
            return Ok();
        }




    }
}
