using QTD2.Infrastructure.Model.Task_Requalification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task
{
    public class TaskWithNumberVM
    {
        public int DANumber { get; set; }

        public int SDANumber { get; set; }

        public string Letter { get; set; }

        public Domain.Entities.Core.Task Task { get; set; }

        public string CompleteNumber { get; set; }

        public int TQId { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string RequiredRequals { get; set; }

        public bool canStart { get; set; }
        public string? Status { get; set; }
        public List<TQEmpWithTasksVM> TQEmpWithTasksVM { get; set; } = new List<TQEmpWithTasksVM>();
    }
}
