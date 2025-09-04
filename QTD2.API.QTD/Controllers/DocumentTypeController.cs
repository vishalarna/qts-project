using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Application.Interfaces.Services.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public class DocumentTypeController : Controller
    {
        private readonly IDocumentTypeService _documentTypeService;
        public DocumentTypeController(IDocumentTypeService documentTypeService)
        {
            _documentTypeService = documentTypeService;
        }

        /// <summary>
        /// Get all active DocumentTypes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/documentTypes/active")]
        public async Task<IActionResult> GetAllActiveAsync()
        {
            var result = await _documentTypeService.GetAllActiveAsync();
            return Ok(new { result });
        }

        /// <summary>
        /// Get a DocumentType's active LinkToDataOptions
        /// </summary>
        /// <param name="documentTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/documentTypes/{documentTypeId}/linkToDataOptions/active")]
        public async Task<IActionResult> GetActiveLinkToDataOptionsAsync(int documentTypeId)
        {
            var result = await _documentTypeService.GetActiveLinkToDataOptionsAsync(documentTypeId);
            return Ok(new { result });
        }
    }
}
