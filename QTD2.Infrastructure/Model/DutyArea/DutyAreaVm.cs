using QTD2.Infrastructure.Model.SubdutyArea;
using QTD2.Infrastructure.Model.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.DutyArea
{
    public class DutyAreaVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Letter { get; set; }
        public int Number { get; set; }
        public List<SubDutyAreaVm> SubDutyArea { get; set; } = new List<SubDutyAreaVm>();
    }
}
