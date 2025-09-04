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
    public class OnILA_DeactivatedHandler : INotificationHandler<OnILA_Deactivated>
    {
        private readonly IInstructorWorkbook_ProspectiveILAService _instructorWorkbook_ProspectiveILAService;
        public OnILA_DeactivatedHandler(IInstructorWorkbook_ProspectiveILAService instructorWorkbook_ProspectiveILAService) 
        {
            _instructorWorkbook_ProspectiveILAService = instructorWorkbook_ProspectiveILAService;
        }
        public async System.Threading.Tasks.Task Handle(OnILA_Deactivated ila, CancellationToken cancellationToken)
        {
            var iwbProspectiveILA = await _instructorWorkbook_ProspectiveILAService.GetIWBProspectiveILAByILAId(ila.DeactivatedILA.Id);
            if (iwbProspectiveILA != null)
            {
                iwbProspectiveILA.InActive = true;
                await _instructorWorkbook_ProspectiveILAService.UpdateAsync(iwbProspectiveILA);
            }
        }
    }
}
