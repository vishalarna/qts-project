using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.ReportSkeleton
{
    public class ReportSkeletonColumn_ReportSkeletonIdRequiredSpec: ISpecification<QTD2.Domain.Entities.Core.ReportSkeletonColumn>
    {
        public bool IsSatisfiedBy(QTD2.Domain.Entities.Core.ReportSkeletonColumn entity, params object[] args)
        {
            return entity.ReportSkeletonId >= 0;
        }
    }
}
