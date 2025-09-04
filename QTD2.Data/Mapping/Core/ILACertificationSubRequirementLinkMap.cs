using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class ILACertificationSubRequirementLinkMap : Common.CommonMap<ILACertificationSubRequirementLink>
    {
        public override void Configure(EntityTypeBuilder<ILACertificationSubRequirementLink> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.CertificationLink).WithMany(w => w.ILACertificationSubRequirementLink).HasForeignKey(f => f.ILACertificationLinkId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasOne(o => o.CertificationSubRequirement).WithMany(w => w.ILACertificationSubRequirementLinks).HasForeignKey(f => f.CertificationSubRequirementId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

        }
    }
}
