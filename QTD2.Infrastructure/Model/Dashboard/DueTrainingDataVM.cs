using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Dashboard
{
    public class DueTrainingDataVM
    {
        public int? Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public int? ParentId { get; set; }
        public bool CanStart { get; set; }
        public DateTime? DueDate { get; set; }
        public string DueDateFormatted => DueDate?.ToShortDateString();

        public DueTrainingDataVM() { }
    }
}
