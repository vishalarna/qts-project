using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Application.Interfaces.Factories;
using QTD2.Infrastructure.Model.TreeDataVMs;

namespace QTD2.Application.Factories
{
	public class EmployeeDetailsTreeItemViewModelBuilder : ITreeItemViewModelBuilder
	{
		private readonly Domain.Interfaces.Service.Core.IEmployeeService _employeeService;
		public EmployeeDetailsTreeItemViewModelBuilder(Domain.Interfaces.Service.Core.IEmployeeService employeeService)
		{
			_employeeService = employeeService;
		}

		public async Task<Infrastructure.Model.TreeDataVMs.TreeItemViewModel> BuildModel()
		{
			var result = new Infrastructure.Model.TreeDataVMs.TreeItemViewModel();

			var dataset = await _employeeService.GetEmployeeDetailsWithPerson();
			result =
			new Infrastructure.Model.TreeDataVMs.TreeItemViewModel()
			{
				Label = "Employee",
				Searchable =true,
				TreeItemOptions = dataset.OrderBy((employee) => employee.Person.FirstName).Select((employee) =>
				new TreeItemOptionViewModel()
				{
					Id = employee.Id,
					Display = employee.Person.FirstName + ' ' + employee.Person.LastName,
				}).ToList()
			};

			return result;
		}
	}
}
