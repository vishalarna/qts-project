using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Task_ReferenceSpecs
{
    public class Task_ReferenceDescriptionRequiredSpec : ISpecification<Task_Reference>
    {
        public bool IsSatisfiedBy(Task_Reference entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Description);
        }
    }
}
