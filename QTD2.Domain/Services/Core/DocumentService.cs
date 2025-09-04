using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class DocumentService : Common.Service<Document>, IDocumentService
    {
        public DocumentService(IDocumentRepository repository, IDocumentValidation validation)
            : base(repository, validation)
        {

        }

        public async System.Threading.Tasks.Task<List<Document>> GetAllActiveAsync()
        {
            var queryable = await FindWithIncludeAsync(x => x.Active, new string[] { "DocumentType" });
            return queryable.ToList();
        }

        public async System.Threading.Tasks.Task<Document> GetActiveAsync(int id)
        {
            List<Expression<Func<Document, bool>>> predicates = new List<Expression<Func<Document, bool>>>();
            if (id > 0)
                predicates.Add(x => x.Active && x.Id == id);
            var queryable = await FindWithIncludeAsync(predicates, new string[] { "DocumentType" });
            return queryable.FirstOrDefault();
        }

        public async System.Threading.Tasks.Task<List<Document>> GetActiveByDocumentTypeAsync(int documentTypeId)
        {
            List<Expression<Func<Document, bool>>> predicates = new List<Expression<Func<Document, bool>>>();
            if (documentTypeId > 0)
                predicates.Add(x => x.Active && x.DocumentTypeId == documentTypeId);
            var queryable = await FindWithIncludeAsync(predicates, new string[] { "DocumentType" });
            return queryable.ToList();
        }

        public async System.Threading.Tasks.Task<List<Document>> GetActiveByLinkedDataAsync(string linkedDataType, int linkedDataId)
        {
            List<Expression<Func<Document, bool>>> predicates = new List<Expression<Func<Document, bool>>>();
            if ((!string.IsNullOrEmpty(linkedDataType)) && linkedDataId > 0)
                predicates.Add(x => x.Active && x.DocumentType.LinkedDataType == linkedDataType && x.LinkedDataId == linkedDataId);
            var queryable = await FindWithIncludeAsync(predicates, new string[] { "DocumentType" });
            return queryable.ToList();
        }

        
        public async System.Threading.Tasks.Task<List<Document>> GetActiveByLinkedDataAndDocumentTypeAsync(string linkedDataType, int linkedDataId, int documentTypeId)
        {
            List<Expression<Func<Document, bool>>> predicates = new List<Expression<Func<Document, bool>>>();
            if ((!string.IsNullOrEmpty(linkedDataType)) && linkedDataId > 0 && documentTypeId > 0)
                predicates.Add(x => x.Active && x.DocumentType.LinkedDataType == linkedDataType && x.LinkedDataId == linkedDataId && x.DocumentTypeId==documentTypeId);
            var queryable = await FindWithIncludeAsync(predicates, new string[] { "DocumentType" });
            return queryable.ToList();
        }
    }
}
