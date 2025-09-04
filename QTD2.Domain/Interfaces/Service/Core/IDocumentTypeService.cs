using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IDocumentTypeService : Common.IService<DocumentType>
    {
        public System.Threading.Tasks.Task<List<DocumentType>> GetAllActiveAsync();
        public System.Threading.Tasks.Task<DocumentType> GetActiveAsync(int id);
    }
}
