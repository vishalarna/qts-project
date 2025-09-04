using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnILA_Deleted : Common.IDomainEvent, INotification
    {
        public ILA DeletedILA{ get; }

        public OnILA_Deleted(ILA deletedILA)
        {
            DeletedILA = deletedILA;
        }
    }
}
