using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Domain.Entities.Core;
using MediatR;

namespace QTD2.Domain.Events.Core
{
    public class OnProcedureReviewPublished : Common.IDomainEvent, INotification
    {
        public ProcedureReview ProcedureReview { get; }

        public OnProcedureReviewPublished(ProcedureReview procedureReview)
        {
            ProcedureReview = procedureReview;
        }
    }
}
