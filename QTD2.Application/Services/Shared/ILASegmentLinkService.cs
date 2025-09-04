using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IILASegmentLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_Segment_LinkService;

namespace QTD2.Application.Services.Shared
{
    public class ILASegmentLinkService : IILASegmentLinkService
    {
        private readonly IILASegmentLinkDomainService _iLASegmentLinkDomainService;
        public ILASegmentLinkService(IILASegmentLinkDomainService iLASegmentLinkDomainService)
        {
            _iLASegmentLinkDomainService = iLASegmentLinkDomainService;
        }
        public async Task<ILA_Segment_Link> GetBySegmentId(int segmentId)
        {
            var ilaSegment = (await _iLASegmentLinkDomainService.FindAsync(x=>x.SegmentId == segmentId)).FirstOrDefault();
            return ilaSegment;
        }
    }
}
