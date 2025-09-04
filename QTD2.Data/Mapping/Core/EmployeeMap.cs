using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    internal class EmployeeMap : Common.CommonMap<Employee>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.EmployeeNumber);
            builder.HasOne(o => o.Person).WithOne(o => o.Employee).HasForeignKey<Employee>(k => k.PersonId).IsRequired();
            builder.Property(o => o.Address).HasMaxLength(100);
            builder.Property(o => o.City).HasMaxLength(50);
            builder.Property(o => o.State).HasMaxLength(50);
            builder.Property(o => o.WorkLocation).HasMaxLength(50);
            //builder.Property(o => o.Notes).HasMaxLength(50);
            builder.Property(o => o.Password).HasMaxLength(50);
            builder.HasIndex(e => e.PersonId).IsUnique().HasFilter("[Deleted] = 0");
        }
    }
}
