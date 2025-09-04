using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.ReportSkeleton
{
    public class ReportSkeletonFilter_MaxOptionRequiredIfPropertyTypeSpec : ISpecification<QTD2.Domain.Entities.Core.ReportSkeletonFilter>
    {
        public bool IsSatisfiedBy(QTD2.Domain.Entities.Core.ReportSkeletonFilter entity, params object[] args)
        {
            return entity.MaxOption != DateTime.MaxValue;
        }
    }
}
