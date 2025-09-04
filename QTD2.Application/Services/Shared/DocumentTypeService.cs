using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.DocumentType;
using QTD2.Infrastructure.Model.TreeDataVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Services.Shared
{
	public class DocumentTypeService : IDocumentTypeService
	{
		private readonly Domain.Interfaces.Service.Core.IDocumentTypeService _documentTypeService;
		private readonly Domain.Interfaces.Service.Core.IEmployeeService _employeeService;
		private readonly Domain.Interfaces.Service.Core.IPositionService _positionService;
		private readonly Domain.Interfaces.Service.Core.IILAService _ilaService;
		private readonly Domain.Interfaces.Service.Core.IProviderService _providerService;

		private readonly QTD2.Application.Interfaces.Factories.ITreeItemViewModelFactory _treeViewModelFactory;

		public DocumentTypeService(
			Domain.Interfaces.Service.Core.IDocumentTypeService documentTypeService,
			Domain.Interfaces.Service.Core.IEmployeeService employeeService,
			Domain.Interfaces.Service.Core.IPositionService positionService,
			Domain.Interfaces.Service.Core.IILAService ilaService,
			Domain.Interfaces.Service.Core.IProviderService providerService,
			QTD2.Application.Interfaces.Factories.ITreeItemViewModelFactory treeViewModelFactory)
		{
			_documentTypeService = documentTypeService;
			_employeeService = employeeService;
			_positionService = positionService;
			_ilaService = ilaService;
			_providerService = providerService;
			_treeViewModelFactory = treeViewModelFactory;
		}
		public async Task<List<DocumentTypeViewModel>> GetAllActiveAsync()
		{
			var documentTypes = await _documentTypeService.GetAllActiveAsync();
			var documentTypeViewModels = documentTypes.Select(s => new DocumentTypeViewModel
			{
				Id = s.Id,
				Name = s.Name,
				LinkedDataType = s.LinkedDataType,
			}).OrderBy(ob => ob.Id).ToList();
			return documentTypeViewModels;
		}

		public async Task<TreeItemViewModel> GetActiveLinkToDataOptionsAsync(int documentTypeId)
		{
			return await _treeViewModelFactory.BuildModelAsync("DOCUMENTTYPE", documentTypeId);
		}
	}
}
