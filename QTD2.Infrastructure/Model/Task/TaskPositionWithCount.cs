using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task
{
    public class TaskPositionWithCount
    {
        public TaskPositionWithCount(Domain.Entities.Core.Position position, int count)
        {
            this.position = position;
            Count = count;
        }

        public QTD2.Domain.Entities.Core.Position position { get; set; }
        public int Count { get; set; }
    }
}
