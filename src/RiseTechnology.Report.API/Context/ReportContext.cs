
using Microsoft.EntityFrameworkCore;
using RiseTechnology.Common.Tools.Extensions;

namespace RiseTechnology.Report.API.Context
{
    public class ReportContext : DbContext
    {
        public ReportContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddSoftDelete();

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<API.Context.DbEntities.Report> Reports { get; set; }
    }

}
