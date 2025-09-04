using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class Ila
    {
        public Ila()
        {
            Cbts = new HashSet<Cbt>();
            ClassSchedules = new HashSet<ClassSchedule>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string OtherAssesmentTool { get; set; }
        public string OtherNercTargetAudience { get; set; }
        public string TrainingPlan { get; set; }
        public int ProviderId { get; set; }
        public bool? IsSelfPaced { get; set; }
        public bool? IsOptional { get; set; }
        public bool? IsPublished { get; set; }
        public DateTime? PublishDate { get; set; }
        public int? DeliveryMethodId { get; set; }
        public bool? HasPilotData { get; set; }
        public bool? IsProgramManual { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? StartDate { get; set; }
        public bool CbtrequiredForCourse { get; set; }
        public bool? Deleted { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string TrainingEvalMethods { get; set; }
        public bool? UseForEmp { get; set; }
        public double? TotalTrainingHours { get; set; }

        public virtual ICollection<Cbt> Cbts { get; set; }
        public virtual ICollection<ClassSchedule> ClassSchedules { get; set; }
    }
}
