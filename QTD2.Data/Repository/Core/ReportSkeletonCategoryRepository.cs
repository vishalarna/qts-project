using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ReportSkeletonCategoryRepository : Common.Repository<ReportSkeleton_Category>, IReportSkeletonCategoryRepository
    {
        public ReportSkeletonCategoryRepository(QTDContext context)
           : base(context)
        {
        }
    }
}
