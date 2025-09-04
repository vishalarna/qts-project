using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TestStatusSpecs
{
    public class TestStatusDescRequiredSpec : ISpecification<TestStatus>
    {
        public bool IsSatisfiedBy(TestStatus entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Description);
        }
    }
}
