using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ILACollaboratorRepository : Common.Repository<ILACollaborator>, IILACollaboratorRepository
    {
        public ILACollaboratorRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
