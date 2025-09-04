using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TrainingIssuesSpecs.TrainingIssueSpecs
{
    public class TrainingIssue_SeverityIdRequiredSpec : ISpecification<TrainingIssue>
    {
        public bool IsSatisfiedBy(TrainingIssue entity, params object[] args)
        {
            return entity.SeverityId > 0;
        }
    }
}
