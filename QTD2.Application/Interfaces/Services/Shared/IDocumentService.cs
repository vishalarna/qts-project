using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Document;

namespace QTD2.Application.Interfaces.Services.Shared
{
	public interface IDocumentService
	{
		Task<List<DocumentViewModel>> GetAllActiveAsync();
		Task<DocumentViewModel> CreateAsync(DocumentCreationOptions options);
		Task<DocumentViewModel> GetActiveAsync(int id);
		Task<DocumentViewModel> UpdateActiveAsync(int id, DocumentUpdateOptions options);
		Task DeleteActiveAsync(int id);
		Task<string> GetActiveFileAsync(int id);
		Task<List<DocumentViewModel>> GetActiveByDocumentTypeAsync(int documentTypeId);
		Task<List<DocumentViewModel>> GetActiveByLinkedDataAsync(string linkedDataType, int linkedDataId);
		Task<List<DocumentViewModel>> GetActiveByLinkedDataAndDocumentTypeAsync(string linkedDataType, int linkedDataId, int documentTypeId);
		Task<string> GetLinkedDataNameAsync(string linkedDataType, int linkedDataId);
	}
}
