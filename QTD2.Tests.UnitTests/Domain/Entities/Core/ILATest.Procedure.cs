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
        [Theory, MemberData(nameof(ILATest.GetILA_ProcedureTestData))]
        public void ILATest_LinkProcedure(ILA ila, Procedure procedure)
        {
            var pCount = ila.ILA_Procedure_Links.Count();
            ila.LinkProcedure(procedure);
            Assert.Equal(pCount + 1, ila.ILA_Procedure_Links.Count());
        }

        [Theory, MemberData(nameof(ILATest.GetILA_ProcedureTestData))]
        public void ILATest_UninkProcedure(ILA ila, Procedure procedure)
        {
            var pCount = ila.ILA_Procedure_Links.Count();
            ila.LinkProcedure(procedure);
            ila.UnlinkProcedure(procedure);
            Assert.Equal(pCount, ila.ILA_Procedure_Links.Count());
        }
    }
}
