using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TestItemSpecs
{
    public class TestItemTypeIdRequiredSpec : ISpecification<TestItem>
    {
        public bool IsSatisfiedBy(TestItem entity, params object[] args)
        {
            return entity.TestItemTypeId > 0;
        }
    }
}
