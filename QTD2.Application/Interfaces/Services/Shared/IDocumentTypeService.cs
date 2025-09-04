using QTD2.Infrastructure.Model.DocumentType;
using QTD2.Infrastructure.Model.TreeDataVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IDocumentTypeService
    {
        System.Threading.Tasks.Task<List<DocumentTypeViewModel>> GetAllActiveAsync();
        System.Threading.Tasks.Task<TreeItemViewModel> GetActiveLinkToDataOptionsAsync(int documentTypeId);
    }
}
