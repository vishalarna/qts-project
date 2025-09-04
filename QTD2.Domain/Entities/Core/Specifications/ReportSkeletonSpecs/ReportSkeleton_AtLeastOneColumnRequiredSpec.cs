using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.ReportSkeleton
{
    public class ReportSkeleton_AtLeastOneColumnRequiredSpec : ISpecification<QTD2.Domain.Entities.Core.ReportSkeleton>
    {
        public bool IsSatisfiedBy(QTD2.Domain.Entities.Core.ReportSkeleton entity, params object[] args)
        {
            if (entity.DisplayColumns.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
