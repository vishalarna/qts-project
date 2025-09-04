using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILASelfRegistrationOptions
{
    public class ILA_SelfRegistrationOptions_ViewModel
    {
        public bool? MakeAvailableForSelfReg { get; set; }
        public bool? RequireAdminApproval { get; set; }
        public bool? SendApprovedEmail { get; set; }
        public bool? AcknowledgeRegDisclaimer { get; set; }
        public string RegDisclaimer { get; set; }
        public bool? LimitForLinkedPositions { get; set; }
        public bool? CloseRegOnStartDate { get; set; }
        public bool? EnableWaitlist { get; set; }
        public int? ClassSize { get; set; }

        public ILA_SelfRegistrationOptions_ViewModel()
        {
        }

        public ILA_SelfRegistrationOptions_ViewModel(bool? makeAvailableForSelfReg,bool? requireAdminApproval,bool? sendApprovedEmail,bool? acknowledgeRegDisclaimer,string regDisclaimer,bool? limitForLinkedPositions,bool? closeRegOnStartDate,bool? enableWaitlist,int? classSize)
        {
            MakeAvailableForSelfReg = makeAvailableForSelfReg;
            RequireAdminApproval = requireAdminApproval;
            SendApprovedEmail = sendApprovedEmail;
            AcknowledgeRegDisclaimer = acknowledgeRegDisclaimer;
            RegDisclaimer = regDisclaimer;
            LimitForLinkedPositions = limitForLinkedPositions;
            CloseRegOnStartDate = closeRegOnStartDate;
            EnableWaitlist = enableWaitlist;
            ClassSize = classSize;
        }
    }

}
