namespace RiseTechnology.Common.DbEntity.Base
{
    public interface IEntitySoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
