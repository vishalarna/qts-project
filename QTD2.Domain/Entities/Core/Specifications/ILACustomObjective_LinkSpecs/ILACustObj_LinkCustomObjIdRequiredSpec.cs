using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ILACustomObjective_LinkSpecs
{
    public class ILACustObj_LinkCustomObjIdRequiredSpec : ISpecification<ILACustomObjective_Link>
    {
        public bool IsSatisfiedBy(ILACustomObjective_Link entity, params object[] args)
        {
            return entity.CustomObjId > 0;
        }
    }
}
