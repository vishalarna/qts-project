using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class CBTScormUploadMap : Common.CommonMap<CBT_ScormUpload>
    {
        public override void Configure(EntityTypeBuilder<CBT_ScormUpload> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.CBT).WithMany(s => s.ScormUploads).HasForeignKey(k => k.CbtId).IsRequired();
        }
    }
}
