using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task
{
    public class TaskVm
    {
        public int Id { get; set; }
        public int SubdutyAreaId { get; set; }
        public string Description { get; set; }
        public int Number { get; set; }
        public string FullNumber { get; set; }
        public List<int> PositionTaskIds { get; set; } = new List<int>();
    }
}
