using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TrainingIssuesSpecs.TrainingIssue_ActionItemPrioritySpecs
{
    public class TrainingIssue_ActionItemPriorityRequiredSpec : ISpecification<TrainingIssue_ActionItemPriority>
    {
        public bool IsSatisfiedBy(TrainingIssue_ActionItemPriority entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Priority);
        }
    }
}
