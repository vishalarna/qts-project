
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ClientUser;
using System.Collections.Generic;

namespace QTD2.Infrastructure.Notification.Interfaces
{
    public interface INotificationFactory
    {
        INotification Create2FANotification(string destination, string token);
        INotification CreateForgotPassword(string destination, string url);
        INotification CreateVerifyEmail(string destination, string token);
        INotification CreateVerifyPhone(string destination, string token);
        INotification CreateClassScheduleNotification(List<string> destination, int order, ClientSettings_Notification setting, ClassSchedule_Employee classSchedule_Employee);
        INotification CreateCertificationExpirationNotification(List<string> destination, int order, ClientSettings_Notification setting, EmployeeCertification employeeCertification, string defaultTimeZone);
        INotification CreateEmpLoginNotification(List<string> destination, int order, ClientSettings_Notification setting, Employee employee, string url);
        INotification CreateEmpTestNotification(List<string> destination, int order, ClientSettings_Notification setting, ClassSchedule_Roster classSchedule_roster, string defaultTimeZone);
        INotification CreateEmpPretestNotification(List<string> destination, int order, ClientSettings_Notification setting, ClassSchedule_Roster classSchedule_roster, string defaultTimeZone);
        INotification CreateEmpCourseNotification(List<string> destination, int order, ClientSettings_Notification setting, Employee employee, CBT course, string defaultTimeZone);
        INotification CreateEmpStudentEvaluationNotification(List<string> destination, int order, ClientSettings_Notification setting, ClassSchedule_Evaluation_Roster classSchedule_Evaluation_Roster, string defaultTimeZone);
        INotification CreateEmpProcedureReviewNotification(List<string> destination, int order, ClientSettings_Notification setting, ProcedureReview_Employee procedureReview_Employee, string defaultTimeZone);
        INotification CreateIDPReviewNotification(List<string> destination, int order, ClientSettings_Notification setting, IDP_Review idpReview, string defaultTimeZone);
        INotification CreateTaskQualitificationTraineeNotification(List<string> destination, int order, ClientSettings_Notification setting, TaskQualification taskQualification);
        INotification CreateTaskQualitificationEvaluatorNotification(List<string> destination, int order, ClientSettings_Notification setting, TaskQualification_Evaluator_Link link);
        INotification CreateSelfRegistrationApprovalNotification(List<string> destination, int order, ClientSettings_Notification setting, ClassSchedule_Employee classSchedule_Employee, string defaultTimeZone);
        INotification CreateSelfRegistrationDenialNotification(List<string> destination, int order, ClientSettings_Notification setting, ClassSchedule_Employee classSchedule_Employee, string defaultTimeZone);
        INotification CreateDifSurveyNotification(List<string> destination, int order, ClientSettings_Notification setting, Employee employee, object survey, string defaultTimeZone);
        INotification CreateGapSurveyNotification(List<string> destination, int order, ClientSettings_Notification setting, Employee employee, object survey, string defaultTimeZone);
        INotification CreateSendReportNotification(Report report, string file, List<string> tos);
        INotification CreateMetaILASelfPacedReleasedNotification(List<string> destination, int order, ClientSettings_Notification setting, Employee employee, Meta_ILAMembers_Link nextIla, MetaILA_Employee_MemberLinkFufillment fufilment);
        INotification CreateMetaILA_Employee_SelfRegistrationRequiredNotification(List<string> destination, int order, ClientSettings_Notification setting, Employee employee, Meta_ILAMembers_Link nextIla, MetaILA_Employee_MemberLinkFufillment fufilment, List<ClassSchedule> availableClassSchedules);
        INotification CreateMetaILA_Admin_SelfRegistrationRequiredNotification(List<string> destination, int order, ClientSettings_Notification setting, Employee employee, Meta_ILAMembers_Link nextIla, MetaILA_Employee_MemberLinkFufillment fufilment, List<ClassSchedule> availableClassSchedules);
        INotification CreateMetaILA_Employee_RegistrationRequiredNotification(List<string> destination, int order, ClientSettings_Notification setting, Employee employee, Meta_ILAMembers_Link nextIla, MetaILA_Employee_MemberLinkFufillment fufilment);
        INotification CreateMetaILA_CourseworkCompletedNotification(List<string> destination, int order, ClientSettings_Notification setting, Employee employee, MetaILA_Employee_MemberLinkFufillment fufilment, ClassSchedule_Roster classScheduleRoster, ClassSchedule_Evaluation_Roster classScheduleEvaluationRoster);
        INotification CreateMetaILA_Admin_RegistrationRequiredNotification(List<string> destination, int order, ClientSettings_Notification setting, Employee employee, Meta_ILAMembers_Link nextIla, MetaILA_Employee_MemberLinkFufillment fufilment);
        INotification CreateEMPOnlineCourseNotification(List<string> destination, int order, ClientSettings_Notification setting, ClassSchedule_Employee employee, CBT cbt, string defaultTimeZone);
        INotification CreateEmployeePortalCompletionNotification(List<string> destination, int order, ClientSettings_Notification setting, AdminEMPCompletionNotification employee_Completion, string defaultTimeZone);
        INotification CreateSimulatorScenarioCollaborationNotification(List<string> destination, int order, ClientSettings_Notification setting, SimulatorScenario_Collaborator simulatorScenarioCollaborator, string url);
        INotification CreateAccountLockedNotification(string destination, string url);

        INotification CreatePublicClassScheduleRequestNotification(List<string> destination, int order, ClientSettings_Notification setting, PublicClassScheduleRequest publicClassScheduleRequest, string defaultTimeZone);

        INotification CreatePublicClassScheduleRequestDeclineNotification(ClientSettings_Notification setting, PublicClassScheduleRequest publicClassScheduleRequest, string defaultTimeZone);

        INotification CreatePublicClassScheduleRequestAcceptedNotification(List<string> destination, int order, ClientSettings_Notification setting, PublicClassScheduleRequest publicClassScheduleRequest, string defaultTimeZone, string url, string courseTitle);
    }
}
