using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class ProcedureReview_EmployeeMap : Common.CommonMap<ProcedureReview_Employee>
    {
        public override void Configure(EntityTypeBuilder<ProcedureReview_Employee> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Employee).WithMany(o => o.ProcedureReview_Employee).HasForeignKey(k => k.EmployeeId).IsRequired(); // Relation
            builder.HasOne(o => o.ProcedureReview).WithMany(o => o.ProcedureReview_Employee).HasForeignKey(k => k.ProcedureReviewId).IsRequired(); // Relation
            builder.Property(o=>o.IsCompleted).HasDefaultValue(false);
        }
    }
}
