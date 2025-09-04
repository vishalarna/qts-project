using QTD2.Infrastructure.ExtensionMethods;
using QTD2.Test.Data.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class EnablingObjectiveTests
    {
        public static IEnumerable<object[]> GetEnablingObjective_ProcedureTestData()
        {
            var data = new List<object[]>();
            var enablingObjectives = EnablingObjectiveTestData.GetAll();
            var procedures = ProcedureTestData.GetAll();


            foreach (var eo in enablingObjectives)
            {
                foreach (var proc in procedures)
                {
                    data.Add(new object[] { eo.DeepCopy(), proc.DeepCopy() });
                }
            }

            return data;
        }


        public static IEnumerable<object[]> GetEnablingObjective_SafetyHazardTestData()
        {
            var data = new List<object[]>();
            var enablingObjectives = EnablingObjectiveTestData.GetAll();
            var saftyHazards = SafetyHazardTestData.GetAll();


            foreach (var eo in enablingObjectives)
            {
                foreach (var sh in saftyHazards)
                {
                    data.Add(new object[] { eo.DeepCopy(), sh.DeepCopy() });
                }
            }

            return data;
        }
    }
}
