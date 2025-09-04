using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.DIFSurvey
{
    public class DIFResult_UpdateOptions
    {
        public int? TrainingStatus_OverrideId { get; set; }
        public int? DIFSurvey_Task_TrainingFrequencyId { get; set; }
        public string? Comments { get; set; }
    }
}
