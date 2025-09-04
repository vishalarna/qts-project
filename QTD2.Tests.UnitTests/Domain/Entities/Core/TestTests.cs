using QTD2.Test.Data.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class TestTests
    {
        public static IEnumerable<object[]> GetTest_TestItem()
        {
            var data = new List<object[]>();
            var tests = TestTestData.GetAll();
            var testItems = TestItemTestData.GetAll();

            foreach (var t in tests)
            {
                foreach (var item in testItems)
                {
                    data.Add(new object[] { item, t });
                }
            }

            return data;

        }
    }
}
