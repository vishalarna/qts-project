using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.AdminMessage_Specs
{
    public class AdminMessage_MessageSpec :ISpecification<AdminMessage>
    {
        public bool IsSatisfiedBy(AdminMessage entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Message);
        }
    }
}
