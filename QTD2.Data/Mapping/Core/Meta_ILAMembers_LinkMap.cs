using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
    public class Meta_ILAMembers_LinkMap : Common.CommonMap<Meta_ILAMembers_Link>
    {
        public override void Configure(EntityTypeBuilder<Meta_ILAMembers_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ILA).WithMany(m => m.Meta_ILAMembers_Links).HasForeignKey(k => k.ILAID).IsRequired();
            builder.HasOne(o => o.MetaILA).WithMany(m => m.Meta_ILAMembers_Links).HasForeignKey(k => k.MetaILAID).IsRequired();
            builder.HasOne(o => o.MetaILAConfigurationPublishOption).WithMany(m => m.Meta_ILAMembers_Links).HasForeignKey(k => k.MetaILAConfigPublishOptionID);
            builder.Property(o => o.SequenceNumber).IsRequired();
            builder.HasIndex(i => new { i.ILAID, i.MetaILAID, i.MetaILAConfigPublishOptionID }).IsUnique();
            //builder.HasOne(o => o.MetaILA).WithMany(m => m.Meta_ILAMembers_Links).HasForeignKey(k => k.MetaILAID).IsRequired().OnDelete(DeleteBehavior.NoAction);
            //builder.HasIndex(i => new { i.ILAID, i.MetaILAID }).IsUnique();
        }
    }
}
