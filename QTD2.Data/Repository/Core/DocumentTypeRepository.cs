using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class DocumentTypeRepository : Common.Repository<DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
