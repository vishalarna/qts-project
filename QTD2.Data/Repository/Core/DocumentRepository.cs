using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class DocumentRepository : Common.Repository<Document>, IDocumentRepository
    {
        public DocumentRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
