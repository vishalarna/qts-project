using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblEmployee
    {
        public TblEmployee()
        {
            RsTblClassStudents = new HashSet<RsTblClassStudent>();
            RsTblEmployeesSummaryTasks = new HashSet<RsTblEmployeesSummaryTask>();
            RsTblEmployeesTasks = new HashSet<RsTblEmployeesTask>();
            TblAlerts = new HashSet<TblAlert>();
            TblCertificationHistories = new HashSet<TblCertificationHistory>();
            TblDifsurveyEmployees = new HashSet<TblDifsurveyEmployee>();
            TblEmployeeGroupEmployees = new HashSet<TblEmployeeGroupEmployee>();
            TblGapRatings = new HashSet<TblGapRating>();
            TblGapstatuses = new HashSet<TblGapstatus>();
            TblIdps = new HashSet<TblIdp>();
            TblOjtevaluatorEidNavigations = new HashSet<TblOjtevaluator>();
            TblOjtevaluatorEvalEs = new HashSet<TblOjtevaluator>();
            TblOjthistoryQuestionEidNavigations = new HashSet<TblOjthistoryQuestion>();
            TblOjthistoryQuestionEvalEs = new HashSet<TblOjthistoryQuestion>();
            TblOjthistoryStepEidNavigations = new HashSet<TblOjthistoryStep>();
            TblOjthistoryStepEvalEs = new HashSet<TblOjthistoryStep>();
            TblQtsmgrOrganizations = new HashSet<TblQtsmgrOrganization>();
        }

        public int Eid { get; set; }
        public string ElastName { get; set; }
        public string EfirstName { get; set; }
        public int? Pid { get; set; }
        public string Enum { get; set; }
        public string Estreet1 { get; set; }
        public string Estreet2 { get; set; }
        public string Ecity { get; set; }
        public string Estate { get; set; }
        public string Ezip { get; set; }
        public string Ephone { get; set; }
        public string EworkLoc { get; set; }
        public string NerccertNum { get; set; }
        public int? NerccertArea { get; set; }
        public DateTime? NerccertIssueDate { get; set; }
        public DateTime? NerccertExpDate { get; set; }
        public bool InActive { get; set; }
        public string UserName { get; set; }
        public int? Oid { get; set; }
        public string RegCertType { get; set; }
        public DateTime? RegCertIssueDate { get; set; }
        public DateTime? RegCertExpDate { get; set; }
        public string Remarks { get; set; }
        public string RegCertNum { get; set; }
        public string Note1 { get; set; }
        public string Note2 { get; set; }
        public string EmiddleInitial { get; set; }
        public string RegCertType2 { get; set; }
        public DateTime? RegCertIssueDate2 { get; set; }
        public DateTime? RegCertExpDate2 { get; set; }
        public string RegCertNum2 { get; set; }
        public int? NerccertAreaExisting { get; set; }
        public string Eemail { get; set; }
        public bool? EnotCertified { get; set; }
        public bool? EwillNotBeRecertified { get; set; }
        public DateTime? EhireDate { get; set; }
        public DateTime? EexpirationDate { get; set; }
        public byte[] Ts { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? PosStartDate { get; set; }
        public DateTime? PosEndDate { get; set; }
        public DateTime? PosQualDate { get; set; }
        public int? TrProgId { get; set; }
        public bool? Qtsmanager { get; set; }
        public bool? Trainee { get; set; }
        public bool? QualDateVerified { get; set; }
        public bool? Retired { get; set; }
        public int? SosuserId { get; set; }
        public string EIdptext { get; set; }
        public bool IsTaskEvaluator { get; set; }
        public bool IsQtdadmin { get; set; }

        public virtual LktblOrganization OidNavigation { get; set; }
        public virtual TblClassNotificationEmployee TblClassNotificationEmployee { get; set; }
        public virtual ICollection<RsTblClassStudent> RsTblClassStudents { get; set; }
        public virtual ICollection<RsTblEmployeesSummaryTask> RsTblEmployeesSummaryTasks { get; set; }
        public virtual ICollection<RsTblEmployeesTask> RsTblEmployeesTasks { get; set; }
        public virtual ICollection<TblAlert> TblAlerts { get; set; }
        public virtual ICollection<TblCertificationHistory> TblCertificationHistories { get; set; }
        public virtual ICollection<TblDifsurveyEmployee> TblDifsurveyEmployees { get; set; }
        public virtual ICollection<TblEmployeeGroupEmployee> TblEmployeeGroupEmployees { get; set; }
        public virtual ICollection<TblGapRating> TblGapRatings { get; set; }
        public virtual ICollection<TblGapstatus> TblGapstatuses { get; set; }
        public virtual ICollection<TblIdp> TblIdps { get; set; }
        public virtual ICollection<TblOjtevaluator> TblOjtevaluatorEidNavigations { get; set; }
        public virtual ICollection<TblOjtevaluator> TblOjtevaluatorEvalEs { get; set; }
        public virtual ICollection<TblOjthistoryQuestion> TblOjthistoryQuestionEidNavigations { get; set; }
        public virtual ICollection<TblOjthistoryQuestion> TblOjthistoryQuestionEvalEs { get; set; }
        public virtual ICollection<TblOjthistoryStep> TblOjthistoryStepEidNavigations { get; set; }
        public virtual ICollection<TblOjthistoryStep> TblOjthistoryStepEvalEs { get; set; }
        public virtual ICollection<TblQtsmgrOrganization> TblQtsmgrOrganizations { get; set; }
    }
}
