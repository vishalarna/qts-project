using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.DutyArea_HistorySpecs
{
    public class DutyAreaIDRequiredSpecs : ISpecification<DutyArea_History>
    {
        public bool IsSatisfiedBy(DutyArea_History entity, params object[] args)
        {
            return entity.DutyAreaId > 0;
        }
    }
}
