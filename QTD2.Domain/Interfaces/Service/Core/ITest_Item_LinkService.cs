using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ITest_Item_LinkService : Common.IService<Test_Item_Link>
    {
        System.Threading.Tasks.Task<List<Test_Item_Link>> GetAllTestSpecificationAsync(IEnumerable<int> testIds);
        System.Threading.Tasks.Task<List<Test_Item_Link>> GetTestItemLinksByTestIdAsync(int testId);
    }
}
