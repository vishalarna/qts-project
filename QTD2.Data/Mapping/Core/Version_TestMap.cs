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
    public class Version_TestMap : Common.CommonMap<Version_Test>
    {
        public override void Configure(EntityTypeBuilder<Version_Test> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.TestTitle);
            builder.Property(o => o.Version_Number);
            builder.Property(o => o.State);
            builder.Property(o => o.IsInUse);
            builder.HasOne(p => p.Test).WithMany(m => m.Version_Tests).HasForeignKey(k => k.TestId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.TestStatus).WithMany(m => m.Version_Tests).HasForeignKey(k => k.TestStatusId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
