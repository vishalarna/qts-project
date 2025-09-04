using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TestSpecs
{
    public class TestStatusIDRequiredSpec : ISpecification<Test>
    {
        public bool IsSatisfiedBy(Test entity, params object[] args)
        {
            return entity.TestStatusId > 0;
        }
    }
}
