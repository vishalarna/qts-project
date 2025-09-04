using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.DIFSurvey
{
    public class DIFTaskRating_VM
    {
        public string TaskNumber { get; set; }
        public bool Active { get; set; }
        public float? AverageDifficulty { get; set; }
        public float? AverageImportance { get; set; }
        public float? AverageFrequency { get; set; }
        public string DefaultTrainingStatus { get; set; }
    }
}
