using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class TestType : Entity
    {
        public string Description { get; set; }

        public virtual ICollection<ILATraineeEvaluation> ILATraineeEvaluations { get; set; } = new List<ILATraineeEvaluation>();

        public virtual ICollection<ClassSchedule_Roster> ClassSchedule_Rosters { get; set; } = new List<ClassSchedule_Roster>();

        //public virtual ICollection<EmpTest> EmpTests { get; set; } = new List<EmpTest>();

        public TestType(string testTypeDescription)
        {
            Description = testTypeDescription;
        }

        public TestType()
        {
        }
    }
}
