using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class DocumentTypeService : Common.Service<DocumentType>, IDocumentTypeService
    {
        public DocumentTypeService(IDocumentTypeRepository repository, IDocumentTypeValidation validation)
            : base(repository, validation)
        {

        }
    		
		public async System.Threading.Tasks.Task<List<DocumentType>> GetAllActiveAsync()
        {
            var queryable = await FindAsync(x => x.Active);
            return queryable.ToList();
        }

        public async System.Threading.Tasks.Task<DocumentType> GetActiveAsync(int id)
        {
            var queryable = await FindAsync(x => x.Id == id && x.Active);
            return queryable.First();
        }
    }
}
