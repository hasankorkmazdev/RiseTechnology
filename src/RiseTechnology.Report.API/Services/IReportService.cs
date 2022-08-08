using RiseTechnology.Common;
using RiseTechnology.Common.Models.Base;
using RiseTechnology.Common.Models.Request;
using RiseTechnology.Common.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RiseTechnology.Report.API.Services
{
    public interface IReportService
    {
        public Task<ServiceResultModel> CreateReport();
        public Task<ServiceResultModel> GetReports();
        public Task<ServiceResultModel> ChangeStatus(Guid reportuuid, Enums.ReportStatus status);
        public Task<ServiceResultModel> UpdateReportPath(Guid reportuuid, UpdateReportPathRequestModel model);


    }
}
