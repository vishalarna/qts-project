using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.DIFSurvey
{
   public class DIFSurvey_CreateOptions 
    {
        public string Title { get; set; }
        public int PositionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string? Instructions { get; set; }
    }
}
