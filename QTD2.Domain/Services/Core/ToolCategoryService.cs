using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ToolCategoryService : Common.Service<ToolCategory>, IToolCategoryService
    {
        public ToolCategoryService(IToolCategoryRepository repository, IToolCategoryValidation validation)
            : base(repository, validation)
        {
        }
        public async Task<List<ToolCategory>> GetToolCategoryByToolAsync()
        {
            var queryable = await FindWithIncludeAsync(x => x.Active, new string[] { "Tools" });
            return queryable.ToList();
        }

    }
}
