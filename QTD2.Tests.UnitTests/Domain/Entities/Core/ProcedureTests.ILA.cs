using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class ProcedureTests
    {
        [Theory, MemberData(nameof(ProcedureTests.GetProcedure_IlaTestData))]
        public void ProcedureTests_LinkIla(Procedure procedure, ILA ila)
        {
            var procCount = procedure.Procedure_ILA_Links.Count();
            procedure.LinkIla(ila);
            Assert.Equal(procCount + 1, procedure.Procedure_ILA_Links.Count());
        }

        [Theory, MemberData(nameof(ProcedureTests.GetProcedure_IlaTestData))]
        public void ProcedureTests_UnlinkIla(Procedure procedure, ILA ila)
        {
            var procCount = procedure.Procedure_ILA_Links.Count();
            procedure.LinkIla(ila);
            procedure.UnlinkIla(ila);
            Assert.Equal(procCount, procedure.Procedure_ILA_Links.Count());
        }
    }
}
