using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.Document;
using QTD2.Infrastructure.QTDSettings;

namespace QTD2.Application.Services.Shared
{
    public class DocumentService : IDocumentService
    {
        private readonly Domain.Interfaces.Service.Core.IDocumentService _documentService;
        private readonly Domain.Interfaces.Service.Core.IDocumentTypeService _documentTypeService;
        private readonly Domain.Interfaces.Service.Core.IEmployeeService _employeeService;
        private readonly Domain.Interfaces.Service.Core.ITrainingProgramReviewService _trainingProgramReviewService;
        private readonly Domain.Interfaces.Service.Core.IPositionService _positionService;
        private readonly Domain.Interfaces.Service.Core.IILAService _ilaService;
        private readonly Domain.Interfaces.Service.Core.IClassScheduleEmployeeService _classScheduleEmployeeService;
        private readonly Domain.Interfaces.Service.Core.IClassScheduleService _classScheduleService;
        private readonly Domain.Interfaces.Service.Core.IClassSchedule_RosterService _classScheduleRosterService;
        private readonly Domain.Interfaces.Service.Core.ITaskQualificationService _taskQualificationService;
        private readonly Domain.Interfaces.Service.Core.IToolService _toolService;
        private readonly Domain.Interfaces.Service.Core.ITaskListReviewService _taskListReviewService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly QTDSettings _qtdSettings;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<DocumentService> _localizer;


        public DocumentService(
            Domain.Interfaces.Service.Core.IDocumentService documentService, 
            Domain.Interfaces.Service.Core.IDocumentTypeService documentTypeService, 
            Domain.Interfaces.Service.Core.IEmployeeService employeeService, 
            Domain.Interfaces.Service.Core.IPositionService positionService,
            Domain.Interfaces.Service.Core.ITrainingProgramReviewService trainingProgramReviewService,
            Domain.Interfaces.Service.Core.IILAService ilaService,
            Domain.Interfaces.Service.Core.IClassScheduleEmployeeService classScheduleEmployeeService,
            Domain.Interfaces.Service.Core.IClassScheduleService classScheduleService,
            Domain.Interfaces.Service.Core.IClassSchedule_RosterService classScheduleRosterService,
            Domain.Interfaces.Service.Core.ITaskQualificationService taskQualificationService,
             Domain.Interfaces.Service.Core.IToolService toolService,
             Domain.Interfaces.Service.Core.ITaskListReviewService taskListReviewService,
        IHttpContextAccessor httpContextAccessor,
            UserManager<AppUser> userManager,
            IOptions<QTDSettings> qtdSettings,
            IAuthorizationService authorizationService,
            IStringLocalizer<DocumentService> localizer)
		{
			_documentService = documentService;
			_documentTypeService = documentTypeService;
			_employeeService = employeeService;
			_positionService = positionService;
            _trainingProgramReviewService = trainingProgramReviewService;
            _ilaService = ilaService;
            _classScheduleEmployeeService = classScheduleEmployeeService;
            _classScheduleService = classScheduleService;
            _classScheduleRosterService = classScheduleRosterService;
            _taskQualificationService = taskQualificationService;
            _toolService = toolService;
            _taskListReviewService = taskListReviewService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _qtdSettings = qtdSettings.Value;
            _authorizationService = authorizationService;
            _localizer = localizer;
        }

		public async Task<List<DocumentViewModel>> GetAllActiveAsync()
        {
            var documents = await _documentService.GetAllActiveAsync();
            var filterDocuments = documents;

            for (int i = filterDocuments.Count - 1; i >= 0; i--)
            {
                var doc = filterDocuments[i];
                var linkedDataName = await GetLinkedDataNameAsync(doc.DocumentType.LinkedDataType, doc.LinkedDataId);
                if (linkedDataName == null)
                {
                    documents.Remove(doc);
                }
            }

            return await MapDocumentsToDocumentViewModelsAsync(documents);
        }

        public async Task<DocumentViewModel> CreateAsync(DocumentCreationOptions options)
        {
            if(options == null || options.file==null)
                throw new ArgumentNullException(nameof(options));
            else
            {
                var instanceName = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == Domain.CustomClaimTypes.InstanceName).Select(c => c.Value).SingleOrDefault();
                string input = options.file;
                string[] parts = input.Split(new string[] { "base64," }, StringSplitOptions.None);
                string actualBase64 = parts[1];
                string[] subparts = parts[0].Split(new string[] { ";data" }, StringSplitOptions.None);
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + subparts[0];
                byte[] fileBytes = Convert.FromBase64String(actualBase64);
                string filePath = "documents/" + instanceName + "/" + fileName;
                string outputPath = _qtdSettings.SaveFilePath + "documents\\" + instanceName + "\\";
                if (!Directory.Exists(outputPath))
                    Directory.CreateDirectory(outputPath);
                outputPath += fileName;
                File.WriteAllBytes(outputPath, fileBytes);
                var document = new QTD2.Domain.Entities.Core.Document
                {
                    FileName = fileName,
                    FilePath = filePath,
                    DateAdded = DateTime.UtcNow,
                    Comments = options.Comments,
                    DocumentTypeId = options.DocumentTypeId,
                    LinkedDataId = Convert.ToInt32(options.LinkedDataId)
                };
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, document, AuthorizationOperations.Create);
                
                document.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                document.CreatedDate = DateTime.Now;
                var validationResult = await _documentService.AddAsync(document);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                
                return await MapDocumentToDocumentViewModelAsync(document);
            }
        }

        public async Task<DocumentViewModel> GetActiveAsync(int id)
        {
            var document = await _documentService.GetActiveAsync(id);
            return await MapDocumentToDocumentViewModelAsync(document);
        }

        public async Task<DocumentViewModel> UpdateActiveAsync(int id, DocumentUpdateOptions options)
        {
            var document = await _documentService.GetActiveAsync(id);
            if(document!=null)
            {
                UpdateDocument(document, options);
                var validationResult = await _documentService.UpdateAsync(document);
                if (validationResult.IsValid)
                {
                    return await MapDocumentToDocumentViewModelAsync(document);
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new QTDServerException("Document Id not found");
            }
        }

        public async Task DeleteActiveAsync(int id)
        {
            var document = await _documentService.GetActiveAsync(id);
            if (document != null)
            {
                document.Delete();
                var validationResult = await _documentService.UpdateAsync(document);
                if (!validationResult.IsValid)
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                throw new QTDServerException("Document Id not found");
            }
        }

        public async Task<string> GetActiveFileAsync(int id)
        {
            var document = await _documentService.GetActiveAsync(id);
            var filePath = document.FilePath;
            if (filePath != null)
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                string base64String = Convert.ToBase64String(fileBytes);
                return base64String;
            }
            else
            {
                throw new QTDServerException("Document Id not found");
            }
        }

        public async Task<List<DocumentViewModel>> GetActiveByDocumentTypeAsync(int documentTypeId)
        {
            var documents = await _documentService.GetActiveByDocumentTypeAsync(documentTypeId);
            return await MapDocumentsToDocumentViewModelsAsync(documents);
        }

        public async Task<List<DocumentViewModel>> GetActiveByLinkedDataAsync(string linkedDataType, int linkedDataId)
        {
            var documents = await _documentService.GetActiveByLinkedDataAsync(linkedDataType,linkedDataId);
            return await MapDocumentsToDocumentViewModelsAsync(documents);
        }

        public async Task<List<DocumentViewModel>> GetActiveByLinkedDataAndDocumentTypeAsync(string linkedDataType, int linkedDataId, int documentTypeId)
        {
            var documents = await _documentService.GetActiveByLinkedDataAndDocumentTypeAsync(linkedDataType, linkedDataId,documentTypeId);
            return await MapDocumentsToDocumentViewModelsAsync (documents);
        }

        public async Task<string> GetLinkedDataNameAsync(string linkedDataType, int linkedDataId)
        {
			switch (linkedDataType)
			{
                case "Employees":
                    var employee = await _employeeService.GetWithPersonAsync(linkedDataId);
                    return employee == null ? null : employee.Person.FirstName + " " + employee.Person.LastName;
                case "TrainingProgramReviews":
                    var trainingProgramReview = await _trainingProgramReviewService.GetAsync(linkedDataId);
                    return trainingProgramReview == null ? null : trainingProgramReview?.TrainingProgram?.ProgramTitle + " - " + trainingProgramReview?.TrainingProgram?.Version;
                case "Positions":
                    return await _positionService.GetPositionTitleByIdAsync(linkedDataId);
                case "ILAs":
                    return await _ilaService.GetNameByIdAsync(linkedDataId);
                case "ClassScheduleEmployees":
                    return await _classScheduleEmployeeService.GetEmployeeNameByIdAsync(linkedDataId);
                case "ClassSchedules":
                    return await _classScheduleService.GetFormattedStartDateByIdAsync(linkedDataId);
                case "ClassScheduleRosters":
                    return await _classScheduleRosterService.GetTestTitleByIdAsync(linkedDataId);
                case "TaskQualifications":
                    return await _taskQualificationService.GetEmployeeNamebyIdAsync(linkedDataId);
                case "Tool":
                    return await _toolService.GetToolsNameByIdAsync(linkedDataId);
                case "TaskListReview":
                    return await _taskListReviewService.GetTitleByIdAsync(linkedDataId);
                default:
                    return "Unknown linkedDataType:\n" + linkedDataType;
			}
		}

        protected void UpdateDocument(Domain.Entities.Core.Document document, DocumentUpdateOptions options)
        {
            if (document.DocumentTypeId != options.DocumentTypeiD)
            {
                document.SetDocumentTypeId(options.DocumentTypeiD);
            }
            if (document.LinkedDataId != options.LinkedDataId)
            {
                document.SetLinkedDataId(options.LinkedDataId);
            }
            if (document.Comments != options.Comments)
            {
                document.SetComments(options.Comments);
            }

            var modifiedBy = _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            if (modifiedBy != null)
            {
                document.Modify(modifiedBy.Result.UserName);
            }
        }

        public async Task<DocumentViewModel>  MapDocumentToDocumentViewModelAsync(Domain.Entities.Core.Document document)
        {
            var documentType = await _documentTypeService.GetActiveAsync(document.DocumentTypeId);
            var displayFileName = document.FileName[(document.FileName.IndexOf("_")+1)..];
            var documentViewModel = new DocumentViewModel
            {
                Id = document.Id,
                FileName = displayFileName,
                Comments = document.Comments,
                DocumentTypeId = document.DocumentTypeId,
                DocumentTypeName = documentType.Name,
                LinkedDataType = documentType.LinkedDataType,
                LinkedDataId = document.LinkedDataId,
                DateAdded = document.DateAdded,
                LinkedDataName = await GetLinkedDataNameAsync(documentType.LinkedDataType, document.LinkedDataId)
            };
            return documentViewModel;
        }

        public async Task<List<DocumentViewModel>> MapDocumentsToDocumentViewModelsAsync(List<Domain.Entities.Core.Document> documents)
        {
            List<DocumentViewModel> documentViewModels = new List<DocumentViewModel>();
            foreach(var document in documents)
            {
                var documentViewModel = await MapDocumentToDocumentViewModelAsync(document);
                documentViewModels.Add(documentViewModel);
            }
            return documentViewModels.OrderByDescending(ob => ob.DateAdded).ToList();
        }
    }
}
