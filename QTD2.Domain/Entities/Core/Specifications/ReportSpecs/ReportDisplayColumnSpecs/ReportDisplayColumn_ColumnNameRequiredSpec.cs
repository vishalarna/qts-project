using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.Report.ReportDisplayColumn
{
   public class ReportDisplayColumn_ColumnNameRequired : ISpecification<QTD2.Domain.Entities.Core.ReportDisplayColumn>
    {
        public bool IsSatisfiedBy(QTD2.Domain.Entities.Core.ReportDisplayColumn entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.ColumnName);
        }
    }
}
