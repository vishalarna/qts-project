using System;
using System.Threading.Tasks;

using QTD2.Application.Interfaces.Factories;

namespace QTD2.Application.Factories
{
	public class TreeItemViewModelFactory : ITreeItemViewModelFactory
	{
		ITreeItemViewModelBuilder _modelBuilder;

		private readonly Domain.Interfaces.Service.Core.ITrainingProgramTypeService _trainingProgramTypeService;
		private readonly Domain.Interfaces.Service.Core.IProviderService _providerService;
		private readonly Domain.Interfaces.Service.Core.IEmployeeService _employeeService;
		private readonly Domain.Interfaces.Service.Core.IDutyAreaService _dutyAreaService;
		private readonly Domain.Interfaces.Service.Core.IToolCategoryService _toolCategoryService;
		private readonly Domain.Interfaces.Service.Core.ITaskListReviewService _taskListReviewService;

		public TreeItemViewModelFactory(
		Domain.Interfaces.Service.Core.ITrainingProgramTypeService trainingProgramTypeService,
			Domain.Interfaces.Service.Core.IEmployeeService employeeService,
			Domain.Interfaces.Service.Core.IProviderService providerService,
			Domain.Interfaces.Service.Core.IDutyAreaService dutyAreaService,
			Domain.Interfaces.Service.Core.IToolCategoryService toolCategoryService,
			Domain.Interfaces.Service.Core.ITaskListReviewService taskListReviewService
		)
		{
			_trainingProgramTypeService = trainingProgramTypeService;
			_employeeService = employeeService;
			_providerService = providerService;
			_dutyAreaService = dutyAreaService;
			_toolCategoryService = toolCategoryService;
			_taskListReviewService = taskListReviewService;
		}

		public async Task<Infrastructure.Model.TreeDataVMs.TreeItemViewModel> BuildModelAsync(string entity, int entityId)
		{
			switch (entity.ToUpper())
			{
				case "DOCUMENTTYPE":
					_modelBuilder = GetDocumentTypeModelBuilder(entityId);
					break;
			}

			return await _modelBuilder.BuildModel();
		}

		private ITreeItemViewModelBuilder GetDocumentTypeModelBuilder(int entityId)
		{
			switch (entityId)
			{
				case 1:
					return new TPReviewSheetTreeItemViewModelBuilder(_trainingProgramTypeService);
				case 2:
					return new EmployeeDetailsTreeItemViewModelBuilder(_employeeService);
				case 5:
					return new OtherEmployeeCourseCompletionInfoTreeItemViewModelBuilder(_providerService);
				case 6:
					return new SignInSheetTreeItemViewModelBuilder(_providerService);
				case 7:
					return new StudentEvaluationTreeItemViewModelBuilder(_providerService);
				case 8:
					return new TaskQualificationTreeItemViewModelBuilder(_dutyAreaService);
				case 9:
					return new TestTreeItemViewModelBuilder(_providerService);
				case 10:
					return new ToolTreeItemViewModelBuilder(_toolCategoryService);
				case 11:
					return new TaskListReviewSupportingDocumentTreeItemViewModelBuilder(_taskListReviewService);
			}

			throw new IndexOutOfRangeException();
		}
	}
}
