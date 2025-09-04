using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.Document;

namespace QTD2.API.QTD.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DocumentController : ControllerBase
	{
		private readonly IDocumentService _documentService;

		public DocumentController(IDocumentService documentService)
		{
			_documentService = documentService;
		}

		/// <summary>
		/// Get all active Documents
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("/documents/active")]
		public async Task<IActionResult> GetAllActiveAsync()
		{
			var result = await _documentService.GetAllActiveAsync();
			return Ok(new { result });
		}

		/// <summary>
		/// Upload a file to the filesystem, create a Document, and return the Document
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("/documents")]
		public async Task<IActionResult> CreateAsync(DocumentCreationOptions options)
		{
			var result = await _documentService.CreateAsync(options);
			return Ok(new { result });
		}

		/// <summary>
		/// Get an active Document by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("/documents/active/{id}")]
		public async Task<IActionResult> GetActiveAsync(int id)
		{
			var result = await _documentService.GetActiveAsync(id);
			return Ok(new { result });
		}

		/// <summary>
		/// Update an active Document by id, and return the Document
		/// </summary>
		/// <param name="id"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		[HttpPut]
		[Route("/documents/active/{id}")]
		public async Task<IActionResult> UpdateActiveAsync(int id, DocumentUpdateOptions options)
		{
			var result = await _documentService.UpdateActiveAsync(id, options);
			return Ok(new { result });
		}

		/// <summary>
		/// Delete an active Document by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete]
		[Route("/documents/active/{id}")]
		public async Task<IActionResult> DeleteActiveAsync(int id)
		{
			await _documentService.DeleteActiveAsync(id);
			return Ok();
		}

		/// <summary>
		/// Get a Document's file by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("/documents/active/{id}/file")]
		public async Task<IActionResult> GetActiveFileAsync(int id)
		{
			var document = await _documentService.GetActiveFileAsync(id);
			return Ok( new { document });
		}

		/// <summary>
		/// Get active Documents of a DocumentType
		/// </summary>
		/// <param name="documentTypeId"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("/documents/active/documentType/{documentTypeId}")]
		public async Task<IActionResult> GetActiveByDocumentTypeAsync(int documentTypeId)
		{
			var result = await _documentService.GetActiveByDocumentTypeAsync(documentTypeId);
			return Ok( new { result });
		}

		/// <summary>
		/// Get active Documents for a LinkedData
		/// </summary>
		/// <param name="linkedDataType"></param>
		/// <param name="linkedDataId"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("/documents/active/{linkedDataType}/{linkedDataId}")]
		public async Task<IActionResult> GetActiveByLinkedDataAsync(string linkedDataType, int linkedDataId)
		{
			var result = await _documentService.GetActiveByLinkedDataAsync(linkedDataType, linkedDataId);
			return Ok( new { result });
		}

		/// <summary>
		/// Get active Documents for a LinkedData and of a DocumentType
		/// </summary>
		/// <param name="linkedDataType"></param>
		/// <param name="linkedDataId"></param>
		/// <param name="documentTypeId"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("/documents/active/{linkedDataType}/{linkedDataId}/documentType/{documentTypeId}")]
		public async Task<IActionResult> GetActiveByLinkedDataAndDocumentTypeAsync(string linkedDataType, int linkedDataId, int documentTypeId)
		{
			var result = await _documentService.GetActiveByLinkedDataAndDocumentTypeAsync(linkedDataType, linkedDataId, documentTypeId);
			return Ok( new { result });
		}

		/// <summary>
		/// Get LinkedData "Name"
		/// </summary>
		/// <param name="linkedDataType"></param>
		/// <param name="linkedDataId"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("/documents/linkedDataName/{linkedDataType}/{linkedDataId}")]
		public async Task<IActionResult> GetLinkedDataNameAsync(string linkedDataType, int linkedDataId)
		{
			var result = await _documentService.GetLinkedDataNameAsync(linkedDataType, linkedDataId);
			return Ok( new { result });
		}
	}
}
