using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TrainingGroup_CategorySpecs
{
    public class TrainingGroup_CategoryTitleRequiredSpec : ISpecification<TrainingGroup_Category>
    {
        public bool IsSatisfiedBy(TrainingGroup_Category entity, params object[] args)
        {
            return !String.IsNullOrEmpty(entity.Title);
        }
    }
}
