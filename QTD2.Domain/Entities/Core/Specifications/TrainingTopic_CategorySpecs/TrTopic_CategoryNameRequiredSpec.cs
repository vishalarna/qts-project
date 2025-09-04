using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TrainingTopic_CategorySpecs
{
    public class TrTopic_CategoryNameRequiredSpec : ISpecification<TrainingTopic_Category>
    {
        public bool IsSatisfiedBy(TrainingTopic_Category entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Name);
        }
    }
}
