using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ClassSchedule_SelfRegistrationOptions : Common.Entity
    {
        public int ClassScheduleId { get; set; }

        public bool MakeAvailableForSelfReg { get; set; }

        public bool RequireAdminApproval { get; set; }

        public bool AcknowledgeRegDisclaimer { get; set; }

        public string RegDisclaimer { get; set; }

        public bool LimitForLinkedPositions { get; set; }

        public bool CloseRegOnStartDate { get; set; }

        public bool EnableWaitlist { get; set; }

        public bool SendApprovedEmail { get; set; }

        public virtual ClassSchedule ClassSchedule { get; set; }

        public ClassSchedule_SelfRegistrationOptions(int classScheduleId, bool makeAvailableForSelfReg, bool requireAdminApproval, bool acknowledgeRegDisclaimer, string regDisclaimer, bool limitForLinkedPositions, bool closeRegOnStartDate, bool enableWaitlist, bool sendApprovedEmail)
        {
            ClassScheduleId = classScheduleId;
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
