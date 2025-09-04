using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.CoversheetSpecs
{
    public class CoversheetInstructionsRequiredSpec : ISpecification<Coversheet>
    {
        public bool IsSatisfiedBy(Coversheet entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.CoversheetInstructions);
        }
    }
}
