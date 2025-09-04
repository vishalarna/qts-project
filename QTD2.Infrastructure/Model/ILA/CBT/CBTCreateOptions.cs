using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA.CBTReleaseSetting
{
    public class CBTCreateOptions
    {
        public bool CBTRequiredForCource { get; set; }
        public string CBTLearningContractInstructions { get; set; }
        public CBTAvailablity Availablity { get; set; }
        public int DueDateAmount { get; set; }
        public int EmpSettingsReleaseTypeId { get; set; }
    }
}
