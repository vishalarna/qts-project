using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class NercStandardMemberTestData
    {
        public static List<NercStandardMember> GetAll()
        {
            return new List<NercStandardMember>
            {
                nsm01(), nsm02()
            };
        }

        public static NercStandardMember nsm01()
        {
            var nps = new NercStandardMember(1, "Test 1", "type");
            nps.Set_Id(1);
            return nps;
        }

        public static NercStandardMember nsm02()
        {
            var nps = new NercStandardMember(2, "Test 1", "type");
            nps.Set_Id(2);
            return nps;
        }
    }
}
