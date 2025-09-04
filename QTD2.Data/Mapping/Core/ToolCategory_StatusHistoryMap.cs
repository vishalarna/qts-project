using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class ToolCategory_StatusHistoryMap : Common.CommonMap<ToolCategory_StatusHistory>
    {
        public override void Configure(EntityTypeBuilder<ToolCategory_StatusHistory> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ToolCategory).WithMany(x => x.ToolCategories_StatusHistories).HasForeignKey(y => y.ToolCategoryId).IsRequired();
            //builder.Property(o => o.Description).IsRequired();

        }
    }
}
