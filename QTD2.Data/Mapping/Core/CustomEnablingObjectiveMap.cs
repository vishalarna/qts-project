using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class CustomEnablingObjectiveMap : Common.CommonMap<CustomEnablingObjective>
    {
        public override void Configure(EntityTypeBuilder<CustomEnablingObjective> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description);
            builder.Property(o => o.IsAddtoEO).HasDefaultValue(false);
            builder.HasOne(o => o.EnablingObjective_Topic).WithMany(m => m.CustomEnablingObjectives).HasForeignKey(k => k.EO_TopicId);
            builder.HasOne(o => o.EnablingObjective_Category).WithMany(m => m.CustomEnablingObjectives).HasForeignKey(k => k.EO_CatId);
            builder.HasOne(o => o.EnablingObjective_SubCategory).WithMany(m => m.CustomEnablingObjectives).HasForeignKey(k => k.EO_SubCatId);
        }
    }
}
