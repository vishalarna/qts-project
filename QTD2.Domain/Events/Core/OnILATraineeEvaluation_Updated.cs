using MediatR;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Events.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnILATraineeEvaluation_Updated : IDomainEvent, INotification
    {
        public ILATraineeEvaluation ILATraineeEvaluation { get; }

        public OnILATraineeEvaluation_Updated(ILATraineeEvaluation eval)
        {
            ILATraineeEvaluation = eval;
        }
    }

}
