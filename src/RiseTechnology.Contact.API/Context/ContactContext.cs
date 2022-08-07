
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RiseTechnology.Common.DbEntity.Base;
using RiseTechnology.Contact.API.Context.DbEntities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace RiseTechnology.Contact.API.Context
{
    public class ContactContext :DbContext
    {
        public ContactContext(DbContextOptions options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Model.GetEntityTypes()
                       .Where(entityType => typeof(IEntitySoftDelete).IsAssignableFrom(entityType.ClrType))
                       .ToList()
                       .ForEach(entityType =>
                       {
                           modelBuilder.Entity(entityType.ClrType)
                           .HasQueryFilter(ConvertFilterExpression<IEntitySoftDelete>(e => !e.IsDeleted, entityType.ClrType));
                       });
       
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<ContactInformation> ContactInformations { get; set; }
        public DbSet<Person> Persons { get; set; }
        private static LambdaExpression ConvertFilterExpression<TInterface>(
                            Expression<Func<TInterface, bool>> filterExpression,
                            Type entityType)
        {
            var newParam = Expression.Parameter(entityType);
            var newBody = ReplacingExpressionVisitor.Replace(filterExpression.Parameters.Single(), newParam, filterExpression.Body);

            return Expression.Lambda(newBody, newParam);
        }
    }

}
