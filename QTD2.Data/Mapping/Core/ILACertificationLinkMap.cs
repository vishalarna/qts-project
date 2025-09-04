using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class ILACertificationLinkMap : Common.CommonMap<ILACertificationLink>
    {
        public override void Configure(EntityTypeBuilder<ILACertificationLink> builder)
        {
            base.Configure(builder);
            //builder.HasOne(o => o.RatingScaleNInfo).WithMany(x => x.RatingScaleExpanded).HasForeignKey(y => y.RatingScaleNId).IsRequired();
            //builder.Property(o => o.Description).IsRequired();

        }
    }
}
