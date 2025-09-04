using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TestItemMatchSpecs
{
    public class TestItemMatch_MatchDescRequiredSpec : ISpecification<TestItemMatch>
    {
        public bool IsSatisfiedBy(TestItemMatch entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.MatchDescription);
        }
    }
}
