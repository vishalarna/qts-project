using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class EmployeeCertificationMap : Common.CommonMap<EmployeeCertification>
    {
        public override void Configure(EntityTypeBuilder<EmployeeCertification> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Employee).WithMany(m => m.EmployeeCertifications).HasForeignKey(k => k.EmployeeId).IsRequired();
            builder.HasOne(o => o.Certification).WithMany(m => m.EmployeeCertifications).HasForeignKey(k => k.CertificationId).IsRequired();
            builder.Property(o => o.IssueDate).IsRequired();
            builder.Property(o => o.ExpirationDate);
            builder.Property(o => o.RenewalDate);
            builder.Property(o => o.CertificationNumber);

            builder.Navigation(n => n.Certification);
            builder.Navigation(n => n.Employee);
        }
    }
}
