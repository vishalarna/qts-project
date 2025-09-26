using QTD2.Data;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.QTD
{
    public interface IJobNotificationService
    {
        Task<bool> SendClassScheduleNotification(int employeeId, int order, QTDContext context);
        Task<bool> SendCertificationExpirationNotification(int employeeCertificationId, int order, QTDContext context);
        Task<bool> SendEmpLoginNotification(int employeeId, int order, string url, QTDContext context);
        Task<bool> SendEmpTestNotification(int classScheduleRosterId, int order, QTDContext context);
        Task<bool> SendEmpPretestNotification(int classScheduleRosterId, int order, QTDContext context);
        Task<bool> SendEmpCourseNotification(int employeeId, int courseId, int order, QTDContext context);
        Task<bool> SendEmpStudentEvaluationNotification(int classSchedule_StudentEvaluation_LinkId, int order, QTDContext context);
        Task<bool> SendEmpProcedureReviewNotification(int procedureReviewEmployeeId, int order, QTDContext context);
        Task<bool> SendEmpIDPReviewNotification(int employeeId, int idpId, int order, QTDContext context);
        Task<bool> SendEmpTaskQualitificationTraineeNotification(int taskQualificationId, int order, QTDContext context);
        Task<bool> SendEmpTaskQualitificationEvaluatorNotification(int taskQualification_Evaluator_LinkId, int order, QTDContext context);
        Task<bool> SendEmpSelfRegistrationApprovalNotification(int classScheduleEmployeeId, int order, QTDContext context);
        Task<bool> SendEmpSelfRegistrationDenialNotification(int classScheduleEmployeeId, int order, QTDContext context);
        Task<bool> SendEmpDifSurveyNotification(int employeeId, int surveyId, int order, QTDContext context);
        Task<bool> SendEmpGapSurveyNotification(int employeeId, int surveyId, int order, QTDContext context);
        Task<bool> SendMetaILASelfPacedReleasedNotification(int employeeId, int nextMeta_ILAMembers_LinkId, int? metaILA_Employee_MemberLinkFufillmentId, int order, QTDContext context);
        Task<bool> SendMetaILA_Employee_SelfRegistrationRequiredNotification(int employeeId, int nextMeta_ILAMembers_LinkId, int? metaILA_Employee_MemberLinkFufillmentId, int order, QTDContext context);
        Task<bool> SendMetaILA_Admin_SelfRegistrationRequiredNotification(int? toPersonId, int employeeId, int nextMeta_ILAMembers_LinkId, int? metaILA_Employee_MemberLinkFufillmentId, int order, QTDContext context);
        Task<bool> SendMetaILA_Employee_RegistrationRequiredNotification(int employeeId, int nextMeta_ILAMembers_LinkId, int? metaILA_Employee_MemberLinkFufillmentId, int order, QTDContext context);
        Task<bool> SendMetaILA_Admin_RegistrationRequiredNotification(int? toPersonId, int employeeId, int nextMeta_ILAMembers_LinkId, int? metaILA_Employee_MemberLinkFufillmentId, int order, QTDContext context);
        Task<bool> SendMetaILA_CourseworkCompletedNotification(int employeeId, int metaILA_Employee_MemberLinkFufillmentId, int? classScheduleRosterId, int? classSchedule_Evaluation_RosterId, int order, QTDContext context);
        Task<bool> SendEMPOnlineCourseNotification(int classScheduleEmployeeId, int cBTId, int order, QTDContext context);
        Task<bool> SendEmpPortalCompletionNotification(AdminEMPCompletionNotification notification, int order, QTDContext context);
        Task<bool> SendSimulatorScenarioCollaborationNotification(SimulatorScenarioCollaborationNotification notification, int order, string url, QTDContext context);

        public Task<bool> SendPublicClassScheduleRequestNotification(int id, PublicClassScheduleRequestNotification publicClassScheduleRequestNotification, int order, int? toPersonId, QTDContext qtdContext);
        public Task<bool> SendPublicClassScheduleRequestAcceptedNotification(int publicClassScheduleRequestId, int order, int? toPersonId, QTDContext qtdContext,  string url);
        public Task<bool> SendEmpSkillQualitificationTraineeNotification(int skillQualificationId, int order, QTDContext context);
        public Task<bool> SendEmpSkillQualitificationEvaluatorNotification(int skillQualification_Evaluator_LinkId, int order, QTDContext context);
    }
}
