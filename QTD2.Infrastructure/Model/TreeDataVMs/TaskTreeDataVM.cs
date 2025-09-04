using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TreeDataVMs
{
    public class TaskTreeDataVM
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int Number { get; set; }

        public bool Active { get; set; }

        public bool IsMeta { get; set; }

        public bool IsReliability { get; set; }
        public ICollection<Position_Task> Position_Tasks { get; set; } = new List<Position_Task>();
    }

    public class SubDutyAreaTreeVM
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public string Title { get; set; }

        public int SubNumber { get; set; }

        public List<TaskTreeDataVM> Tasks { get; set; } = new List<TaskTreeDataVM>();
    }

    public class DutyAreaTreeVM
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool Active { get; set; }

        public string Letter { get; set; }

        public int Number { get; set; }

        public List<SubDutyAreaTreeVM> SubdutyAreas { get; set; } = new List<SubDutyAreaTreeVM>();
    }
}
