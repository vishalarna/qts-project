using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.IDP
{
    public class IDP_ReviewCreateOptions
    {
        public string Title { get; set; }

        public string Instructions { get; set; }

        public string Comments { get; set; }

        public DateTime? CompletedDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsStarted { get; set; }

        public int EmployeeId { get; set; }
    }
}
