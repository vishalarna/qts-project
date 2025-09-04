using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ILAHistorySpecs
{
    public class ILAHistoryILAIdRequiredSpec : ISpecification<ILAHistory>
    {
        public bool IsSatisfiedBy(ILAHistory entity, params object[] args)
        {
            return entity.ILAId > 0;
        }
    }
}
