using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class NERCAudienceTestData
    {
        public static List<NERCTargetAudience> GetAll()
        {
            return new List<NERCTargetAudience>
            {
                nta01(), nta02()
            };
        }

        public static NERCTargetAudience nta01()
        {
            var nta = new NERCTargetAudience("First NERCAudience", false, "First Other NERCAudience");
            nta.Set_Id(1);
            return nta;
        }

        public static NERCTargetAudience nta02()
        {
            var nta = new NERCTargetAudience("Second NERCAudience", false, "Second Other NERCAudience");
            nta.Set_Id(2);
            return nta;
        }
    }
}
