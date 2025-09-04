using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Authentication.Specifications.AdminMessageAuthSpecs
{
    public class AdminMessageAuth_InstanceSpec : ISpecification<AdminMessageAuth>
    {
        public bool IsSatisfiedBy(AdminMessageAuth entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Instance);
        }
    }
}
