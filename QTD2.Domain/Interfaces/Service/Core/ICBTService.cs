using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ICBTService : Common.IService<CBT>
    {
        System.Threading.Tasks.Task<List<CBT>> GetByILAIdListAsync(List<int> list);
        System.Threading.Tasks.Task<CBT> GetForNotificationAsync(int cbtId);
        System.Threading.Tasks.Task<List<string>> GetCBTsScormUploadNamesByILAId(int ilaId);
        System.Threading.Tasks.Task<CBT> GetActiveByIlaAsync(int ilaId);
    }
}
