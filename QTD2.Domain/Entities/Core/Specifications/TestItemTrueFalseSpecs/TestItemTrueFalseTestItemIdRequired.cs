using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TestItemTrueFalseSpecs
{
    public class TestItemTrueFalseTestItemIdRequired : ISpecification<TestItemTrueFalse>
    {
        public bool IsSatisfiedBy(TestItemTrueFalse entity, params object[] args)
        {
            return entity.TestItemId > 0;
        }
    }
}