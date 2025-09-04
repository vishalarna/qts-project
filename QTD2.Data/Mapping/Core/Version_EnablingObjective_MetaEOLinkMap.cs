using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Version_EnablingObjective_MetaEOLinkMap : Common.CommonMap<Version_EnablingObjective_MetaEOLink>
    {
        public override void Configure(EntityTypeBuilder<Version_EnablingObjective_MetaEOLink> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Version_Number);
            builder.HasOne(o => o.Version_EnablingObjective).WithMany().HasForeignKey(k => k.Version_EnablingObjectiveId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            builder.HasOne(o => o.Version_MetaEO).WithMany(m => m.Version_EnablingObjective_MetaEOLinks).HasForeignKey(k => k.Version_MetaEOId).IsRequired();
        }
    }
}
