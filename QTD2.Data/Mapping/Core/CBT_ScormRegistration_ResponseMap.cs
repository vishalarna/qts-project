using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class CBT_ScormRegistration_ResponseMap : Common.CommonMap<CBT_ScormRegistration_Response>
    {
        public override void Configure(EntityTypeBuilder<CBT_ScormRegistration_Response> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.CBT_ScormRegistration).WithMany(e => e.CBT_ScormRegistration_Responses).HasForeignKey(k => k.CBTScormRegistrationId).IsRequired();
            builder.HasOne(o => o.CBT_ScormUpload_Question_Choice).WithMany(e => e.CBT_ScormRegistration_Responses).HasForeignKey(k => k.CBTScormUploadQuestionChoiceId).OnDelete(DeleteBehavior.NoAction).IsRequired();
        }
    }
}
