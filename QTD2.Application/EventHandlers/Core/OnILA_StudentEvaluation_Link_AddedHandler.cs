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
    public class OnILA_StudentEvaluation_Link_AddedHandler : INotificationHandler<OnILA_StudentEvaluation_Link_Added>
    {
        private readonly IClassScheduleService _classScheduleService;
        private readonly IClassSchedule_StudentEvaluations_LinkService _classSchedule_StudentEvaluations_LinkService;

        public OnILA_StudentEvaluation_Link_AddedHandler(IClassScheduleService classScheduleService, IClassSchedule_StudentEvaluations_LinkService classSchedule_StudentEvaluations_LinkService)
        {
            _classScheduleService = classScheduleService;
            _classSchedule_StudentEvaluations_LinkService = classSchedule_StudentEvaluations_LinkService;
        }

        public async System.Threading.Tasks.Task Handle(OnILA_StudentEvaluation_Link_Added iLA_StudentEvaluation_Link, CancellationToken cancellationToken)
        {
            var classSchedules = await _classScheduleService.GetClassSchedulesByIdAsync(iLA_StudentEvaluation_Link.ILA_StudentEvaluation_Link.ILAId);
            var utcNow = DateTime.UtcNow;

            if (classSchedules.Count() != 0 && classSchedules != null)
            {
                foreach (var classSchedule in classSchedules)
                {
                    if (classSchedule.EndDateTime > utcNow)
                    {
                        var classEvalLink = (await _classSchedule_StudentEvaluations_LinkService.FindAsync(x => x.ClassScheduleId == classSchedule.Id)).FirstOrDefault();
                        if(classEvalLink != null)
                        {
                            classEvalLink.UpdateClassEval(iLA_StudentEvaluation_Link.ILA_StudentEvaluation_Link.studentEvalFormID);
                            await _classSchedule_StudentEvaluations_LinkService.UpdateAsync(classEvalLink);
                        }
                        else
                        {
                            var link = new ClassSchedule_StudentEvaluations_Link
                            {
                                ClassScheduleId = classSchedule.Id,
                                StudentEvaluationId = iLA_StudentEvaluation_Link.ILA_StudentEvaluation_Link.studentEvalFormID
                            };
                            await _classSchedule_StudentEvaluations_LinkService.AddAsync(link);
                        }
                    }
                }
            }

        }
    }
}
