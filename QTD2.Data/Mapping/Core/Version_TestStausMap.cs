using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Version_TestStausMap : Common.CommonMap<Version_TestStaus>
    {
        public override void Configure(EntityTypeBuilder<Version_TestStaus> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Description);
            builder.Property(p => p.Version_Number);
            builder.HasOne(o => o.TestStatus).WithMany(k => k.Version_TestStauses).HasForeignKey(f => f.TestStatusId).IsRequired();
        }
    }
}
