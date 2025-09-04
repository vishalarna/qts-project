using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ClassSchedule_TQEMPSettings;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IClassSchedule_Evaluator_LinksService
    {
        public System.Threading.Tasks.Task LinkEvaluators(ClassScheduleEvaluatorLinksVM options);
        public System.Threading.Tasks.Task UnlinkEvaluator(ClassScheduleEvaluatorLinksVM options);
        public System.Threading.Tasks.Task<List<Employee>> LinkEvaluatorsFromILA(int classScheduleId);
        public System.Threading.Tasks.Task<List<Employee>> GetClassScheduleTQEvaluators(int classScheduleId);
    }
}
