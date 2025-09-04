using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.EnablingObjective_SubCategoryHistorySpecs
{
    public class EO_SubCatHisSubCategoryIdRequiredSpec : ISpecification<EnablingObjective_SubCategoryHistory>
    {
        public bool IsSatisfiedBy(EnablingObjective_SubCategoryHistory entity, params object[] args)
        {
            return entity.EnablingObjectiveSubCategoryId > 0;
        }
    }
}
