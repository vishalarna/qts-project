using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task
{
    public class TaskVersionVM
    {
        public TaskVersionVM(string createdBy, DateTime createdDate, int id, string versionNumber)
        {
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            Id = id;
            VersionNumber = versionNumber;
        }

        public TaskVersionVM()
        {
        }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Id { get; set; }

        public string TaskDescription { get; set; }

        public string Title { get; set; }

        public string VersionNumber { get; set; }

    }
}
