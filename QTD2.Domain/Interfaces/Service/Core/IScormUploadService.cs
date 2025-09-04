using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
   public interface IScormUploadService : Common.IService<CBT_ScormUpload>
    {
        public System.Threading.Tasks.Task<List<CBT_ScormUpload>> GetAllByCBTIdAsync(int ilaId, bool current);
        System.Threading.Tasks.Task<CBT_ScormUpload> GetByIlaIdAsync(int iLAID);
        public System.Threading.Tasks.Task<List<CBT_ScormUpload>> GetAllCBTScormUploadsAsync();
    }
}
