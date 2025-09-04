using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TrainingIssuesSpecs.TrainingIssue_StatusSpecs
{
    public class TrainingIssue_StatusRequiredSpec : ISpecification<TrainingIssue_Status>
    {
        public bool IsSatisfiedBy(TrainingIssue_Status entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Status);
        }
    }
}
