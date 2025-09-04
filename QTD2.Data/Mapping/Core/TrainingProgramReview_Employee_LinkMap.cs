using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class TrainingProgramReview_Employee_LinkMap : Common.CommonMap<TrainingProgramReview_Employee_Link>
    {
        public override void Configure(EntityTypeBuilder<TrainingProgramReview_Employee_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.TrainingProgramReview).WithMany(x => x.Reviewers).HasForeignKey(y => y.TrainingProgramReviewId).IsRequired();
            builder.HasOne(o => o.Employee).WithMany().HasForeignKey(y => y.EmployeeId).IsRequired();
        }
    }
}
