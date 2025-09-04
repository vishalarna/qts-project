using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class CertificationSubRequirementMap : Common.CommonMap<CertificationSubRequirement>
    {
        public override void Configure(EntityTypeBuilder<CertificationSubRequirement> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Certification).WithMany(m => m.CertificationSubRequirements).HasForeignKey(o => o.CertificationId).IsRequired();
        }
    }
}
