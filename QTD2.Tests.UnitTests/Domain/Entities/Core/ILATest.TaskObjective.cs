using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class ILATest
    {
        [Theory, MemberData(nameof(ILATest.GetILA_TaskObjectiveTestData))]
        public void ILATest_LinkTaskObjective(ILA ila, QTD2.Domain.Entities.Core.Task task)
        {
            var ilaCount = ila.ILA_TaskObjective_Links.Count();
            ila.LinkTaskObjective(task);
            Assert.Equal(ilaCount + 1, ila.ILA_TaskObjective_Links.Count());
        }

        [Theory, MemberData(nameof(ILATest.GetILA_TaskObjectiveTestData))]
        public void ILATest_UnlinkTaskObjective(ILA ila, QTD2.Domain.Entities.Core.Task task)
        {
            var ilaCount = ila.ILA_TaskObjective_Links.Count();
            ila.LinkTaskObjective(task);
            ila.UnlinkTaskObjective(task);
            Assert.Equal(ilaCount, ila.ILA_TaskObjective_Links.Count());
        }
    }
}
