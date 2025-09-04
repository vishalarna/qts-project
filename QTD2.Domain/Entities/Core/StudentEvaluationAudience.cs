using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class StudentEvaluationAudience : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<ILA_StudentEvaluation_Link> ILA_StudentEvaluation_Links { get; set; } = new List<ILA_StudentEvaluation_Link>();

        public StudentEvaluationAudience(string name)
        {
            Name = name;
        }

        public StudentEvaluationAudience()
        {
        }
    }
}
