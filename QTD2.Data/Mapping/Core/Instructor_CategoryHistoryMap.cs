using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Instructor_CategoryHistoryMap : Common.CommonMap<Instructor_CategoryHistory>
    {
        public override void Configure(EntityTypeBuilder<Instructor_CategoryHistory> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.ICategoryNotes);
            builder.Property(o => o.EffectiveDate).IsRequired();
            builder.HasOne(o => o.Instructor_Category).WithMany(x => x.Instructor_CategoryHistories).HasForeignKey(y => y.ICategoryId).IsRequired();
        }
    }
}
