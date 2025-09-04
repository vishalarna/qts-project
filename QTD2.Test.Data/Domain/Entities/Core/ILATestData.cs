using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class ILATestData
    {
        public static List<ILA> getAll()
        {
            return new List<ILA> {
                ila01()
                //,ila02()
            };
        }

        static ILA ila01()
        {
            var ila = new ILA("test name", "test NickName", "123", "test Description", "", "", 1, false, true, false, null, 1, false, false, null, null, null, DateTime.Now,false,string.Empty,false,true);
            ila.Set_Id(1);
            return ila;
        }

        /*static ILA ila02()
        {
            var ila = new ILA("test name 2", "test NickName 2", 123, "test Description 2", "", "", 1, 1, false, true, false, null, 1, false, false, null, null, null);
            ila.Set_Id(2);
            return ila;
        }*/
    }
}
