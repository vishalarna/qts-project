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
    public class OnDutyAreaDeletedHandler : INotificationHandler<OnDutyAreaDeleted>
    {
        private readonly ISubdutyAreaService _subdutyAreaService;
        public OnDutyAreaDeletedHandler(ISubdutyAreaService subdutyAreaService) 
        {
            _subdutyAreaService = subdutyAreaService;
        }

        public async System.Threading.Tasks.Task Handle(OnDutyAreaDeleted dutyAreaDeleted, CancellationToken cancellationToken)
        {
            var subdutyAreas = await _subdutyAreaService.GetSubDutyAreasByDutyAreaIdAsync(dutyAreaDeleted.DutyAreaDeleted.Id);
            foreach(var sda in subdutyAreas)
            {
                sda.Delete();
            }
            await _subdutyAreaService.BulkUpdateAsync(subdutyAreas);
        }
    }
}
