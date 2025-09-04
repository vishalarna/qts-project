using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.CoverSheetTypeSpecs
{
    public class CoverSheetTypeNameRequiredSpec : ISpecification<CoverSheetType>
    {
        public bool IsSatisfiedBy(CoverSheetType entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Name);
        }
    }
}
