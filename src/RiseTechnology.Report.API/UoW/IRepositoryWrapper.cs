using RiseTechnology.Common.GenericRepository;

namespace RiseTechnology.Report.API.UoW
{
    public interface IRepositoryWrapper
    {
        public IGenericRepository<Context.DbEntities.Report> ReportRepository { get; }
    }
}
