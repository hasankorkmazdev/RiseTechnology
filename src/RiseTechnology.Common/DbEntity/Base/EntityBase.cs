using System;
using System.ComponentModel.DataAnnotations;

namespace RiseTechnology.Common.DbEntity.Base
{
    public class EntityBase : IEntity
    {
        [Key]
        public Guid UUID { get; set; } = Guid.NewGuid();

    }
    public class EntityBaseAuditable : EntityBase, IEntitySoftDelete, IEntityActivable
    {
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
