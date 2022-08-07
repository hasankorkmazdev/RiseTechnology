using RiseTechnology.Common.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RiseTechnology.Common.Enums;

namespace RiseTechnology.Common.Models.Response
{
    public class ReportResponseModel : BaseModel
    {
        public DateTime RequestDate { get; set; }
        public ReportStatus Status { get; set; }
    }
}
