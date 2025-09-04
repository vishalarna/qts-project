using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Infrastructure.Model.Task_Requalification
{
    public class TaskQualificationWithEvalsVM
    {
        public TaskQualification TaskQualification { get; set; }

        public List<Domain.Entities.Core.Employee> Evaluators { get; set; } = new List<Domain.Entities.Core.Employee>();
    }
}
