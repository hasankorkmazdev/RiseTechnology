using AutoMapper;
using RiseTechnology.Common.Models.Response;

namespace RiseTechnology.Report.API.MapProfile
{
    public class ReportApiMapperProfile: Profile
    {
        public ReportApiMapperProfile() { 
        CreateMap<Context.DbEntities.Report, ReportResponseModel>();
        }
    }
}
