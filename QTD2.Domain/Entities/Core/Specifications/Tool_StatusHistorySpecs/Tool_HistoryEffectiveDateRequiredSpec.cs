using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Tool_StatusHistorySpecs
{
    public class Tool_HistoryEffectiveDateRequiredSpec : ISpecification<Tool_StatusHistory>
    {
        public bool IsSatisfiedBy(Tool_StatusHistory entity, params object[] args)
        {
            return entity.ChangeEffectiveDate != DateTime.MinValue;
        }
    }
}
