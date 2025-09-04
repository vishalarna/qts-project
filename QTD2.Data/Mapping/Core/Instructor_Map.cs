using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Instructor_Map : Common.CommonMap<Instructor>
    {
        public override void Configure(EntityTypeBuilder<Instructor> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.InstructorNumber).HasMaxLength(50).IsRequired();
            builder.Property(o => o.InstructorName).HasMaxLength(200).IsRequired();
            builder.Property(o => o.EffectiveDate).IsRequired();
            builder.HasOne(o => o.Instructor_Category).WithMany(m => m.Instructors).HasForeignKey(k => k.ICategoryId).IsRequired();
        }
    }
}
