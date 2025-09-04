using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA.CBTReleaseSetting
{
    public class CBTUpdateOptions
    {
        public bool CBTRequiredForCource { get; set; }
        public string CBTLearningContractInstructions { get; set; }
        public int DueDateAmount { get; set; }
        public Domain.Entities.Core.CBTAvailablity Availablity { get; set; }
        public bool ChangeDueDate { get; set; }
        public int EmpSettingsReleaseTypeId { get; set; }
    }
}
