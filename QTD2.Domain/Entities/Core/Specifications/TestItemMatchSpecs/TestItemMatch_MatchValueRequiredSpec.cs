using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TestItemMatchSpecs
{
    public class TestItemMatch_MatchValueRequiredSpec : ISpecification<TestItemMatch>
    {
        public bool IsSatisfiedBy(TestItemMatch entity, params object[] args)
        {
            return !char.IsWhiteSpace(entity.MatchValue) && char.IsLetter(entity.MatchValue);
        }
    }
}
