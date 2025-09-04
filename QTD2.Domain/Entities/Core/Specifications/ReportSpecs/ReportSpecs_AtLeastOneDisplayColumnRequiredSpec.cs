using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.Report
{
  public class ReportSpecs_AtLeastOneDisplayColumnRequiredSpec: ISpecification<QTD2.Domain.Entities.Core.Report>
    {
        public bool IsSatisfiedBy(QTD2.Domain.Entities.Core.Report entity, params object[] args)
        {
            if (entity.DisplayColumns.Count > 0)
            {
                return true;
            }

            return false;
        }
    }
}
