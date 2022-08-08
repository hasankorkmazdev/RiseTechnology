using RiseTechnology.Common.DbEntity.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using static RiseTechnology.Common.Enums;

namespace RiseTechnology.Report.API.Context.DbEntities
{
    [Table(name: "Report", Schema = "RiseReport")]
    public class Report : EntityBaseAuditable
    {
        public DateTime RequestDate { get; set; }
        public ReportStatus Status { get; set; }
        public string XLSXPath { get; set; }
    }

}
