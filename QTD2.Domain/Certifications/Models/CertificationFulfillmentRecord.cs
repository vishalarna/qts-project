using System;
using System.Collections.Generic;
using System.Linq;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Certifications.Models
{
    public class CertificationFulfillmentRecord
    {
        public int ClassScheduleEmployeeId { get; set; }
        public int ILAId { get; set; }
        public bool ILAActive { get; set; }
        public string ILANumber { get; set; }
        public string ILAName { get; set; }
        public double? ILATotalTrainingHours { get; set; }
        public bool ILAProviderIsNERC { get; set; }
        public string ILAProviderNumber { get; set; }
        public double TotalTrainingHours { get { return ILATotalTrainingHours ?? 0; } }
        public double TotalTrainingHoursAwarded { get { return new List<string>() { "F", "O" }.Contains(Grade) ? 0 : TotalTrainingHours; } }
        public DateTime? ClassCompletionDate { get; set; }
        public DateTime ClassScheduledDate { get; set; }
        public DateTime CreditAwardedDate { get; set; }
        public bool IsComplete { get; set; }
        public string Grade { get; set; }
        public int? Score { get; set; }
        public string GradeNotes { get; set; }
        public double? CEHHours { get; set; }
        public double? PartialCreditHour { get; set; }
        public double? ActualCEHHours { get { return PartialCreditHour.HasValue ? PartialCreditHour : CEHHours.HasValue ? CEHHours : 0; } }
        public double CEHAwarded { get { return AwardsCehs && IsComplete ? (ActualCEHHours ?? 0) : 0; } }
        public double CEHScheduled { get { return AwardsCehs ? (ActualCEHHours ?? 0) : 0; } }
        public double CEHPending { get { return AwardsCehs && !IsComplete ? (ActualCEHHours ?? 0) : 0; } }
        public bool AwardsCehs { get; set; }
        public double PendingCEHAwarded { get { return PendingAwardsCehs && !IsComplete ? (ActualCEHHours ?? 0) : 0; } }
        public bool PendingAwardsCehs { get; set; }
        public bool IsIncludeSimulation { get; set; }
        public bool IsEmergencyOpHours { get; set; }
        public virtual List<CertificationFulfillmentSubRequirement> CertificationFulfillmentSubRequirements { get; set; } = new List<CertificationFulfillmentSubRequirement>();

        public CertificationFulfillmentRecord(ClassSchedule_Employee classScheduleEmployee, ILACertificationLink ilaCertificationLink, bool awardsCehs, bool pendingAwardsCehs, ClassScheduleEmployee_ILACertificationLink_PartialCredit partialCredit = null)
        {
            ClassScheduleEmployeeId = classScheduleEmployee.Id;
            ILAId = classScheduleEmployee.ClassSchedule.ILA.Id;
            ILAActive = classScheduleEmployee.ClassSchedule.ILA.Active;
            ILAName = classScheduleEmployee.ClassSchedule.ILA.Name;
            ILANumber = classScheduleEmployee.ClassSchedule.ILA.Number;
            ILAProviderIsNERC = classScheduleEmployee.ClassSchedule.ILA.Provider.IsNERC;
            ILAProviderNumber = classScheduleEmployee.ClassSchedule.ILA.Provider.Number;
            ILATotalTrainingHours = classScheduleEmployee.ClassSchedule.ILA.TotalTrainingHours;
            ClassCompletionDate = classScheduleEmployee.CompletionDate;
            ClassScheduledDate = classScheduleEmployee.ClassSchedule.EndDateTime;
            CreditAwardedDate = classScheduleEmployee.IsComplete ? classScheduleEmployee.CompletionDate.Value : classScheduleEmployee.ClassSchedule.EndDateTime;
            IsComplete = classScheduleEmployee.IsComplete;
            Grade = classScheduleEmployee.FinalGrade;
            CEHHours = ilaCertificationLink.CEHHours;
            IsIncludeSimulation = ilaCertificationLink.IsIncludeSimulation;
            IsEmergencyOpHours = ilaCertificationLink.IsEmergencyOpHours;
            AwardsCehs = awardsCehs;
            PendingAwardsCehs = pendingAwardsCehs;
            Score = classScheduleEmployee?.FinalScore;
            GradeNotes = classScheduleEmployee.GradeNotes;
            PartialCreditHour = partialCredit?.PartialCreditHours;


            foreach (var ilaCertificationSubRequirementLink in ilaCertificationLink.ILACertificationSubRequirementLink)
            {
                var partialCreditSubRequirement = partialCredit?
                    .ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits
                    .FirstOrDefault(cseSubReqPartialCredit => cseSubReqPartialCredit.ILACertificationSubRequirementLinkId == ilaCertificationSubRequirementLink.Id);

                var ActualReqHours = (partialCreditSubRequirement != null && partialCreditSubRequirement.PartialCreditHours.HasValue) ? 
                    partialCreditSubRequirement.PartialCreditHours.Value : 
                    ilaCertificationSubRequirementLink.ReqHour;

                var certificationFulfillmentSubRequirement = new CertificationFulfillmentSubRequirement();
                certificationFulfillmentSubRequirement.CertificationSubRequirementId = ilaCertificationSubRequirementLink.CertificationSubRequirementId;
                certificationFulfillmentSubRequirement.CertificationSubRequirementName = ilaCertificationSubRequirementLink.CertificationSubRequirement.ReqName;
                certificationFulfillmentSubRequirement.Hours = ActualReqHours;
                certificationFulfillmentSubRequirement.AwardedHours = AwardsCehs ? ActualReqHours : 0;
                certificationFulfillmentSubRequirement.PendingHours = PendingAwardsCehs ? ActualReqHours : 0;

                CertificationFulfillmentSubRequirements.Add(certificationFulfillmentSubRequirement);
            }
        }
    }
}
