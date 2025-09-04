using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnTestDeleted : Common.IDomainEvent, INotification
    {
        public Test Test;

        public OnTestDeleted(Test test)
        {
            Test = test;
        }
    }
}
