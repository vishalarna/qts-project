using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.Report.ReportFilter
{
   public class ReportFilter_ReportIdRequiredSpec: ISpecification<QTD2.Domain.Entities.Core.ReportFilter>
    {
        public bool IsSatisfiedBy(QTD2.Domain.Entities.Core.ReportFilter entity, params object[] args)
        {
        return entity.ReportId > 0;
        }
}
}
