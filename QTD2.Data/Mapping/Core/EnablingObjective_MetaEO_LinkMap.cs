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
    public class EnablingObjective_MetaEO_LinkMap : Common.CommonMap<EnablingObjective_MetaEO_Link>
    {
        public override void Configure(EntityTypeBuilder<EnablingObjective_MetaEO_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.EnablingObjective).WithMany().HasForeignKey(k => k.EOID).OnDelete(DeleteBehavior.Restrict).IsRequired();
            builder.HasOne(o => o.MetaEO).WithMany(m => m.EnablingObjective_MetaEO_Links).HasForeignKey(k => k.MetaEOId).IsRequired();

            builder.HasIndex(i => new { i.MetaEOId, i.EOID }).IsUnique();
        }
    }
}
