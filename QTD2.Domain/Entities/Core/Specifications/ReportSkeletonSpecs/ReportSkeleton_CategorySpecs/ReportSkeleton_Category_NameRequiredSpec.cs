using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ReportSkeletonSpecs.ReportSkeleton_CategorySpecs
{
    public class ReportSkeleton_Category_NameRequiredSpec : ISpecification<QTD2.Domain.Entities.Core.ReportSkeleton_Category>
    {
        public bool IsSatisfiedBy(QTD2.Domain.Entities.Core.ReportSkeleton_Category entity, params object[] args)
    {
        return !string.IsNullOrEmpty(entity.Name);
    }
}
}
