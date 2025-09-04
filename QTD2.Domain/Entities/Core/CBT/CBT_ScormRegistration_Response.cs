using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class CBT_ScormRegistration_Response : Common.Entity
    {
        public CBT_ScormRegistration_Response()
        {
        }
        public CBT_ScormRegistration_Response(CBT_ScormRegistration cBT_ScormRegistration, CBT_ScormUpload_Question_Choice choice)
        {
            CBT_ScormRegistration = cBT_ScormRegistration;
            CBT_ScormUpload_Question_Choice = choice;
        }

        public int CBTScormRegistrationId { get; set; }
        public int CBTScormUploadQuestionChoiceId { get; set; }

        public virtual CBT_ScormRegistration CBT_ScormRegistration { get; set; }
        public virtual CBT_ScormUpload_Question_Choice CBT_ScormUpload_Question_Choice { get; set; }

        public void UpdateChoice(CBT_ScormUpload_Question_Choice choice)
        {
            CBT_ScormUpload_Question_Choice = choice;
        }
    }
}
