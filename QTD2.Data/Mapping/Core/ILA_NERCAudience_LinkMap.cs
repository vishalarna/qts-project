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
    public class ILA_NERCAudience_LinkMap : Common.CommonMap<ILA_NERCAudience_Link>
    {
        public override void Configure(EntityTypeBuilder<ILA_NERCAudience_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ILA).WithMany(m => m.ILA_NERCAudience_Links).HasForeignKey(k => k.ILAId).IsRequired();
            builder.HasOne(o => o.NERCTargetAudience).WithMany(m => m.ILA_NERCAudience_Links).HasForeignKey(k => k.NERCAudienceID).IsRequired();

            builder.HasIndex(i => new { i.ILAId, i.NERCAudienceID }).IsUnique();
        }
    }
}
