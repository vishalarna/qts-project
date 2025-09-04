using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILATraineeEvaluation
{
    public class ILATraineeEvaluationUpdateOptions
    {
        public int TestId { get; set; }

        public int ILAId { get; set; }

        public int TestTypeId { get; set; }

        public int EvaluationTypeId { get; set; }

        public string TestTitle { get; set; }

        public string TestInstruction { get; set; }

        public int TestTimeLimitHours { get; set; }

        public int TestTimeLimitMinutes { get; set; }

        public string TrainingEvaluationMethod { get; set; }
    }
}
