using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QTD2.Domain.Entities.Core
{
    public class CBT : Common.Entity
    {
        public int ILAId { get; set; }

        public List<CBT_ScormUpload> ScormUploads { get; set; } = new List<CBT_ScormUpload>();

        public CBTAvailablity Availablity { get; set; }

        public string CBTLearningContractInstructions { get; set; }

        public int DueDateAmount { get; set; }

        public int? EmpSettingsReleaseTypeId { get; set; }

        public virtual ILA ILA { get; set; }
        public virtual EmpSettingsReleaseType EmpSettingsReleaseType { get; set; }

        public CBT(int iLAId, string cBTLearningContractInstructions, int dueDateAmount, int empSettingsReleaseTypeId, CBTAvailablity availablity)
        {
            ILAId = iLAId;
            CBTLearningContractInstructions = cBTLearningContractInstructions;
            DueDateAmount = dueDateAmount;
            EmpSettingsReleaseTypeId = empSettingsReleaseTypeId;
            Availablity = availablity;
        }

        public CBT()
        {
        }

        public void AttachScormUpload(CBT_ScormUpload upload)
        {
            if (ScormUploads == null) ScormUploads = new List<CBT_ScormUpload>();
           
            ScormUploads.Add(upload);
            upload.Connect();
        }

        public void SetDueDate(int dueDateAmount, int empSettingsReleaseTypeId)
        {
            DueDateAmount = dueDateAmount;
            EmpSettingsReleaseTypeId = empSettingsReleaseTypeId;
        }

        public override void Create(string username)
        {
            base.Create(username);
            AddDomainEvent(new Domain.Events.Core.OnCbtCreated(this));
        }
        public void SetAvailability(CBTAvailablity availablity)
        {
            Availablity = availablity;
        }

        public DateTime GetDueDate(DateTime dueDate)
        {
            if (EmpSettingsReleaseTypeId == 3)
            {
                return dueDate.AddMonths(DueDateAmount);
            } 
            else if (EmpSettingsReleaseTypeId == 2)
            {
                return dueDate.AddDays(DueDateAmount * 7);
            }
            else if(EmpSettingsReleaseTypeId == 1)
            {
                return dueDate.AddDays(DueDateAmount);
            }
            return DateTime.MaxValue;
        }

        public void setCBTLearningContractInstructions(string cBTLearningContractInstructions)
        {
            CBTLearningContractInstructions = cBTLearningContractInstructions;
        }

        public void DisconnectAllScormUploads()
        {
            if (ScormUploads == null) return;

            foreach (var scorm in ScormUploads)
            {
                scorm.Disconnect();
            }
        }
    }
}
