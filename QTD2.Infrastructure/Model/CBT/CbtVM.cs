using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.CBT
{
    public class CbtVM
    {
        public int ILAId { get; set; }

        public List<CBT_ScormUploadVM> ScormUploads { get; set; } = new List<CBT_ScormUploadVM>();

        public CBTAvailablity Availablity { get; set; }

        public string CBTLearningContractInstructions { get; set; }

        public int DueDateAmount { get; set; }

        public int? EmpSettingsReleaseTypeId { get; set; }

        public void Load(Domain.Entities.Core.CBT cbt)
        {
            this.ILAId = cbt.ILAId;
            this.Availablity = cbt.Availablity;
            this.CBTLearningContractInstructions = cbt.CBTLearningContractInstructions;
            this.DueDateAmount = cbt.DueDateAmount;
            this.EmpSettingsReleaseTypeId = cbt.EmpSettingsReleaseTypeId;

            loadScormUploads(cbt);
        }

        private void loadScormUploads(Domain.Entities.Core.CBT cbt)
        {
            ScormUploads = new List<CBT_ScormUploadVM>();

            foreach(var scormUpload in cbt.ScormUploads)
            {
                ScormUploads.Add(new CBT_ScormUploadVM(scormUpload.Id, scormUpload.CbtId, cbt.ILAId, null, scormUpload.Name, scormUpload.Active));
            }
        }
    }
}
