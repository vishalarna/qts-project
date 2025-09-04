using System.Linq;
using System.Threading.Tasks;
using QTD2.Application.Interfaces.Factories;
using QTD2.Infrastructure.Model.TreeDataVMs;

namespace QTD2.Application.Factories
{
	class TaskQualificationTreeItemViewModelBuilder : ITreeItemViewModelBuilder
	{
		private readonly Domain.Interfaces.Service.Core.IDutyAreaService _dutyAreaService;

		public TaskQualificationTreeItemViewModelBuilder(Domain.Interfaces.Service.Core.IDutyAreaService dutyAreaService)
		{
			_dutyAreaService = dutyAreaService;
		}

		public async Task<TreeItemViewModel> BuildModel()
		{
			var result = new Infrastructure.Model.TreeDataVMs.TreeItemViewModel();

			var dataset = await _dutyAreaService.GetDutyAreasWithSubDutyAreaTaskTaskQualificationEmployees();

			result =
				new Infrastructure.Model.TreeDataVMs.TreeItemViewModel()
				{
					Label = "Duty Area",
					Searchable=true,
					TreeItemOptions = dataset.OrderBy((dutyArea) => dutyArea.Letter).ThenBy((dutyArea) => dutyArea.Number).Select((dutyArea) =>
					new TreeItemOptionViewModel()
					{
						Id = dutyArea.Id,
						Display = dutyArea.Title,
						SubTreeItem =
						new Infrastructure.Model.TreeDataVMs.TreeItemViewModel()
						{
							Label = "Sub Duty Area",
							Searchable=true,
							TreeItemOptions = dutyArea.SubdutyAreas.Where((subDutyArea) => subDutyArea.Active).OrderBy((subDutyArea) => subDutyArea.SubNumber).Select((subDutyArea) =>
							new TreeItemOptionViewModel
							{
								Id = subDutyArea.Id,
								Display = subDutyArea.Title,
								SubTreeItem =
								new Infrastructure.Model.TreeDataVMs.TreeItemViewModel()
								{
									Label = "Task",
									TreeItemOptions = subDutyArea.Tasks.Where((task) => task.Active).OrderBy((task) => task.Number).Select((task) =>
									new TreeItemOptionViewModel()
									{
										Id = task.Id,
										Display = task.Description,
										SubTreeItem =
										new Infrastructure.Model.TreeDataVMs.TreeItemViewModel()
										{
											Label = "Task Qualification",
											TreeItemOptions = task.TaskQualifications.Where((taskQualification) => taskQualification.Active).Select((taskQualification) =>
											new TreeItemOptionViewModel()
											{
												Id = taskQualification.Id,
												Display = taskQualification.Employee.Person.FirstName + ' ' + taskQualification.Employee.Person.LastName
											}).ToList()
										}
									}).ToList()
								}
							}).ToList()
						}
					}).ToList()
				};

			return result;
		}
	}
}
