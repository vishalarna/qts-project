using QTD2.Infrastructure.ExtensionMethods;
using QTD2.Test.Data.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class Procedure_IssuingAuthorityTests
    {
        public static IEnumerable<object[]> GetProcedure_IssuingAuthority_TestData()
        {
            var data = new List<object[]>();
            var issuingAuthorities = IssuingAuthorityTestData.GetAll();
            var procedures = ProcedureTestData.GetAll();

            foreach (var ia in issuingAuthorities)
            {
                foreach (var proc in procedures)
                {
                    data.Add(new object[] { ia.DeepCopy(), proc.DeepCopy() });
                }
            }

            return data;
        }
    }
}
