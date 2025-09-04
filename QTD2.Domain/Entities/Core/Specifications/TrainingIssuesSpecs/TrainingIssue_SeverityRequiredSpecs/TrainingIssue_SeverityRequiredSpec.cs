using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TrainingIssuesSpecs.TrainingIssue_SeverityRequiredSpecs
{
    public class TrainingIssue_SeverityRequiredSpec : ISpecification<TrainingIssue_Severity>
    {
        public bool IsSatisfiedBy(TrainingIssue_Severity entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Severity);
        }
    }
}
