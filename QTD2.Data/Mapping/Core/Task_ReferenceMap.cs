using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Task_ReferenceMap : Common.CommonMap<Task_Reference>
    {
        public override void Configure(EntityTypeBuilder<Task_Reference> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description).IsRequired();
            builder.Property(o => o.DisplayName).HasMaxLength(50).IsRequired();
        }
    }
}
