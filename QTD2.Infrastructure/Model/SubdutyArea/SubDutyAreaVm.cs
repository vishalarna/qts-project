using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Infrastructure.Model.Task;

namespace QTD2.Infrastructure.Model.SubdutyArea
{
    public class SubDutyAreaVm
    {
        public int Id { get; set; }
        public int DutyAreaId { get; set; }
        public string Description { get; set; }
        public int SubNumber { get; set; }
        public string Title { get; set; }
        public List<TaskVm> Task { get; set; } = new List<TaskVm>();
    }
}
