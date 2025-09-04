using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IDocumentService : Common.IService<Document>
    {
        public System.Threading.Tasks.Task<List<Document>> GetAllActiveAsync();
        public System.Threading.Tasks.Task<Document> GetActiveAsync(int id);
        public System.Threading.Tasks.Task<List<Document>> GetActiveByDocumentTypeAsync(int documentTypeId);
        public System.Threading.Tasks.Task<List<Document>> GetActiveByLinkedDataAsync(string linkedDataType, int linkedDataId);
        public System.Threading.Tasks.Task<List<Document>> GetActiveByLinkedDataAndDocumentTypeAsync(string linkedDataType, int linkedDataId, int documentTypeId);
    }
}
