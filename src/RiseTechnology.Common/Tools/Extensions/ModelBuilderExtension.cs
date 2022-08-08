using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RiseTechnology.Common.DbEntity.Base;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace RiseTechnology.Common.Tools.Extensions
{
    public static class ModelBuilderExtension
    {
        public static ModelBuilder AddSoftDelete(this ModelBuilder modelBuilder)
        {
            modelBuilder.Model.GetEntityTypes()
                       .Where(entityType => typeof(IEntitySoftDelete).IsAssignableFrom(entityType.ClrType))
                       .ToList()
                       .ForEach(entityType =>
                       {
                           Expression<Func<IEntitySoftDelete, bool>> filterExpression = x => !x.IsDeleted;
                           var param = Expression.Parameter(entityType.ClrType);
                           var newBody = ReplacingExpressionVisitor.Replace(filterExpression.Parameters.Single(), param, filterExpression.Body);

                           modelBuilder.Entity(entityType.ClrType)
                           .HasQueryFilter(Expression.Lambda(newBody, param));
                       });
            return modelBuilder;
        }
    }
}
