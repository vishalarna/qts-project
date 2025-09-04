using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class EnablingObjective_SubCategoryMap : Common.CommonMap<EnablingObjective_SubCategory>
    {
        public override void Configure(EntityTypeBuilder<EnablingObjective_SubCategory> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description);
            builder.Property(o => o.Title).IsRequired();
            builder.Property(o => o.Number);

            builder.HasOne(o => o.EnablingObjectives_Category).WithMany(m => m.EnablingObjective_SubCategories).HasForeignKey(k => k.CategoryId).IsRequired();

            builder.HasIndex(i => new { i.CategoryId, i.Number }).IsUnique().HasFilter("[Number] IS NOT NULL");
            //builder.Navigation(o => o.EnablingObjective_Topics).AutoInclude();
            //builder.Navigation(o => o.EnablingObjectives).AutoInclude();
        }
    }
}
