using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.SaftyHazard;
using QTD2.Infrastructure.Model.Task;
using QTD2.Infrastructure.Model.Tool;
using QTD2.Infrastructure.Model.ToolCategory;
using QTD2.Infrastructure.Model.ToolGroup;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IToolService
    { 
        Task<List<Tool>> GetAsync();

        Task<Tool> GetAsync(int id);

        Task<Tool> CreateAsync(ToolCreateOptions options);

        Task<Tool> UpdateAsync(int id, ToolUpdateOptions options);

        System.Threading.Tasks.Task DeleteAsync(int id);

        Task<List<ToolGroup>> GetToolGroupsAsync();

        Task<ToolGroup> GetToolGroupAsync(int toolGroupId);

        Task<ToolGroup> CreateToolGroupAsync(ToolGroupCreateOptions options);

        Task<ToolGroup> UpdateToolGroupAsync(int toolGroupId, ToolGroupUpdateOptions options);

        System.Threading.Tasks.Task DeleteToolGroupAsync(int toolGroupId);

        Task<List<Tool>> AddTooltoToolGroupAsync(int toolGroupId, ToolAddOptions options);

        System.Threading.Tasks.Task RemoveToolFromToolGroupAsync(int toolGroupId, int toolId);

        // Tool Categories
        Task<List<ToolCategory>> GetToolCategoriesAsync(bool includeTools = false);

        Task<ToolCategory> GetToolCategoryAsync(int id);

        Task<ToolCategory> CreateToolCategoryAsync(ToolCategoryCreateOptions options);
        System.Threading.Tasks.Task DeleteToolAsync(int id);

        Task<ToolCategory> UpdateToolCategoryAsync(int id, ToolCategoryUpdateOptions options);
        Task<Tool> UpdateToolAsync(int id, ToolCreateOptions options);


        System.Threading.Tasks.Task DeleteToolCategoryAsync(int id);
        Task<List<ToolNestedData>> GetToolsWithCategoriesAsync();
        Task<ToolDashboardModel> GetToolsStatistics();
        System.Threading.Tasks.Task LinksafetyHazardsByTools(LinkToolsOptions options);

        System.Threading.Tasks.Task LinkEOsByTools(LinkToolsOptions options);
        System.Threading.Tasks.Task LinkTasksByTools(LinkToolsOptions options);
        System.Threading.Tasks.Task unLinkTasksByTools(LinkToolsOptions options);
        System.Threading.Tasks.Task unLinkEOsByTools(LinkToolsOptions options);
        System.Threading.Tasks.Task unLinksafetyHazardsByTools(LinkToolsOptions options);

        Task<List<TaskWithCountOptions>> GetLinkedTasksByToolId(int id);
        Task<List<EnablingObjectiveWithCountOptions>> GetLinkedSkillsByToolId(int id);
        Task<List<SafetyHazardWithLinkCount>> GetLinkedsafetyHazardsByToolId(int id);



        System.Threading.Tasks.Task ActiveToolCategoryAsync(int id);
        System.Threading.Tasks.Task InActiveToolAsync(int id);
        System.Threading.Tasks.Task ActiveToolAsync(int id);


        System.Threading.Tasks.Task InActiveToolCategoryAsync(int id);

        Task<int> GetToolNumber(int catId);

        Task<List<Tool>> GetToolTaskIsLinkedTo(int id);

        Task<List<Tool>> GetToolsEoIsLinkedTo(int id);

        Task<List<Tool>> GetToolsSHIsLinkedTo(int id);

        Task<List<Tool>> GetToolList(string notLinkedWith);

        Task<List<ToolCategory>> GetCategoryList(string notLinkedWith);
        
        Task<Result<IEnumerable<UnlinkedToolDto>>> GetToolsNotLinkedToTaskAsync();
        Task<Result<IEnumerable<UnlinkedToolDto>>> GetToolsNotLinkedToEoAsync();
    }
}
