using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ActionItem_OperationType_Links : Common.Entity
    {
        public string ActionItemOperationName { get; set; }
        public int OperationTypeId { get; set; }
        public virtual ActionItem_OperationTypes OperationType { get; set; }
    }
}
