using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.Report.ReportFilter
{
    class ReportFilter_PropertyTypeRequiredSpec : ISpecification<QTD2.Domain.Entities.Core.ReportFilter>
    {
        public bool IsSatisfiedBy(QTD2.Domain.Entities.Core.ReportFilter entity, params object[] args)
        {
            return entity.PropertyType > 0;
        }
    }
}
