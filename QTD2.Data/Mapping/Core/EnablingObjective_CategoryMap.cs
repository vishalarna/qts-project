using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class EnablingObjective_CategoryMap : Common.CommonMap<EnablingObjective_Category>
    {
        public override void Configure(EntityTypeBuilder<EnablingObjective_Category> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description);
            builder.Property(o => o.Number).IsRequired();
            builder.Property(o => o.Title).IsRequired();
            //builder.Navigation(o => o.EnablingObjective_SubCategories).AutoInclude();
        }
    }
}
