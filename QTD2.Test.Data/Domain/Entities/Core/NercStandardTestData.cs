using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class NercStandardTestData
    {
        public static List<NercStandard> GetAll()
        {
            return new List<NercStandard>
            {
                nps01(), nps02()
            };
        }

        public static NercStandard nps01()
        {
            var nps = new NercStandard("First NercStandard", false, false);
            nps.Set_Id(1);
            return nps;
        }

        public static NercStandard nps02()
        {
            var nps = new NercStandard("second NercStandard", false, false);
            nps.Set_Id(2);
            return nps;
        }
    }
}
