using RiseTechnology.Common.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RiseTechnology.Report.API.Services
{
    public interface IReportService
    {
        public Task CreateReport();
        public Task<List<ReportResponseModel>> GetReports();
    }
}
