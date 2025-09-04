using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TrainingIssuesSpecs.TrainingIssue_ActionItemStatusSpecs
{
    public class TrainingIssue_ActionItemStatusRequiredSpec : ISpecification<TrainingIssue_ActionItemStatus>
    {
        public bool IsSatisfiedBy(TrainingIssue_ActionItemStatus entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Status);
        }
    }
}
