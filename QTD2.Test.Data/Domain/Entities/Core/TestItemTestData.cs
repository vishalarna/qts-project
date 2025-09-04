using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class TestItemTestData
    {
        public static List<TestItem> GetAll()
        {
            return new List<TestItem>()
            {
                TestItem1(),
              //  TestItem2()
            };
        }

        static TestItem TestItem1()
        {
            return new TestItem(1, 1, true, "Supervise the machines and operators", "ITM-1.0", 1);
        }

       /* static TestItem TestItem2()
        {
            return new TestItem(2, 2, true, "Supervise the machines and operators", "image2", 2);
        }*/
    }
}
