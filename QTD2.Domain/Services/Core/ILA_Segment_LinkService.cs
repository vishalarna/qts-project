using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ILA_Segment_LinkService : Common.Service<ILA_Segment_Link>, IILA_Segment_LinkService
    {
        public ILA_Segment_LinkService(IILA_Segment_LinkRepository repository, IILA_Segment_LinkValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<ILA_Segment_Link>> GetILASegmentLinksWithSegments(int id)
        {
            return (await FindWithIncludeAsync(x => x.ILAId == id, new[] { "Segment.SegmentObjective_Links" })).ToList();
        }
    }
}
