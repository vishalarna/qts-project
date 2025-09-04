using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Infrastructure.ExtensionMethods;
using QTD2.Test.Data.Domain.Entities.Core;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class RegulatoryRequirementTest
    {
        public static IEnumerable<object[]> GetRegulatoryRequirement_ILATestData()
        {
            var data = new List<object[]>();
            var regulatoryRequirements = RegulatoryRequirementTestData.getAll();
            var ilas = ILATestData.getAll();


            foreach (var rr in regulatoryRequirements)
            {
                foreach (var ila in ilas)
                {
                    data.Add(new object[] { rr.DeepCopy(), ila.DeepCopy() });
                }
            }

            return data;
        }

        public static IEnumerable<object[]> GetRegulatoryRequirement_EOTestdata()
        {
            var data = new List<object[]>();
            var rrs = RegulatoryRequirementTestData.getAll();
            var eos = EnablingObjectiveTestData.GetAll();

            foreach (var rr in rrs)
            {
                foreach (var eo in eos)
                {
                    data.Add(new object[] {rr.DeepCopy(), eo.DeepCopy() });
                }
            }

            return data;
        }
    }
}
