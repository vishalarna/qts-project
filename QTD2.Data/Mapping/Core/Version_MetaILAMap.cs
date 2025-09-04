using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Version_MetaILAMap : Common.CommonMap<Version_MetaILA>
    {
        public override void Configure(EntityTypeBuilder<Version_MetaILA> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.MetaILAName).IsRequired().HasMaxLength(500);
            builder.Property(o => o.MetaILADesc).HasMaxLength(4000);
            builder.Property(o => o.Reason).HasMaxLength(500);

            builder.HasOne(o => o.MetaILA_Status).WithMany(m => m.Version_MetaILAs).HasForeignKey(o => o.MetaILAStatusId);
            builder.HasOne(o => o.MetaILA).WithMany(m => m.Version_MetaILAs).HasForeignKey(o => o.MetaILAId);
        }
    }
}
