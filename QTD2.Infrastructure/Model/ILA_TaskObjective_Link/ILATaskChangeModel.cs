using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA_TaskObjective_Link
{
    public class ILATaskChangeModel
    {
        public List<QTD2.Domain.Entities.Core.Task> TasksAdded { get; set; } = new List<Domain.Entities.Core.Task>();
        public List<QTD2.Domain.Entities.Core.Task> TasksRemoved { get; set; } = new List<Domain.Entities.Core.Task>();
    }
}
