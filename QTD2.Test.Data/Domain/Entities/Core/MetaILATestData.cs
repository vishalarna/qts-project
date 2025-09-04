using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public class MetaILATestData
    {
        public static List<MetaILA> getAll()
        {
            return new List<MetaILA> {
                ila01()
            };
        }

        static MetaILA ila01()
        {
            var ila = new MetaILA("Western Power Pool", "Western Power Pool", 1, DateTime.Now, 1,"reason");
            ila.Set_Id(1);
            return ila;
        }
    }
}
