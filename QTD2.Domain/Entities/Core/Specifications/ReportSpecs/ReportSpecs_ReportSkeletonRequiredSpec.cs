using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.Report
{
   public class ReportSpecs_ReportSkeletonRequiredSpec: ISpecification<QTD2.Domain.Entities.Core.Report>
    {
            public bool IsSatisfiedBy(QTD2.Domain.Entities.Core.Report entity, params object[] args)
            {
                return entity.ReportSkeletonId > 0;
            }
        }
    }

