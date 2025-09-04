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
    public class TestReleaseEMPSettingsMap : Common.CommonMap<TestReleaseEMPSettings>
    {
        public override void Configure(EntityTypeBuilder<TestReleaseEMPSettings> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ILA).WithOne(w => w.TestReleaseEMPSettings).HasForeignKey<TestReleaseEMPSettings>(f => f.ILAId).IsRequired();
            builder.HasOne(o => o.FinalTest).WithMany().HasForeignKey(f => f.FinalTestId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.PreTest).WithMany(x => x.TestReleaseEMPSettings).HasForeignKey(f => f.PreTestId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.EmpSettingsReleaseType).WithMany().HasForeignKey(f => f.EmpSettingsReleaseTypeId);
        }
    }
}
