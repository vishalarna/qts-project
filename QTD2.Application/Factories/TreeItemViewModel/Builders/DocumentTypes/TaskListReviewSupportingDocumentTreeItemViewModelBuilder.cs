using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Application.Interfaces.Factories;
using QTD2.Infrastructure.Model.TreeDataVMs;

namespace QTD2.Application.Factories
{
	public class TaskListReviewSupportingDocumentTreeItemViewModelBuilder : ITreeItemViewModelBuilder
	{
		private readonly Domain.Interfaces.Service.Core.ITaskListReviewService _taskListReviewService;
		public TaskListReviewSupportingDocumentTreeItemViewModelBuilder(Domain.Interfaces.Service.Core.ITaskListReviewService taskListReviewService)
		{
			_taskListReviewService = taskListReviewService;
		}

		public async Task<Infrastructure.Model.TreeDataVMs.TreeItemViewModel> BuildModel()
		{
			var result = new Infrastructure.Model.TreeDataVMs.TreeItemViewModel();

			var dataset = await _taskListReviewService.GetAllAsync();
			result =
			new Infrastructure.Model.TreeDataVMs.TreeItemViewModel()
			{
				Label = "Task List Review",
				Searchable =true,
				TreeItemOptions = dataset.OrderBy((taskListReview) => taskListReview.Title).Select((taskListReview) =>
				new TreeItemOptionViewModel()
				{
					Id = taskListReview.Id,
					Display = taskListReview.Title,
				}).ToList()
			};

			return result;
		}
	}
}
