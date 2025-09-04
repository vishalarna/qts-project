using QTD2.Domain.Entities.Core;
using System.Linq;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class Procedure_IssuingAuthorityTests
    {
        [Theory, MemberData(nameof(Procedure_IssuingAuthorityTests.GetProcedure_IssuingAuthority_TestData))]
        public void Procedure_IssuingAuthority_AddProcedure(Procedure_IssuingAuthority proc_ia, Procedure procedure)
        {
            var procCount = proc_ia.Procedures.Count();

            proc_ia.AddProcedure(procedure);

            Assert.Equal(procCount + 1, proc_ia.Procedures.Count());
        }

        [Theory, MemberData(nameof(Procedure_IssuingAuthorityTests.GetProcedure_IssuingAuthority_TestData))]
        public void Procedure_IssuingAuthority_RemoveProcedure(Procedure_IssuingAuthority proc_ia, Procedure procedure)
        {
            var procCount = proc_ia.Procedures.Count();

            proc_ia.AddProcedure(procedure);
            proc_ia.RemoveProcedure(procedure);

            Assert.Equal(procCount, proc_ia.Procedures.Count());
        }
    }
}
