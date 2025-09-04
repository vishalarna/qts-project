using QTD2.Infrastructure.ExtensionMethods;
using QTD2.Test.Data.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class ProcedureTests
    {
        public static IEnumerable<object[]> GetProcedure_EnablingObjectiveTestData()
        {
            var data = new List<object[]>();
            var enablingObjectives = EnablingObjectiveTestData.GetAll();
            var procedures = ProcedureTestData.GetAll();


            foreach (var proc in procedures)
            {
                foreach (var eo in enablingObjectives)
                {
                    data.Add(new object[] { proc.DeepCopy(), eo.DeepCopy() });
                }
            }

            return data;
        }

        public static IEnumerable<object[]> GetProcedure_SafetyHazardTestData()
        {
            var data = new List<object[]>();
            var procedures = ProcedureTestData.GetAll();
            var saftyHazards = SafetyHazardTestData.GetAll();


            foreach (var proc in procedures)
            {
                foreach (var sh in saftyHazards)
                {
                    data.Add(new object[] { proc.DeepCopy(), sh.DeepCopy() });
                }
            }

            return data;
        }

        public static IEnumerable<object[]> GetProcedure_IlaTestData()
        {
            var data = new List<object[]>();
            var procedures = ProcedureTestData.GetAll();
            var Ilas = ILATestData.getAll();
            foreach(var proc in procedures)
            {
                foreach(var ila in Ilas)
                {
                    data.Add(new object[] { proc.DeepCopy(), ila.DeepCopy() });
                }
            }
            return data;
        }
    }
}
