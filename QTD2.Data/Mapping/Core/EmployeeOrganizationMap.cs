using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class EmployeeOrganizationMap : Common.CommonMap<EmployeeOrganization>
    {
        public override void Configure(EntityTypeBuilder<EmployeeOrganization> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Employee).WithMany(o => o.EmployeeOrganizations).HasForeignKey(k => k.EmployeeId).IsRequired(); // Relation
            builder.HasOne(o => o.Organization).WithMany(o => o.EmployeeOrganizations).HasForeignKey(k => k.OrganizationId).IsRequired(); // Relation
            builder.Property(o => o.IsManager).HasDefaultValue(false);
        }
    }
}
