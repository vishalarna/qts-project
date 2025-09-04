using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Dashboard
{
    public class RequiredEMPSettingVM
    {
        public bool PreTestRequired { get; set; }

        public bool CBTRequiredForCourse { get; set; }
        public bool WrittenTest { get; set; }
        public bool TaskQualification { get; set; }
        public bool StudentEvaluation { get; set; }
        public bool OralTest { get; set; }
        public bool SimulatorScenerio { get; set; }
    }
}
