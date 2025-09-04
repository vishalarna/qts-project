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
    public class SkillQualificationStatusMap : Common.CommonMap<SkillQualificationStatus>
    {
        public override void Configure(EntityTypeBuilder<SkillQualificationStatus> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name);
            builder.Property(o => o.Description);
        }
    }
}