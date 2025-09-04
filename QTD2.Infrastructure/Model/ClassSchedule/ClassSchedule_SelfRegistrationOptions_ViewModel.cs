using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule
{
    public class ClassSchedule_SelfRegistrationOptions_ViewModel
    {
        public bool MakeAvailableForSelfReg { get; set; }

        public bool RequireAdminApproval { get; set; }

        public bool AcknowledgeRegDisclaimer { get; set; }

        public string RegDisclaimer { get; set; }

        public bool LimitForLinkedPositions { get; set; }

        public bool CloseRegOnStartDate { get; set; }


        public bool EnableWaitlist { get; set; }

        public bool SendApprovedEmail { get; set; }

        public ClassSchedule_SelfRegistrationOptions_ViewModel(bool makeAvailableForSelfReg, bool requireAdminApproval, bool acknowledgeRegDisclaimer, string regDisclaimer,
                bool limitForLinkedPositions, bool closeRegOnStartDate, bool enableWaitlist, bool sendApprovedEmail)
        {
            MakeAvailableForSelfReg = makeAvailableForSelfReg;
            RequireAdminApproval = requireAdminApproval;
            AcknowledgeRegDisclaimer = acknowledgeRegDisclaimer;
            RegDisclaimer = regDisclaimer;
            LimitForLinkedPositions = limitForLinkedPositions;
            CloseRegOnStartDate = closeRegOnStartDate;
            EnableWaitlist = enableWaitlist;
            SendApprovedEmail = sendApprovedEmail;
        }
    }
}
