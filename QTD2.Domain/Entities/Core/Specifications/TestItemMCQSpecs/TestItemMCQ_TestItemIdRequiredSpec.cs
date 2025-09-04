using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TestItemMCQSpecs
{
    public class TestItemMCQ_TestItemIdRequiredSpec : ISpecification<TestItemMCQ>
    {
        public bool IsSatisfiedBy(TestItemMCQ entity, params object[] args)
        {
            return entity.TestItemId > 0;
        }
    }
}
