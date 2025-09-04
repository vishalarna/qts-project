using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Infrastructure.ExtensionMethods;
using QTD2.Test.Data.Domain.Entities.Core;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class ILATest
    {
        public static IEnumerable<object[]> GetILA_RegulatoryRequirementTestData()
        {
            var data = new List<object[]>();
            var regulatoryRequirements = RegulatoryRequirementTestData.getAll();
            var ilas = ILATestData.getAll();


            foreach (var ila in ilas)
            {
                foreach (var rr in regulatoryRequirements)
                {
                    data.Add(new object[] { ila.DeepCopy(), rr.DeepCopy() });
                }
            }

            return data;
        }

        public static IEnumerable<object[]> GetILA_SegmentTestData()
        {
            var data = new List<object[]>();
            var ilas = ILATestData.getAll();
            var segments = SegmentTestData.getAll();


            foreach (var ila in ilas)
            {
                foreach (var sg in segments)
                {
                    data.Add(new object[] { ila.DeepCopy(), sg.DeepCopy() });
                }
            }

            return data;
        }

        public static IEnumerable<object[]> GetILA_TrainingTopicTestData()
        {
            var data = new List<object[]>();
            var ilas = ILATestData.getAll();
            var trainingTopics = TrainingTopicTestData.getAll();
            
            foreach(var ila in ilas) 
            {
                foreach(var tt in trainingTopics) 
                {
                    data.Add(new object[] { ila.DeepCopy(), tt.DeepCopy() });
                }
            }

            return data;
        }

        public static IEnumerable<object[]> GetILA_ProcedureTestData()
        {
            var data = new List<object[]>();
            var ilas = ILATestData.getAll();
            var procedures = ProcedureTestData.GetAll();

            foreach (var ila in ilas)
            {
                foreach (var proc in procedures)
                {
                    data.Add(new object[] { ila.DeepCopy(), proc.DeepCopy() });
                }
            }

            return data;
        }

        public static IEnumerable<object[]> GetILA_AssessmentToolTestData()
        {
            var data = new List<object[]>();
            var ilas = ILATestData.getAll();
            var assessmentTools = AssessmentToolTestData.Getall();

            foreach(var ila in ilas)
            {
                foreach( var at in assessmentTools)
                {
                    data.Add(new object[] { ila.DeepCopy(), at.DeepCopy() });
                }
            }

            return data;
        }

        public static IEnumerable<object[]> GetILA_NercStandardTestData()
        {
            var data = new List<object[]>();
            var ilas = ILATestData.getAll();
            var nercs = NercStandardTestData.GetAll();
            var nercStandardMembers = NercStandardMemberTestData.GetAll();

            foreach (var ila in ilas)
            {
                foreach (var nerc in nercs)
                {
                    foreach (var std in nercStandardMembers)
                    {
                        data.Add(new object[] { ila.DeepCopy(), nerc.DeepCopy(), std.DeepCopy(),3});
                    }
                }
            }

            return data;
        }

        public static IEnumerable<object[]> GetILA_TaskObjectiveTestData()
        {
            var data = new List<object[]>();
            var ilas = ILATestData.getAll();
            var tasks = TaskTestData.GetAll();

            foreach (var ila in ilas)
            {
                foreach (var task in tasks)
                {
                    data.Add(new object[] { ila.DeepCopy(), task.DeepCopy() });
                }
            }

            return data;
        }

        public static IEnumerable<object[]> GetILA_EnablingObjectiveTestData()
        {
            var data = new List<object[]>();
            var ilas = ILATestData.getAll();
            var eos = EnablingObjectiveTestData.GetAll();

            foreach(var ila in ilas)
            {
                foreach(var eo in eos)
                {
                    data.Add(new object[] { ila.DeepCopy(), eo.DeepCopy() });
                }
            }

            return data;
        }

        public static IEnumerable<object[]> GetILA_SafetyHazardTestData()
        {
            var data = new List<object[]>();
            var ilas = ILATestData.getAll();
            var shs = SafetyHazardTestData.GetAll();

            foreach(var ila in ilas)
            {
                foreach(var sh in shs)
                {
                    data.Add(new object[] { ila.DeepCopy(), sh.DeepCopy() });
                }
            }

            return data;
        }

        public static IEnumerable<object[]> GetILA_PositionTestData()
        {
            var data = new List<object[]>();
            var ilas = ILATestData.getAll();
            var positions = PositionTestData.GetAll();

            foreach(var ila in ilas)
            {
                foreach(var position in positions)
                {
                    data.Add(new object[] { ila.DeepCopy(), position.DeepCopy() });
                }
            }

            return data;
        }

        public static IEnumerable<object[]> GetILA_NERCAudienceTestData()
        {
            var data = new List<object[]>();
            var ilas = ILATestData.getAll();
            var nercs = NERCAudienceTestData.GetAll();

            foreach (var ila in ilas)
            {
                foreach (var nerc in nercs)
                {
                    data.Add(new object[] { ila, nerc});
                }
            }

            return data;
        }

        public static IEnumerable<object[]> GetILA_CustomEnablingObjectiveTestData()
        {
            var data = new List<object[]>();
            var ilas = ILATestData.getAll();
            var eos = CustomEnablingObjectiveTestData.GetAll();

            foreach (var ila in ilas)
            {
                foreach (var eo in eos)
                {
                    data.Add(new object[] { ila.DeepCopy(), eo.DeepCopy() });
                }
            }

            return data;
        }
    }
}
