using System.Linq;
using System.Threading.Tasks;

using QTD2.Application.Interfaces.Factories;
using QTD2.Infrastructure.Model.TreeDataVMs;

namespace QTD2.Application.Factories
{
	class TestTreeItemViewModelBuilder : ITreeItemViewModelBuilder
	{
		private readonly Domain.Interfaces.Service.Core.IProviderService _providerService;

		public TestTreeItemViewModelBuilder(Domain.Interfaces.Service.Core.IProviderService providerService)
		{
			_providerService = providerService;
		}

		public async Task<TreeItemViewModel> BuildModel()
		{
			var result = new Infrastructure.Model.TreeDataVMs.TreeItemViewModel();

			var dataset = await _providerService.GetProvidersWithILAClassScheduleEmployeesAndTests();

			result =
				new Infrastructure.Model.TreeDataVMs.TreeItemViewModel()
				{
					Label = "Provider",
					TreeItemOptions = dataset.OrderByDescending((provider) => provider.IsPriority).ThenBy((provider) => provider.Name).Select((provider) =>
					new TreeItemOptionViewModel()
					{
						Id = provider.Id,
						Display = provider.Name,
						SubTreeItem =
						new Infrastructure.Model.TreeDataVMs.TreeItemViewModel()
						{
							Label = "ILA",
							TreeItemOptions = provider.ILAs.Where((ila) => ila.Active).Select((ila) =>
							new TreeItemOptionViewModel
							{
								Id = ila.Id,
								Display = ila.Name,
								SubTreeItem =
								new Infrastructure.Model.TreeDataVMs.TreeItemViewModel()
								{
									Label = "Class Schedule",
									TreeItemOptions = ila.ClassSchedules.Where((classSchedule) => classSchedule.Active).Select((classSchedule) =>
									new TreeItemOptionViewModel()
									{
										Id = classSchedule.Id,
										Display = classSchedule.StartDateTime.ToString("yyyy-MM-dd"),
										SubTreeItem =
										new Infrastructure.Model.TreeDataVMs.TreeItemViewModel()
										{
											Label = "Employee",
											TreeItemOptions = classSchedule.ClassSchedule_Employee.Where((classScheduleEmployee) => classScheduleEmployee.Active).Select((classScheduleEmployee) =>
											new TreeItemOptionViewModel()
											{
												Id = classScheduleEmployee.Employee.Id,
												Display = classScheduleEmployee.Employee.Person.FirstName + ' ' + classScheduleEmployee.Employee.Person.LastName,
												SubTreeItem =
												new Infrastructure.Model.TreeDataVMs.TreeItemViewModel()
												{
													Label = "Class Schedule Roster",
													TreeItemOptions = classSchedule.ClassSchedule_Rosters.Where((classScheduleRoster) => classScheduleRoster.Active && classScheduleRoster.ClassScheduleId == classSchedule.Id && classScheduleRoster.EmpId == classScheduleEmployee.Employee.Id).Select((classScheduleRoster) =>
													new TreeItemOptionViewModel()
													{
														Id = classScheduleRoster.Id,
														Display = classScheduleRoster.Test.TestTitle
													}).ToList()
												}
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
