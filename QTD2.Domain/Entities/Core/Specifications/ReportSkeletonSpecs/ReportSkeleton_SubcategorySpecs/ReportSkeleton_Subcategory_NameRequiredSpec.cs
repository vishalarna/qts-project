using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ReportSkeletonSpecs.ReportSkeleton_SubcategorySpecs
{
    public class ReportSkeleton_Subcategory_NameRequiredSpec : ISpecification<QTD2.Domain.Entities.Core.ReportSkeleton_Subcategory>
    {
        public bool IsSatisfiedBy(QTD2.Domain.Entities.Core.ReportSkeleton_Subcategory entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Name);
        }
    }
}
