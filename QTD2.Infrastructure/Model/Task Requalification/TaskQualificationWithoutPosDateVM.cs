using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Requalification
{
    public class TaskQualificationWithoutPosDateVM
    {
        public int EmpId { get; set; }

        public string EmpImage { get; set; }

        public string EmpFirstName { get; set; }

        public string EmpLastName { get; set; }

        public string EmpEmail { get; set; }

        public int PosCount { get; set; }
    }
}
