using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class CbtScormRegistration
    {
        public int Id { get; set; }
        public int CbtscormUploadId { get; set; }
        public int ClassScheduleEmployeeId { get; set; }
        public string LaunchLink { get; set; }
        public int RegistrationCompletion { get; set; }
        public int RegistrationSuccess { get; set; }
        public double Score { get; set; }
        public bool Deleted { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public decimal? PassingScore { get; set; }
        public int PassingScoreSource { get; set; }

        public virtual ClassScheduleEmployee ClassScheduleEmployee { get; set; }
        public virtual CbtScormUpload ScormUpload { get; set; }
    }
}
