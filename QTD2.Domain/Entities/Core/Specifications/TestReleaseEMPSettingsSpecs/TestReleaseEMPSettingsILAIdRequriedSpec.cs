using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TestReleaseEMPSettingsSpecs
{
    public class TestReleaseEMPSettingsILAIdRequriedSpec : ISpecification<TestReleaseEMPSettings>
    {
        public bool IsSatisfiedBy(TestReleaseEMPSettings entity, params object[] args)
        {
            return entity.ILAId > 0;
        }
    }
}
