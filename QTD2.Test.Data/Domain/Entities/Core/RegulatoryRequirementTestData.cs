using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class RegulatoryRequirementTestData
    {
        public static List<RegulatoryRequirement> getAll()
        {
            return new List<RegulatoryRequirement> {
                rr01(),rr02()
            };
        }

        static RegulatoryRequirement rr01()
        {
            var rr = new RegulatoryRequirement(1, "12", "first requirement", "first Description", "1", null, new byte[] { }, String.Empty, "Name");
            rr.Set_Id(1);
            return rr;
        }

        static RegulatoryRequirement rr02()
        {
            var rr = new RegulatoryRequirement(1, "12", "second requirement", "second Description", "1", null, new byte[] { },String.Empty, "Name");
            rr.Set_Id(2);
            return rr;
        }
    }
}
