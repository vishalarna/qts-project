using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class TraineeEvaluationType : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<ILATraineeEvaluation> ILATraineeEvaluations { get; set; } = new List<ILATraineeEvaluation>();

        public TraineeEvaluationType(string name)
        {
            Name = name;
        }

        public TraineeEvaluationType()
        {
        }
    }
}
