using QTD2.Test.Data.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class SafetyHazardTests
    {
        public static IEnumerable<object[]> GetSafetyHazard_AbatementData()
        {
            var data = new List<object[]>();
            var safetyHazards = SafetyHazardTestData.GetAll();
            var sh_abatments = SafetyHazard_AbatementTestData.GetAll();


            foreach (var sh in safetyHazards)
            {
                foreach (var sh_a in sh_abatments)
                {
                    data.Add(new object[] { sh, sh_a });
                }
            }

            return data;
        }


        public static IEnumerable<object[]> GetSafetyHazard_ControlData()
        {
            var data = new List<object[]>();
            var safetyHazards = SafetyHazardTestData.GetAll();
            var sh_controls = SafetyHazard_ControlTestData.GetAll();


            foreach (var sh in safetyHazards)
            {
                foreach (var sh_c in sh_controls)
                {
                    data.Add(new object[] { sh, sh_c });
                }
            }

            return data;
        }

        /* safty hazrad reg requirement link starts*/
        public static IEnumerable<object[]> GetSafetyHazard_RegulatoryRequirement()
        {
            var data = new List<object[]>();
            var safteyHazards = SafetyHazardTestData.GetAll();
            var regRequirements = RegulatoryRequirementTestData.getAll();

            foreach (var sh in safteyHazards)
            {
                foreach (var rr in regRequirements)
                {
                    data.Add(new object[] { rr, sh});
                }
            }

            return data;

        }

        /* safty hazrad reg requirement link ends*/

        public static IEnumerable<object[]> GetSafetyHazard_Procedure()
        {
            var data = new List<object[]>();
            var safteyHazards = SafetyHazardTestData.GetAll();
            var procedures = ProcedureTestData.GetAll();

            foreach (var sh in safteyHazards)
            {
                foreach (var pr in procedures)
                {
                    data.Add(new object[] { pr, sh });
                }
            }

            return data;

        }

        public static IEnumerable<object[]> GetSafetyHazard_ILA()
        {
            var data = new List<object[]>();
            var safteyHazards = SafetyHazardTestData.GetAll();
            var ilas = ILATestData.getAll();

            foreach (var sh in safteyHazards)
            {
                foreach (var i in ilas)
                {
                    data.Add(new object[] { i, sh });
                }
            }

            return data;

        }
    }
}
