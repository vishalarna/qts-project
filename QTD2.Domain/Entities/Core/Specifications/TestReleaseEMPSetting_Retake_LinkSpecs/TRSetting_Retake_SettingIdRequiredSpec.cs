using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TestReleaseEMPSetting_Retake_LinkSpecs
{
    public class TRSetting_Retake_SettingIdRequiredSpec : ISpecification<TestReleaseEMPSetting_Retake_Link>
    {
        public bool IsSatisfiedBy(TestReleaseEMPSetting_Retake_Link entity, params object[] args)
        {
            return entity.TestReleaseSettingId > 0;
        }
    }
}
