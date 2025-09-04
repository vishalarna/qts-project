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
    public class Meta_ILAMembers_LinkService : Common.Service<Meta_ILAMembers_Link>, IMeta_ILAMembers_LinkService
    {
        public Meta_ILAMembers_LinkService(IMeta_ILAMembers_LinkRepository repository, IMeta_ILAMembers_LinkValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<Meta_ILAMembers_Link> UpdateAsync(int iLAID, int metaILAID, int? metaILAConfigPublishOptionID, int sequenceNumber,DateTime? startDate)
        {
            var obj = await GetAsync(iLAID,metaILAID);
                obj.MetaILAConfigPublishOptionID = metaILAConfigPublishOptionID;
                obj.SequenceNumber = sequenceNumber;
                obj.ModifiedDate = DateTime.Now;
                obj.StartDate = startDate;
                await UpdateAsync(obj);
                    return obj;
        }

        public async Task<Meta_ILAMembers_Link> GetAsync(int iLAID, int metaILAID)
        {
            var meta_ILAMembers_Link = await FindAsync(x => x.MetaILAID == metaILAID && x.ILAID == iLAID);
            return meta_ILAMembers_Link.FirstOrDefault();
        }

        public async Task<Meta_ILAMembers_Link> GetForNotificationAsync(int nextMeta_ILAMembers_LinkId)
        {
           return (await FindWithIncludeAsync(r => r.Id == nextMeta_ILAMembers_LinkId, new string[] { "ILA" })).FirstOrDefault();
        }

        public async Task<List<Meta_ILAMembers_Link>> GetMetaILAMembersByMetaILAIdAsync(int metaILAId)
        {
            return (await FindWithIncludeAsync(r => r.MetaILAID == metaILAId, new[] { "ILA" })).ToList();
        }

        public async Task<List<Meta_ILAMembers_Link>> GetMetaILAMembersByIdsAsync(int metaILAId,List<int> ilaIds)
        {
            return (await FindWithIncludeAsync(m => ilaIds.Contains(m.ILAID) && m.MetaILAID == metaILAId, new[] { "ILA.Provider", "ILA.DeliveryMethod", "MetaILAConfigurationPublishOption" })).ToList();
        }
    }
}