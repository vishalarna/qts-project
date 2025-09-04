using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using MediatR;

namespace QTD2.Domain.Events.Core
{
    public class OnMetaILA_MemberLink_Deleted : Common.IDomainEvent, INotification
    {
        public Meta_ILAMembers_Link Meta_ILAMembers_Link { get; set; }

        public OnMetaILA_MemberLink_Deleted(Meta_ILAMembers_Link meta_ILAMembers_Link)
        {
            Meta_ILAMembers_Link = meta_ILAMembers_Link;
        }
    }
}
