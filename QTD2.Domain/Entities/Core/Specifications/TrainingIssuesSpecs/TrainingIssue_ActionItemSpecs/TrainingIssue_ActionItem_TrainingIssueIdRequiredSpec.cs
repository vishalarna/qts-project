using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TrainingIssuesSpecs.TrainingIssue_ActionItemSpecs
{
    public class TrainingIssue_ActionItem_TrainingIssueIdRequiredSpec : ISpecification<TrainingIssue_ActionItem>
    {
        public bool IsSatisfiedBy(TrainingIssue_ActionItem entity, params object[] args)
        {
            return entity.TrainingIssueId > 0;
        }
    }
}
