using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Instructor_CategoryMap : Common.CommonMap<Instructor_Category>
    {
        public override void Configure(EntityTypeBuilder<Instructor_Category> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.ICategoryDescription);
            builder.Property(o => o.IEffectiveDate).IsRequired();
            builder.Property(o => o.ICategoryTitle).HasMaxLength(200).IsRequired();
        }
    }
}
