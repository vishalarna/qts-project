using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.NercStandardSpecs
{
    public class NercStandardNameRequiredSpec : ISpecification<NercStandard>
    {
        public bool IsSatisfiedBy(NercStandard entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Name);
        }
    }
}
