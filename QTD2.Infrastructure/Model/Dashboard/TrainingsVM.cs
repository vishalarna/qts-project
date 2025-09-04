using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Dashboard
{
    public class TrainingsVM
    {
        public int ilaId { get; set; }
        public string IlaTitle { get; set; }
        public string type { get; set; }
        public string Location { get; set; }
        public bool IsCollapsable { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public int ParentId { get; set; }
        public List<TrainingItems> Trainings { get; set; }
        
    }
    public class TrainingItems
    {
        public int Id { get; set; }
        public string type { get; set; }
        public string Status { get; set; }
        public string TestType { get; set; }
        
        public int ParentId { get; set; }
        public DateTime DueDate { get; set; }

        public string Title { get; set; }
       

    }
}
