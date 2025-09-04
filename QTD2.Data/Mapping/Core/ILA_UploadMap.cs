using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ILA_UploadMap : Common.CommonMap<ILA_Upload>
    {
        public override void Configure(EntityTypeBuilder<ILA_Upload> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ila).WithMany(x => x.ILA_Uploads).HasForeignKey(i => i.ILAId).IsRequired();
            builder.Property(o => o.FileAsBase64);
            builder.Property(o => o.FileName);
            builder.Property(o => o.FileSize);
            builder.Property(o => o.FileSize);
        }
    }
}
