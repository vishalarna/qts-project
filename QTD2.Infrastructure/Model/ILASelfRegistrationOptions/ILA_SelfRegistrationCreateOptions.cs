using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILASelfRegistrationOptions
{
    public class ILA_SelfRegistrationCreateOptions
    {
        public int ILAId { get; set; }

        public bool MakeAvailableForSelfReg { get; set; }

        public bool RequireAdminApproval { get; set; }

        public bool AcknowledgeRegDisclaimer { get; set; }

        public string RegDisclaimer { get; set; }

        public bool LimitForLinkedPositions { get; set; }

        public bool CloseRegOnStartDate { get; set; }

        public int? ClassSize { get; set; }

        public bool EnableWaitlist { get; set; }
        public bool SendApprovedEmail { get; set; }

    }
}
