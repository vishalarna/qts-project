using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class EnablingObjective_SubCategoryHistoryMap : Common.CommonMap<EnablingObjective_SubCategoryHistory>
    {
        public override void Configure(EntityTypeBuilder<EnablingObjective_SubCategoryHistory> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.OldStatus).IsRequired();
            builder.Property(o => o.NewStatus).IsRequired();
            builder.Property(o => o.ChangeEffectiveDate).IsRequired();
            builder.Property(o => o.ChangeNotes);
            builder.HasOne(o => o.EnablingObjective_SubCategory).WithMany(x => x.EnablingObjective_SubCategoryHistories).HasForeignKey(y => y.EnablingObjectiveSubCategoryId).IsRequired();
        }
    }
}
