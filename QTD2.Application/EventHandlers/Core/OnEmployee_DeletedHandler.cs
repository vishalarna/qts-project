using MediatR;
using QTD2.Domain.Events.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnEmployee_DeletedHandler : INotificationHandler<OnEmployee_Deleted>
    {
        private readonly IDocumentService _documentService;
        private readonly ITaskQualification_Evaluator_LinkService _taskQualification_Evaluator_LinkService;
        public OnEmployee_DeletedHandler(IDocumentService documentService, ITaskQualification_Evaluator_LinkService taskQualification_Evaluator_LinkService)
        {
            _documentService = documentService;
            _taskQualification_Evaluator_LinkService = taskQualification_Evaluator_LinkService;
        }

        public async System.Threading.Tasks.Task Handle(OnEmployee_Deleted deletedEmployee, CancellationToken cancellationToken)
        {
            var documents = await _documentService.GetActiveByLinkedDataAsync("Employees", deletedEmployee.Employee.Id);
            var taskQualifciationEvaluatorLinks = await _taskQualification_Evaluator_LinkService.GetTaskQualificationsByEmployeeId(deletedEmployee.Employee.Id);

            foreach(var taskQualificationEvaluatoLink in taskQualifciationEvaluatorLinks)
            {
                taskQualificationEvaluatoLink.Delete();
            }

            foreach (var doc in documents)
            {
                doc.Delete();
            }

            await _documentService.BulkUpdateAsync(documents);
            await _taskQualification_Evaluator_LinkService.BulkUpdateAsync(taskQualifciationEvaluatorLinks);
        }
    }
}
