using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IILA_Segment_LinkService : Common.IService<ILA_Segment_Link>
    {
        public Task<List<ILA_Segment_Link>> GetILASegmentLinksWithSegments(int id);
    }
}
