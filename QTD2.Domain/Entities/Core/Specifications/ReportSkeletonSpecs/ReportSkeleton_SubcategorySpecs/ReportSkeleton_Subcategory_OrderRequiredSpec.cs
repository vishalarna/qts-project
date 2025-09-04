using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ReportSkeletonSpecs.ReportSkeleton_SubcategorySpecs
{
   public class ReportSkeleton_Subcategory_OrderRequiredSpec : ISpecification<QTD2.Domain.Entities.Core.ReportSkeleton_Subcategory_Report>
    {
        public bool IsSatisfiedBy(QTD2.Domain.Entities.Core.ReportSkeleton_Subcategory_Report entity, params object[] args)
        {
            return entity.ReportSkeleton_SubcategoryId > 0;
        }
    }
}
