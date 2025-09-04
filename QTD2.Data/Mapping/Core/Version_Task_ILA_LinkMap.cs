using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class Version_Task_ILA_LinkMap : Common.CommonMap<Version_Task_ILA_Link>
    {
        public override void Configure(EntityTypeBuilder<Version_Task_ILA_Link> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.VersionNumber).HasMaxLength(20);
            builder.HasOne(o => o.Version_Task).WithMany(m => m.Version_Task_ILA_Links).IsRequired();
            builder.HasOne(o => o.Version_ILA).WithMany(m => m.Version_Task_ILA_Links).IsRequired();
        }
    }
}
