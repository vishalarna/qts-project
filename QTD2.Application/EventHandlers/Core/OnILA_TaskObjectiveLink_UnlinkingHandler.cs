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
    public class OnILA_TaskObjectiveLink_UnlinkingHandler : INotificationHandler<OnILA_TaskObjectiveLink_Unlinking>
    {
        private readonly ISegmentObjective_LinkService _segmentObjective_LinkService;
        private readonly IILA_Segment_LinkService _iLA_Segment_LinkService;
        public OnILA_TaskObjectiveLink_UnlinkingHandler(ISegmentObjective_LinkService segmentObjective_LinkService, IILA_Segment_LinkService iLA_Segment_LinkService) 
        {
            _segmentObjective_LinkService = segmentObjective_LinkService;
            _iLA_Segment_LinkService = iLA_Segment_LinkService;
        }

        public async System.Threading.Tasks.Task Handle(OnILA_TaskObjectiveLink_Unlinking ila_TaskObjLink, CancellationToken cancellationToken)
        {
            var IlaSegments = (await _iLA_Segment_LinkService.FindWithIncludeAsync(x => x.ILAId == ila_TaskObjLink._iLA_TaskObjective_Link.ILAId, new string[] { "Segment.SegmentObjective_Links" })).ToList();
            foreach(var ilaSeg in IlaSegments)
            {
                var segObj = ilaSeg.Segment.SegmentObjective_Links.Where(x => x.TaskId == ila_TaskObjLink._iLA_TaskObjective_Link.TaskId).FirstOrDefault();
                if(segObj != null)
                {
                    segObj.Delete();
                    await _segmentObjective_LinkService.UpdateAsync(segObj);
                }
            }
        }
    }
}
