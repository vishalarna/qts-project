using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class MetaILA_Employee_MemberLinkFufillmentMap : Common.CommonMap<MetaILA_Employee_MemberLinkFufillment>
    {
        public override void Configure(EntityTypeBuilder<MetaILA_Employee_MemberLinkFufillment> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.MetaILA_EmployeeId).IsRequired();
            builder.Property(o => o.Meta_ILAMembers_LinkId).IsRequired();
            builder.Property(o => o.FufilledBy_ClassScheduleEmployeeId);

            builder.HasOne(o => o.MetaILA_Employee).WithMany(s => s.MetaILA_Employee_MemberLinkFufillments).HasForeignKey(f => f.MetaILA_EmployeeId).IsRequired();
            builder.HasOne(o => o.Meta_ILAMembers_Link).WithMany().HasForeignKey(f => f.Meta_ILAMembers_LinkId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            builder.HasOne(o => o.FufilledBy_ClassScheduleEmployee).WithMany().HasForeignKey(f => f.FufilledBy_ClassScheduleEmployeeId);
        }
    }
}
