using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IMeta_ILAMembers_LinkService : Common.IService<Meta_ILAMembers_Link>
    {
        public Task<Meta_ILAMembers_Link> UpdateAsync(int iLAID, int metaILAID, int? metaILAConfigPublishOptionID, int sequenceNumber,DateTime? startDate);
        Task<Meta_ILAMembers_Link> GetForNotificationAsync(int nextMeta_ILAMembers_LinkId);
        Task<List<Meta_ILAMembers_Link>> GetMetaILAMembersByMetaILAIdAsync(int metaILAId);
        Task<List<Meta_ILAMembers_Link>> GetMetaILAMembersByIdsAsync(int metaILAId, List<int> ilaIds);
    }
}
