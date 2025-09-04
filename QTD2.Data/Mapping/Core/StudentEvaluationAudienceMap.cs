using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class StudentEvaluationAudienceMap : Common.CommonMap<StudentEvaluationAudience>
    {
        public override void Configure(EntityTypeBuilder<StudentEvaluationAudience> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name).IsRequired().HasMaxLength(100);
        }
    }
}
