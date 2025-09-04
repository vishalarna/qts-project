using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class EmployeePositionMap : Common.CommonMap<EmployeePosition>
    {
        public override void Configure(EntityTypeBuilder<EmployeePosition> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Employee).WithMany(o => o.EmployeePositions).HasForeignKey(k => k.EmployeeId).IsRequired(); // Relation
            builder.HasOne(o => o.Position).WithMany(o => o.EmployeePositions).HasForeignKey(k => k.PositionId).IsRequired(); // Relation
            builder.Property(o => o.StartDate).IsRequired();
            builder.Property(o => o.EndDate);
            builder.Property(o => o.QualificationDate);
            builder.Property(o => o.ManagerName).HasMaxLength(50);
            builder.Property(o => o.TrainingProgramVersion).HasMaxLength(50);
            builder.Property(n => n.IsSignificant).IsRequired();
            builder.Property(n => n.IsCertificationNotRequired).HasDefaultValue(false);
        }
    }
}
