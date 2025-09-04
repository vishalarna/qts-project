using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnILA_StudentEvaluation_Link_Removed : Common.IDomainEvent, INotification
    {
        public ILA_StudentEvaluation_Link ILA_StudentEvaluation_Link;

        public OnILA_StudentEvaluation_Link_Removed(ILA_StudentEvaluation_Link iLA_StudentEvaluation_Link)
        {
            ILA_StudentEvaluation_Link = iLA_StudentEvaluation_Link;
        }
    }
}
