using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Infrastructure.Model.TreeDataVMs;

namespace QTD2.Application.Interfaces.Factories
{
    public interface ITreeItemViewModelBuilder
    {
        Task<TreeItemViewModel> BuildModel();
    }
}
