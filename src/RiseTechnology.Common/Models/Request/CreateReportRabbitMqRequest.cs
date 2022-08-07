using RiseTechnology.Common.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseTechnology.Common.Models.Request
{
    public class CreateReportRabbitMqRequest
    {
        public Guid ReportUUID { get; set; }

        public List<PersonContactInformationResponseModel> ContactItems {get;set;}
    }
}
