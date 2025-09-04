using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Factories
{
    public interface ITreeItemViewModelFactory
    {
        Task<QTD2.Infrastructure.Model.TreeDataVMs.TreeItemViewModel> BuildModelAsync(string entity, int entityId);
    }
}
