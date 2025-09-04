using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class CBT_ScormUpload_Question_ChoiceMap : Common.CommonMap<CBT_ScormUpload_Question_Choice>
    {
        public override void Configure(EntityTypeBuilder<CBT_ScormUpload_Question_Choice> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Choice).IsRequired();
            builder.Property(e => e.CorrectChoice).IsRequired();
            builder.HasOne(o => o.CBT_ScormUpload_Question).WithMany(e => e.CBT_ScormUpload_Question_Choices).HasForeignKey(k => k.CBTScormUploadQuestionId).IsRequired();
        }
    }
}
