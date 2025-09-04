using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class EnablingObjective_TopicMap : Common.CommonMap<EnablingObjective_Topic>
    {
        public override void Configure(EntityTypeBuilder<EnablingObjective_Topic> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description);
            builder.Property(o => o.Title).IsRequired();
            builder.Property(o => o.Number);

            builder.HasOne(o => o.EnablingObjectives_SubCategory).WithMany(m => m.EnablingObjective_Topics).HasForeignKey(k => k.SubCategoryId).IsRequired();

            builder.HasIndex(i => new { i.SubCategoryId, i.Number }).IsUnique().HasFilter("[Number] IS NOT NULL");
            //builder.Navigation(o => o.EnablingObjectives).AutoInclude();
        }
    }
}
