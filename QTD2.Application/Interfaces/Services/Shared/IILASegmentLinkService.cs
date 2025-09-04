using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IILASegmentLinkService
    {
        public Task<ILA_Segment_Link> GetBySegmentId(int segmentId);
    }
}
