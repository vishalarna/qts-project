using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Application.Interfaces.Factories;
using QTD2.Infrastructure.Model.TreeDataVMs;

namespace QTD2.Application.Factories
{
	public class ToolTreeItemViewModelBuilder : ITreeItemViewModelBuilder
	{
		private readonly Domain.Interfaces.Service.Core.IToolCategoryService _toolCategoryService;

		public ToolTreeItemViewModelBuilder(Domain.Interfaces.Service.Core.IToolCategoryService toolCategoryService)
		{
			_toolCategoryService = toolCategoryService;
		}

		public async Task<Infrastructure.Model.TreeDataVMs.TreeItemViewModel> BuildModel()
		{
			var result = new Infrastructure.Model.TreeDataVMs.TreeItemViewModel();

			var dataset = await _toolCategoryService.GetToolCategoryByToolAsync();
			result =
			new Infrastructure.Model.TreeDataVMs.TreeItemViewModel()
			{
				Label = "Tool Category",
				Searchable = false,
				TreeItemOptions = dataset.OrderBy((toolCategory) => toolCategory.Title).Select((toolCategory) =>
				new TreeItemOptionViewModel()
				{
					Id = toolCategory.Id,
					Display = toolCategory.Title,
					SubTreeItem =
						new Infrastructure.Model.TreeDataVMs.TreeItemViewModel()
						{
							Label = "Tool",
							TreeItemOptions = toolCategory.Tools.Where(tool => tool.Active).Select(tool => new TreeItemOptionViewModel()
							{
								Id = tool.Id,
								Display = tool.Name
							}).ToList()
						}
				}).ToList()
			};
	

			return result;
		}
	}
}
