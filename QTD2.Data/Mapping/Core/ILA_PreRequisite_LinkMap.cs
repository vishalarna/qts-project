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
    public class ILA_PreRequisite_LinkMap : Common.CommonMap<ILA_PreRequisite_Link>
    {
        public override void Configure(EntityTypeBuilder<ILA_PreRequisite_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ILA).WithMany(m => m.ILA_PreRequisite_Links).HasForeignKey(k => k.ILAId).IsRequired();
            builder.HasOne(o => o.PreRequisite).WithMany(k => k.DependentILAs).HasForeignKey(k => k.PreRequisiteId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            builder.HasIndex(i => new { i.ILAId, i.PreRequisiteId }).IsUnique();
        }
    }
}
