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
    public class OnILA_EnablingObjectiveLink_UnlinkingHandler : INotificationHandler<OnILA_EnablingObjectiveLink_Unlinking>
    {
        private readonly ISegmentObjective_LinkService _segmentObjective_LinkService;
        private readonly IILA_Segment_LinkService _iLA_Segment_LinkService;
        public OnILA_EnablingObjectiveLink_UnlinkingHandler(ISegmentObjective_LinkService segmentObjective_LinkService, IILA_Segment_LinkService iLA_Segment_LinkService) 
        {
            _segmentObjective_LinkService = segmentObjective_LinkService;
            _iLA_Segment_LinkService = iLA_Segment_LinkService;
        }

        public async System.Threading.Tasks.Task Handle(OnILA_EnablingObjectiveLink_Unlinking ila_EnablingObjLink, CancellationToken cancellationToken)
        {
            var IlaSegments = (await _iLA_Segment_LinkService.FindWithIncludeAsync(x => x.ILAId == ila_EnablingObjLink._iLA_EnablingObjective_Link.ILAId, new string[] { "Segment.SegmentObjective_Links" })).ToList();
            foreach (var ilaSeg in IlaSegments)
            {
                var segObj = ilaSeg.Segment.SegmentObjective_Links.Where(x => x.EnablingObjectiveId == ila_EnablingObjLink._iLA_EnablingObjective_Link.EnablingObjectiveId).FirstOrDefault();
                if (segObj != null)
                {
                    segObj.Delete();
                    await _segmentObjective_LinkService.UpdateAsync(segObj);
                }
            }
        }
    }
}
