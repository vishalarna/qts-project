using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class TestReleaseEMPSetting_Retake_LinkMap : Common.CommonMap<TestReleaseEMPSetting_Retake_Link>
    {
        public override void Configure(EntityTypeBuilder<TestReleaseEMPSetting_Retake_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.RetakeTest).WithMany(w => w.TestReleaseEMPSetting_Retake_Links).HasForeignKey(f => f.RetakeTestId).IsRequired();
            builder.HasOne(o => o.TestReleaseEMPSettings).WithMany(w => w.TestReleaseEMPSetting_Retake_Links).HasForeignKey(f => f.TestReleaseSettingId).IsRequired();
        }
    }
}
