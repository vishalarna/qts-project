using System.Linq;
using System.Threading.Tasks;

using QTD2.Application.Interfaces.Factories;
using QTD2.Infrastructure.Model.TreeDataVMs;

namespace QTD2.Application.Factories
{
    public class SignInSheetTreeItemViewModelBuilder : ITreeItemViewModelBuilder
    {
		private readonly Domain.Interfaces.Service.Core.IProviderService _providerService;

		public SignInSheetTreeItemViewModelBuilder(Domain.Interfaces.Service.Core.IProviderService providerService)
        {
			_providerService = providerService;
		}

		public async Task<Infrastructure.Model.TreeDataVMs.TreeItemViewModel> BuildModel()
        {
			var result = new Infrastructure.Model.TreeDataVMs.TreeItemViewModel();

			var dataset = await _providerService.GetProvidersWithILAsAndClassSchedules();
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
										Display = classSchedule.StartDateTime.ToString("yyyy-MM-dd")
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
