using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Domain.Entities.Core;
using MediatR;

namespace QTD2.Domain.Events.Core
{
    public class OnProcedureReview_EmployeeCreated : Common.IDomainEvent, INotification
    {
        public ProcedureReview_Employee ProcedureReview_Employee { get; }

        public OnProcedureReview_EmployeeCreated(ProcedureReview_Employee procedureReviewEmployee)
        {
            ProcedureReview_Employee = procedureReviewEmployee;
        }
    }
}
