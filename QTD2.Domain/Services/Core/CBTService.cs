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
    public class CBTService : Common.Service<CBT>, ICBTService
    {
        public CBTService(ICBTRepository repository, ICBTValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<CBT>> GetByILAIdListAsync(List<int> list)
        {
            return (await FindAsync(r => list.Contains(r.ILAId))).ToList();
        }

        public async Task<CBT> GetForNotificationAsync(int cbtId)
        {
            var cbts = await FindWithIncludeAsync(r => r.Id == cbtId, new[] { "ILA" });

            return cbts.First();
        }

        public async Task<List<string>> GetCBTsScormUploadNamesByILAId(int ilaId)
        {
            var cbtSettings = (await FindWithIncludeAsync(x => x.ILAId == ilaId, new string[] { "ScormUploads" }));
            return cbtSettings.SelectMany(x => x.ScormUploads.Select(y => y.Name)).ToList();
        }

        public async Task<CBT> GetActiveByIlaAsync(int ilaId)
        {
            var cbt = await FindAsync(r => r.ILAId == ilaId && r.Active);
            return cbt.FirstOrDefault();
        }
    }
}
