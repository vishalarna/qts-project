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
    public class ILA_EnablingObjective_LinkMap : Common.CommonMap<ILA_EnablingObjective_Link>
    {
        public override void Configure(EntityTypeBuilder<ILA_EnablingObjective_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ILA).WithMany(m => m.ILA_EnablingObjective_Links).HasForeignKey(k => k.ILAId).IsRequired();
            builder.HasOne(o => o.EnablingObjective).WithMany(m => m.ILA_EnablingObjective_Links).HasForeignKey(k => k.EnablingObjectiveId).IsRequired();

            builder.HasIndex(i => new { i.ILAId, i.EnablingObjectiveId }).IsUnique().HasFilter("[Deleted] = 0");
        }
    }
}
