using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class SegmentObjective_LinkService : Common.Service<SegmentObjective_Link>, ISegmentObjective_LinkService
    {
        public SegmentObjective_LinkService(ISegmentObjective_LinkRepository repository, ISegmentObjective_LinkValidation validation)
            : base(repository, validation)
        {
        }
    }
}
