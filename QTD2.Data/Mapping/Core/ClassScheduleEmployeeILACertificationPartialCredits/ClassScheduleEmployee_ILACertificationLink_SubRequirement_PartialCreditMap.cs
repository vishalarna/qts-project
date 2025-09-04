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
    public class ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditMap : Common.CommonMap<ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredit>
    {
        public override void Configure(EntityTypeBuilder<ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredit> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.ClassScheduleEmployee_ILACertificationLink_PartialCreditId).IsRequired();
            builder.Property(o => o.ILACertificationSubRequirementLinkId).IsRequired();
            builder.Property(o => o.PartialCreditHours);
            builder.HasOne(o => o.ClassScheduleEmployee_ILACertificationLink_PartialCredit).WithMany(m => m.ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits).HasForeignKey(k => k.ClassScheduleEmployee_ILACertificationLink_PartialCreditId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.ILACertificationSubRequirementLink).WithMany(m => m.ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits).HasForeignKey(k => k.ILACertificationSubRequirementLinkId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}