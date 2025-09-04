using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Instructor_HistoryMap : Common.CommonMap<Instructor_History>
    {
        public override void Configure(EntityTypeBuilder<Instructor_History> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.InstructorNotes);
            builder.Property(o => o.EffectiveDate).IsRequired();
            builder.HasOne(o => o.Instructor).WithMany(x => x.Instructor_Histories).HasForeignKey(y => y.InstructorId).IsRequired();
        }
    }
}
