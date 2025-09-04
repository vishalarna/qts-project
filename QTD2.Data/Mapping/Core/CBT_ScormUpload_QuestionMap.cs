using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class CBT_ScormUpload_QuestionMap : Common.CommonMap<CBT_ScormUpload_Question>
    {
        public override void Configure(EntityTypeBuilder<CBT_ScormUpload_Question> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description).IsRequired();
            builder.HasOne(o => o.CBT_ScormUpload).WithMany(e => e.CBT_ScormUpload_Question).HasForeignKey(k => k.CbtScormUploadId).IsRequired();
        }
    }
}
