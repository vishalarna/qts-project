using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Common;

namespace QTD2.Data.Mapping.Common
{
    public abstract class CommonMap<TEntityBase> : IEntityTypeConfiguration<TEntityBase>
        where TEntityBase : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntityBase> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Active).IsRequired();
            builder.Property(o => o.Deleted).HasDefaultValue(false).IsRequired();
            builder.Property(o => o.CreatedBy);
            builder.Property(o => o.CreatedDate);
            builder.Property(o => o.ModifiedBy);
            builder.Property(o => o.ModifiedDate);
            builder.HasQueryFilter(f => !f.Deleted);
        }
    }
}
