using MediatR;
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
    public class OnILA_ActivatedHandler : INotificationHandler<OnILA_Activated>
    {
        private readonly IInstructorWorkbook_ProspectiveILAService _instructorWorkbook_ProspectiveILAService;
        public OnILA_ActivatedHandler(IInstructorWorkbook_ProspectiveILAService instructorWorkbook_ProspectiveILAService)
        {
            _instructorWorkbook_ProspectiveILAService = instructorWorkbook_ProspectiveILAService;
        }
        public async System.Threading.Tasks.Task Handle(OnILA_Activated ila, CancellationToken cancellationToken)
        {
            var iwbProspectiveILA = await _instructorWorkbook_ProspectiveILAService.GetIWBProspectiveILAByILAId(ila.ActivatedILA.Id);
            if (iwbProspectiveILA != null)
            {
                iwbProspectiveILA.InActive = false;
                await _instructorWorkbook_ProspectiveILAService.UpdateAsync(iwbProspectiveILA);
            }
        }
    }
}
