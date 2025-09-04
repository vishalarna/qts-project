using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Dashboard
{
    public class ClassInfoVM
    {
        public string ILAName { get; set; }

        public string ILANumber { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Instructor { get; set; }

        public string Location { get; set; }

        public string DeliveryMethod { get; set; }

        public string CourseInstruction { get; set; }

        public string WebinarLink { get; set; }

        public bool PreTestRequired { get; set; }

        public bool CBTRequired { get; set; }

        public bool FinalTestRequired { get; set; }

        public bool TaskQualificationRequired { get; set; }

        public bool StudentEvaluationRequired { get; set; }
    }
}
