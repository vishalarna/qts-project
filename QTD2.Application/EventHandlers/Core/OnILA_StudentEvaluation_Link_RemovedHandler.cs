using MediatR;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Events.Core;
using QTD2.Domain.Interfaces.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnILA_StudentEvaluation_Link_RemovedHandler : INotificationHandler<OnILA_StudentEvaluation_Link_Removed>
    {
        private readonly IClassScheduleService _classScheduleService;
        private readonly IClassSchedule_StudentEvaluations_LinkService _classSchedule_StudentEvaluations_LinkService;

        public OnILA_StudentEvaluation_Link_RemovedHandler(IClassScheduleService classScheduleService, IClassSchedule_StudentEvaluations_LinkService classSchedule_StudentEvaluations_LinkService)
        {
            _classScheduleService = classScheduleService;
            _classSchedule_StudentEvaluations_LinkService = classSchedule_StudentEvaluations_LinkService;
        }

        public async System.Threading.Tasks.Task Handle(OnILA_StudentEvaluation_Link_Removed iLA_StudentEvaluation_Link, CancellationToken cancellationToken)
        {
            var classSchedules = await _classScheduleService.GetClassSchedulesByIdAsync(iLA_StudentEvaluation_Link.ILA_StudentEvaluation_Link.ILAId);
            var utcNow = DateTime.UtcNow;

            if (classSchedules.Count() != 0 && classSchedules != null)
            {

                foreach (var classSchedule in classSchedules)
                {
                    if (classSchedule.EndDateTime > utcNow)
                    {
                        var classSchedule_StudentEvaluationLinks = (await _classSchedule_StudentEvaluations_LinkService.FindAsync(x => x.ClassScheduleId == classSchedule.Id)).ToList();
                        foreach (var studentEvaluation in classSchedule_StudentEvaluationLinks)
                        {
                            studentEvaluation.Delete();
                        }
                        await _classSchedule_StudentEvaluations_LinkService.BulkUpdateAsync(classSchedule_StudentEvaluationLinks);
                    }
                }
            }
        }
    }
}
