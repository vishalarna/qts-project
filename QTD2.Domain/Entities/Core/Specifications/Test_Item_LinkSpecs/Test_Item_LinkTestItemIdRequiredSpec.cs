using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Test_Item_LinkSpecs
{
    public class Test_Item_LinkTestItemIdRequiredSpec : ISpecification<Test_Item_Link>
    {
        public bool IsSatisfiedBy(Test_Item_Link entity, params object[] args)
        {
            return entity.TestItemId > 0;
        }
    }
}
