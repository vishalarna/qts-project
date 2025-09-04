using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class TestTests
    {
        [Theory, MemberData(nameof(TestTests.GetTest_TestItem))]
        public void Test_AddTestItem(TestItem testItem, QTD2.Domain.Entities.Core.Test test)
        {
            var testCount = test.Test_Item_Links.Count();
            test.LinkTestItem(testItem);
            Assert.Equal(testCount + 1, test.Test_Item_Links.Count());
        }
        [Theory, MemberData(nameof(TestTests.GetTest_TestItem))]
        public void Test_RemoveTestItem(TestItem testItem, QTD2.Domain.Entities.Core.Test test)
        {
            var testCount = test.Test_Item_Links.Count();
            test.LinkTestItem(testItem);
            test.UnLinkTestItem(testItem);
            Assert.Equal(testCount, test.Test_Item_Links.Count());
        }
    }
}
