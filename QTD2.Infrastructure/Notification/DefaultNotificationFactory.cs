using System;
using System.Linq;
using System.Collections.Generic;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Notification.Content.Models;
using QTD2.Infrastructure.Notification.Interfaces;
using Microsoft.AspNetCore.Http;
using QTD2.Infrastructure.Model.ClientUser;
using QTD2.Infrastructure.ExtensionMethods;

namespace QTD2.Infrastructure.Notification
{
    public class DefaultNotificationFactory : INotificationFactory
    {
        private readonly Content.IContentGeneratorFactory _contentGeneratorFactory;
        private Content.IContentGenerator _contentGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public DefaultNotificationFactory(Content.IContentGeneratorFactory contentGeneratorFactory, IHttpContextAccessor httpContextAccessor)
        {
            _contentGeneratorFactory = contentGeneratorFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public INotification Create2FANotification(string destination, string token)
        {
            NotificationMethod method = getNotificationMethod();
            _contentGenerator = getContentGenerator(method);

            string content = _contentGenerator.GetContent("Notification/Content/Views/TwoFactorToken.cshtml", new TwoFactorEmailModel(destination, token));

            return new Notifications.EmailNotification(content, "Your Verification Code", method, new List<string>() { destination });
        }

        public INotification CreateCertificationExpirationNotification(List<string> destination, int order, ClientSettings_Notification setting, EmployeeCertification employeeCertification, string defaultTimeZone )
        {
            if (setting.Name == "Certification Expiration")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                CertificationExpirationModel certificationExpirationModel = new CertificationExpirationModel(employeeCertification.Employee.Person.FirstName, employeeCertification.Employee.Person.LastName, employeeCertification.Certification.Name, employeeCertification.CertificationNumber, (employeeCertification.ExpirationDate.Value.DayNumber - DateOnly.FromDateTime(DateTime.Now).DayNumber), (employeeCertification.ExpirationDate ?? DateOnly.FromDateTime(DateTime.Now)).ToDateTime(TimeOnly.MinValue), defaultTimeZone);

                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, certificationExpirationModel);

                return new Notifications.EmailNotification(content, "Certification Expiration Notification", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateClassScheduleNotification(List<string> destination, int order, ClientSettings_Notification setting, ClassSchedule_Employee classSchedule_Employee)
        {
            if (setting.Name == "Class Schedule")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                ClassScheduleModel classScheduleModel = new ClassScheduleModel(classSchedule_Employee.Employee.Person.FirstName, classSchedule_Employee.Employee.Person.LastName);

                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, classScheduleModel);

                return new Notifications.EmailNotification(content, "Class Schedule Notificaiotn", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateDifSurveyNotification(List<string> destination, int order, ClientSettings_Notification setting, Employee employee, object survey, string defaultTimeZone)
        {
            if (setting.Name == "EMP DIF Survey")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                EmpDifSurveyModel empDifSurveyModel = new EmpDifSurveyModel(employee.Person.FirstName, employee.Person.LastName, "PositionTitle- NO FIELD YET", "SurveyTitle-No Field Yet", DateTime.Now /*SurveyStartDate*/, DateTime.Now /*"SurveyEndDate"*/, defaultTimeZone);

                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, empDifSurveyModel);

                return new Notifications.EmailNotification(content, "Your Verification Token", method, destination);
            }
            else
            {
                throw new ArgumentException("EMP DIF Survey");
            }
        }

        public INotification CreateEmpCourseNotification(List<string> destination, int order, ClientSettings_Notification setting, Employee employee, CBT cbt, string defaultTimeZone)
        {
            if (setting.Name == "EMP Online Course")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                EmpCourseModel empCourseModel = new EmpCourseModel(employee.Person.FirstName, employee.Person.LastName, cbt.ILA.Name, "Instructor-No FIELD YET", "Location-No FIELD YET", DateTime.Now /*"courseEndDate"*/, DateTime.Now /*"EvalDuedate"*/, DateTime.Now /*"courseEndDate"*/, DateTime.Now /*"EvalDuedate"*/, defaultTimeZone);

                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, empCourseModel);

                return new Notifications.EmailNotification(content, "EMP Online Course Notification", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateEmpLoginNotification(List<string> destination, int order, ClientSettings_Notification setting, Employee employee, string url)
        {
            if (setting.Name == "EMP Login")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);
                //string anchorTag = "<a href=\""+url+"\">Login Page</a>";
                EMPLoginModel empLoginModel = new EMPLoginModel(employee.Person.FirstName, employee.Person.LastName, url, employee.Person.Username);

                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                //string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, empLoginModel);
                string content = _contentGenerator.GetContent("Notification/Content/Views/EmpLoginNotification.cshtml", empLoginModel);
                return new Notifications.EmailNotification(content, "EMP Login Notification", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateEmpPretestNotification(List<string> destination, int order, ClientSettings_Notification setting, ClassSchedule_Roster classScheduleRoster, string defaultTimeZone)
        {
            if (setting.Name == "EMP Pretest")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                EmpPresetModel empPresetModel = new EmpPresetModel(
                    classScheduleRoster.Employee.Person.FirstName,
                    classScheduleRoster.Employee.Person.LastName,
                    classScheduleRoster.ClassSchedule.ILA.Name,
                    classScheduleRoster.ClassSchedule.Instructor?.InstructorName,
                    classScheduleRoster.ClassSchedule.Location?.LocName,
                    classScheduleRoster.ClassSchedule.StartDateTime,
                    classScheduleRoster.ClassSchedule.EndDateTime,
                    classScheduleRoster.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.PreTest.TestTitle,
                    classScheduleRoster.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.PreTest.Id,
                    classScheduleRoster.ReleaseDate ?? classScheduleRoster.ClassSchedule.StartDateTime,
                    defaultTimeZone
                    );

                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, empPresetModel);

                return new Notifications.EmailNotification(content, "EMP Pretest", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateEmpProcedureReviewNotification(List<string> destination, int order, ClientSettings_Notification setting, ProcedureReview_Employee procedureReview_Employee, string defaultTimeZone)
        {
            if (setting.Name == "EMP Procedure Review")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                EmpProcedureReviewModel empProcedureReviewModel = new EmpProcedureReviewModel(procedureReview_Employee.Employee.Person.FirstName, procedureReview_Employee.Employee.Person.LastName, procedureReview_Employee.ProcedureReview.Procedure.Number, procedureReview_Employee.ProcedureReview.ProcedureReviewTitle, procedureReview_Employee.ProcedureReview.StartDateTime, procedureReview_Employee.ProcedureReview.EndDateTime, defaultTimeZone);

                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, empProcedureReviewModel);

                return new Notifications.EmailNotification(content, "Procedure Review Notification", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateEmpStudentEvaluationNotification(List<string> destination, int order, ClientSettings_Notification setting, ClassSchedule_Evaluation_Roster classSchedule_Evaluation_Roster, string defaultTimeZone)
        {
            if (setting.Name == "EMP Student Evaluation")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                EmpStudentEvaluationModel empStudentEvaluationModel = new EmpStudentEvaluationModel(
                    classSchedule_Evaluation_Roster.Employee.Person.FirstName,
                    classSchedule_Evaluation_Roster.Employee.Person.LastName,
                    classSchedule_Evaluation_Roster.ClassScheduleInfo.ILA.Name,
                    classSchedule_Evaluation_Roster.ClassScheduleInfo.Instructor?.InstructorName,
                    classSchedule_Evaluation_Roster.ClassScheduleInfo.Location?.LocName,
                    classSchedule_Evaluation_Roster.ReleaseDate ?? classSchedule_Evaluation_Roster.ClassScheduleInfo.EndDateTime,
                    classSchedule_Evaluation_Roster.ClassScheduleInfo.EndDateTime,
                    defaultTimeZone,
                    classSchedule_Evaluation_Roster.ClassScheduleInfo.StartDateTime,
                    classSchedule_Evaluation_Roster.ClassScheduleInfo.EndDateTime);

                var step = setting.Steps.Where(r => r.Order == order).First(); 
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, empStudentEvaluationModel);

                return new Notifications.EmailNotification(content, "EMP Student Evaluation Notification", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateEmpTestNotification(List<string> destination, int order, ClientSettings_Notification setting, ClassSchedule_Roster classScheduleRoster, string defaultTimeZone)
        {

            if (setting.Name == "EMP Test")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                EmpTestModel empTestModel = new EmpTestModel(
                    classScheduleRoster.Employee.Person.FirstName,
                    classScheduleRoster.Employee.Person.LastName,
                    classScheduleRoster.ClassSchedule.Location?.LocName,
                    classScheduleRoster.ClassSchedule.Instructor?.InstructorName,
                    classScheduleRoster.ClassSchedule.ILA.Name,
                    classScheduleRoster.ClassSchedule.StartDateTime,
                    classScheduleRoster.ClassSchedule.EndDateTime,
                    classScheduleRoster.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.FinalTest.TestTitle,
                    classScheduleRoster.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.FinalTest.Id,
                    classScheduleRoster.ClassSchedule.EndDateTime,
                    classScheduleRoster.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.GetDueDate(classScheduleRoster.ClassSchedule.EndDateTime),
                    defaultTimeZone
                    );

                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, empTestModel);

                return new Notifications.EmailNotification(content, "EMP Test Notification", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateForgotPassword(string destination, string url)
        {
            NotificationMethod method = getNotificationMethod();
            _contentGenerator = getContentGenerator(method);

            var model = new ForgotPasswordModel(destination, url);

            string content = _contentGenerator.GetContent("Notification/Content/Views/ForgotPassword.cshtml", model);
            //content = _contentGenerator.GetContent("ForgotPassword", content, model);

            return new Notifications.EmailNotification(content, "Reset your Password", method, new List<string>() { destination });
        }

        public INotification CreateGapSurveyNotification(List<string> destination, int order, ClientSettings_Notification setting, Employee employee, object survey, string defaultTimeZone)
        {
            if (setting.Name == "EMP GAP Survey")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                EmpGapSurveyModel empGapSurveyModel = new EmpGapSurveyModel(employee.Person.FirstName, employee.Person.LastName, "PositionTitle- NO FIELD YET", "SurveyTitle-No Field Yet", DateTime.Now /*SurveyStartDate*/, DateTime.Now /*"SurveyEndDate"*/, defaultTimeZone);

                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, empGapSurveyModel);

                return new Notifications.EmailNotification(content, "Your Verification Token", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateIDPReviewNotification(List<string> destination, int order, ClientSettings_Notification setting, IDP_Review idpReview, string defaultTimeZone)
        {
            if (setting.Name == "EMP IDP Review")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                EmpIdpReviewModel empIdpReviewModel = new EmpIdpReviewModel(idpReview.Employee.Person.FirstName, idpReview.Employee.Person.LastName, idpReview.Title, idpReview.ReleaseDate, idpReview.EndDate, defaultTimeZone);

                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, empIdpReviewModel);

                return new Notifications.EmailNotification(content, "EMP IDP Review Notification", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateSelfRegistrationApprovalNotification(List<string> destination, int order, ClientSettings_Notification setting, ClassSchedule_Employee classSchedule_Employee, string defaultTimeZone)
        {
            if (setting.Name == "EMP Self-Registration Approval")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                EmpSelf_RegistrationApprovalModel empSelf_RegistrationApprovalModel = new EmpSelf_RegistrationApprovalModel(classSchedule_Employee.Employee.Person.FirstName, classSchedule_Employee.Employee.Person.LastName, classSchedule_Employee.ClassSchedule.ILA.Name, classSchedule_Employee.ClassSchedule.ILA.Number, classSchedule_Employee.ClassSchedule.StartDateTime, classSchedule_Employee.ClassSchedule.EndDateTime, defaultTimeZone);

                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, empSelf_RegistrationApprovalModel);

                return new Notifications.EmailNotification(content, "EMP Self-Registration Approval Notification", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateSelfRegistrationDenialNotification(List<string> destination, int order, ClientSettings_Notification setting, ClassSchedule_Employee classSchedule_Employee, string defaultTimeZone)
        {
            if (setting.Name == "EMP Self-Registration Denial")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                EmpSelf_RegistrationDenialModel empSelf_RegistrationDenialModel = new EmpSelf_RegistrationDenialModel(classSchedule_Employee.Employee.Person.FirstName, classSchedule_Employee.Employee.Person.LastName, classSchedule_Employee.ClassSchedule.ILA.Name, classSchedule_Employee.ClassSchedule.ILA.Number, classSchedule_Employee.ClassSchedule.StartDateTime, classSchedule_Employee.ClassSchedule.EndDateTime, defaultTimeZone);

                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, empSelf_RegistrationDenialModel);

                return new Notifications.EmailNotification(content, "EMP Self-Registration Denial Notification", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateTaskQualitificationEvaluatorNotification(List<string> destination, int order, ClientSettings_Notification setting, TaskQualification_Evaluator_Link link)
        {
            if (setting.Name == "EMP Task And Skill Qualification - Evaluator")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                EmpTaskQualitification_EvaluatorModel empTaskQualitification_EvaluatorModel = new EmpTaskQualitification_EvaluatorModel(link.Evaluator.Person.FirstName, link.Evaluator.Person.LastName, link.TaskQualification.Task.FullNumber, link.TaskQualification.Task.Description, link.TaskQualification.Employee.Person.FirstName + " " + link.TaskQualification.Employee.Person.LastName);

                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, empTaskQualitification_EvaluatorModel);

                return new Notifications.EmailNotification(content, "EMP Task Qualification - Evaluator Notification", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateTaskQualitificationTraineeNotification(List<string> destination, int order, ClientSettings_Notification setting, TaskQualification taskQualification)
        {
            if (setting.Name == "EMP Task And Skill Qualification - Trainee")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                EmpTaskQualitification_TraineeModel empTaskQualitification_TraineeModel = new EmpTaskQualitification_TraineeModel(taskQualification.Employee.Person.FirstName, taskQualification.Employee.Person.LastName, taskQualification.Task.FullNumber, taskQualification.Task.Description, taskQualification.TaskQualification_Evaluator_Links.Count() > 0 ? String.Join(Environment.NewLine, taskQualification.TaskQualification_Evaluator_Links.Select(r => r.Evaluator.Person.FirstName + " " + r.Evaluator.Person.LastName).ToList()) : "");

                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, empTaskQualitification_TraineeModel);

                return new Notifications.EmailNotification(content, "EMP Task Qualification - Trainee - Notification", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateSendReportNotification(Report report, string file, List<string> tos)
        {
            NotificationMethod method = getNotificationMethod();
            _contentGenerator = getContentGenerator(method);

            SendReportModel sendReportModel = new SendReportModel(report.InternalReportTitle);
            var template = "Notification/Content/Views/SendReport.cshtml";

            string content = _contentGenerator.GetContent(template, sendReportModel);

            var notification = new Notifications.EmailNotification(content, "Your QTS Report", method, tos);
            notification.AddAttachment(file);

            return notification;
        }


        public INotification CreateVerifyEmail(string destination, string token)
        {
            throw new NotImplementedException();
        }

        public INotification CreateVerifyPhone(string destination, string token)
        {
            throw new NotImplementedException();
        }

        protected Content.IContentGenerator getContentGenerator(NotificationMethod method)
        {
            return _contentGeneratorFactory.GetGenerator(method);
        }

        protected NotificationMethod getNotificationMethod()
        {
            // for now since we don't know if we're doing sms
            return NotificationMethod.Email;
        }

        public INotification CreateMetaILASelfPacedReleasedNotification(List<string> destination, int order, ClientSettings_Notification setting, Employee employee, Meta_ILAMembers_Link nextIla, MetaILA_Employee_MemberLinkFufillment fufilment)
        {
            if (setting.Name == "Meta ILA - Self Paced Released")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                MetaILA_SelfPacedReleasedModel metaILA_SelfPacedReleasedModel = new MetaILA_SelfPacedReleasedModel(employee.Person.FirstName, employee.Person.LastName, fufilment == null ? "" : fufilment.Meta_ILAMembers_Link.ILA.Name, nextIla.ILA.Name);
                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, metaILA_SelfPacedReleasedModel);

                return new Notifications.EmailNotification(content, "Meta ILA - Self Paced Released", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateMetaILA_Employee_SelfRegistrationRequiredNotification(List<string> destination, int order, ClientSettings_Notification setting, Employee employee, Meta_ILAMembers_Link nextIla, MetaILA_Employee_MemberLinkFufillment fufilment, List<ClassSchedule> availableClassSchedules)
        {
            if (setting.Name == "Meta ILA - Employee - Self Registration Needed")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                MetaILA_Employee_SelfRegistrationNeededModel metaILA_Employee_SelfRegistrationNeededModel = new MetaILA_Employee_SelfRegistrationNeededModel(employee.Person.FirstName, employee.Person.LastName, fufilment == null ? "" : fufilment.Meta_ILAMembers_Link.ILA.Name, nextIla.ILA.Name, availableClassSchedules.Count() > 0);
                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, metaILA_Employee_SelfRegistrationNeededModel);

                return new Notifications.EmailNotification(content, "Meta ILA - Employee - Self Registration Needed", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateMetaILA_Admin_SelfRegistrationRequiredNotification(List<string> destination, int order, ClientSettings_Notification setting, Employee employee, Meta_ILAMembers_Link nextIla, MetaILA_Employee_MemberLinkFufillment fufilment, List<ClassSchedule> availableClassSchedules)
        {
            if (setting.Name == "Meta ILA - Admin - Self Registration Needed")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                MetaILA_Admin_SelfRegistrationNeededModel metaILA_Admin_SelfRegistrationNeededModel = new MetaILA_Admin_SelfRegistrationNeededModel(employee.Person.FirstName, employee.Person.LastName, fufilment == null ? "" : fufilment.Meta_ILAMembers_Link.ILA.Name, nextIla.ILA.Name, availableClassSchedules.Count() > 0);
                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, metaILA_Admin_SelfRegistrationNeededModel);

                return new Notifications.EmailNotification(content, "Meta ILA - Admin - Self Registration Needed", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateMetaILA_Employee_RegistrationRequiredNotification(List<string> destination, int order, ClientSettings_Notification setting, Employee employee, Meta_ILAMembers_Link nextIla, MetaILA_Employee_MemberLinkFufillment fufilment)
        {
            if (setting.Name == "Meta ILA - Employee - Registration Needed")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                MetaILA_Employee_RegistrationNeededModel metaILA_Employee_RegistrationNeededModel = new MetaILA_Employee_RegistrationNeededModel(employee.Person.FirstName, employee.Person.LastName, fufilment == null ? "" : fufilment.Meta_ILAMembers_Link.ILA.Name, nextIla.ILA.Name);
                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, metaILA_Employee_RegistrationNeededModel);

                return new Notifications.EmailNotification(content, "Meta ILA - Employee - Registration Needed", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateMetaILA_Admin_RegistrationRequiredNotification(List<string> destination, int order, ClientSettings_Notification setting, Employee employee, Meta_ILAMembers_Link nextIla, MetaILA_Employee_MemberLinkFufillment fufilment)
        {
            if (setting.Name == "Meta ILA - Admin - Registration Needed")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                MetaILA_Admin_RegistrationNeededModel metaILA_Admin_RegistrationNeededModel = new MetaILA_Admin_RegistrationNeededModel(employee.Person.FirstName, employee.Person.LastName, fufilment == null ? "" : fufilment.Meta_ILAMembers_Link.ILA.Name, nextIla.ILA.Name);
                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, metaILA_Admin_RegistrationNeededModel);

                return new Notifications.EmailNotification(content, "Meta ILA - Admin - Registration Needed", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateMetaILA_CourseworkCompletedNotification(List<string> destination, int order, ClientSettings_Notification setting, Employee employee, MetaILA_Employee_MemberLinkFufillment fufilment, ClassSchedule_Roster classScheduleRoster, ClassSchedule_Evaluation_Roster classScheduleEvaluationRoster)
        {
            if (setting.Name == "Meta ILA - Coursework Complete")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                MetaILA_CourseworkCompletedModel metaILA_CourseworkCompletedModel = new MetaILA_CourseworkCompletedModel(employee.Person.FirstName, employee.Person.LastName, fufilment.Meta_ILAMembers_Link.ILA.Name);
                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, metaILA_CourseworkCompletedModel);

                return new Notifications.EmailNotification(content, "Meta ILA - Admin - Registration Needed", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateEMPOnlineCourseNotification(List<string> destination, int order, ClientSettings_Notification setting, ClassSchedule_Employee classScheduleEmployee, CBT cbt, string defaultTimeZone)
        {
            if (setting.Name == "EMP Online Course")
            {
                var employee = classScheduleEmployee.Employee;
                var instructor = classScheduleEmployee.ClassSchedule.Instructor;
                var location = classScheduleEmployee.ClassSchedule.Location;

                var cbtAvailableDate = DateTime.Now.ToUniversalTime();

                switch(cbt.Availablity)
                {
                    case CBTAvailablity.OnClassEndDateTime:
                        cbtAvailableDate = classScheduleEmployee.ClassSchedule.EndDateTime;
                        break;
                    case CBTAvailablity.OnClassStartDateTime:
                        cbtAvailableDate = classScheduleEmployee.ClassSchedule.StartDateTime;
                        break;
                    default:
                        break;
                }

                var cbtDueDate = cbt.GetDueDate(classScheduleEmployee.ClassSchedule.EndDateTime);

                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                EmpCourseModel empCourseModel = new EmpCourseModel(
                        employee.Person.FirstName, 
                        employee.Person.LastName, 
                        cbt.ILA.Name, 
                        instructor == null ? "No Instructor" : instructor.InstructorName, 
                        location == null ? "No Location" : location.LocName, 
                        classScheduleEmployee.ClassSchedule.StartDateTime, 
                        classScheduleEmployee.ClassSchedule.EndDateTime,
                        cbtAvailableDate,
                        cbtDueDate,
                        defaultTimeZone);

                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, empCourseModel);

                return new Notifications.EmailNotification(content, "EMP Online Course Notification", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateEmployeePortalCompletionNotification(List<string> destination, int order, ClientSettings_Notification setting, AdminEMPCompletionNotification employee_Completion, string defaultTimeZone)
        {
            if (setting.Name == "Admin - Employee Portal Completions")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;
                AdminEmployeePortalCompletionsModel employeeCompletion = new AdminEmployeePortalCompletionsModel(defaultTimeZone, employee_Completion.Items);
                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, employeeCompletion);

                return new Notifications.EmailNotification(content, "Employee Portal Completion Notification", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateSimulatorScenarioCollaborationNotification(List<string> destination, int order, ClientSettings_Notification setting, SimulatorScenario_Collaborator simulatorScenarioCollaborator, string url)
        {
            if (setting.Name == "Simulator Scenario Collaboration")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;
                SimulatorScenarioCollaborationModel simulatorScenarioCollaborationModel = new SimulatorScenarioCollaborationModel(
                    simulatorScenarioCollaborator.User?.Person.FirstName,
                    simulatorScenarioCollaborator.User?.Person.LastName,
                    simulatorScenarioCollaborator.SimulatorScenario?.Title,
                    url,
                    simulatorScenarioCollaborator.SimulatorScenario?.Message
                    );
                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, simulatorScenarioCollaborationModel);

                return new Notifications.EmailNotification(content, "Simulator Scenario Collaboration Notification", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }
        public INotification CreateAccountLockedNotification(string destination, string url)
        {
            NotificationMethod method = getNotificationMethod();
            _contentGenerator = getContentGenerator(method);
            var model = new ForgotPasswordModel(destination, url);
            string content = _contentGenerator.GetContent("Notification/Content/Views/AccountLockedNotifcation.cshtml", model);
            return new Notifications.EmailNotification(content, "Account Locked", method, new List<string>() { destination });
        }

        public INotification CreatePublicClassScheduleRequestNotification(List<string> destination, int order, ClientSettings_Notification setting, PublicClassScheduleRequest publicClassScheduleRequest, string defaultTimeZone)
        {
            NotificationMethod method = getNotificationMethod();
            _contentGenerator = getContentGenerator(method);
            if (setting.Name == "Public Class Schedule Request")
            {
                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;


                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template,null);

                return new Notifications.EmailNotification(content, "Public Class Schedule Request Notification", method, destination);
            }            
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }

        }

        public INotification CreatePublicClassScheduleRequestAcceptedNotification(List<string> destination, int order, ClientSettings_Notification setting, PublicClassScheduleRequest publicClassScheduleRequest, string defaultTimeZone, string url, string courseTitle)
        {
            NotificationMethod method = getNotificationMethod();
            _contentGenerator = getContentGenerator(method);
            if (setting.Name == "Public Class Schedule Request Accepted")
            {

                PublicClassScheduleRequestAcceptedModel publicClassScheduleRequestModel = new PublicClassScheduleRequestAcceptedModel(publicClassScheduleRequest.FirstName, publicClassScheduleRequest.LastName, publicClassScheduleRequest.ClassSchedule.StartDateTime, publicClassScheduleRequest.ClassSchedule.EndDateTime, defaultTimeZone, url, courseTitle);


                var step = setting.Steps.First();
                var template = step.Template;                

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, publicClassScheduleRequestModel);

                return new Notifications.EmailNotification(content, "Public Class Schedule Request Accepted Notification", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }

        }

        public INotification CreatePublicClassScheduleRequestDeclineNotification( ClientSettings_Notification setting, PublicClassScheduleRequest publicClassScheduleRequest, string defaultTimeZone)
        {
            if (setting.Name == "Public Class Schedule Request Rejected")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                PublicClassScheduleRequestRejectedModel publicClassScheduleRequestRejectedModel = new PublicClassScheduleRequestRejectedModel(publicClassScheduleRequest.FirstName, publicClassScheduleRequest.LastName, publicClassScheduleRequest.ClassSchedule.StartDateTime, publicClassScheduleRequest.ClassSchedule.EndDateTime, defaultTimeZone);

                var destination = new List<string>();
                destination.Add(publicClassScheduleRequest.EmailAddress);
                var step = setting.Steps.First();
                var template = step.Template;


                string content = _contentGenerator.GetContent(setting.Name, template, publicClassScheduleRequestRejectedModel);

                return new Notifications.EmailNotification(content, "Public Class Schedule Request Rejected Notification", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }

        }

        public INotification CreateSkillQualitificationEvaluatorNotification(List<string> destination, int order, ClientSettings_Notification setting, SkillQualification_Evaluator_Link link)
        {
            if (setting.Name == "EMP Task And Skill Qualification - Evaluator")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                EmpTaskQualitification_EvaluatorModel empTaskQualitification_EvaluatorModel = new EmpTaskQualitification_EvaluatorModel(link.Evaluator.Person.FirstName, link.Evaluator.Person.LastName, link.SkillQualification.EnablingObjective.FullNumber, link.SkillQualification.EnablingObjective.Description, link.SkillQualification.Employee.Person.FirstName + " " + link.SkillQualification.Employee.Person.LastName);

                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, empTaskQualitification_EvaluatorModel);

                return new Notifications.EmailNotification(content, "EMP Skill Qualification - Evaluator Notification", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }

        public INotification CreateSkillQualitificationTraineeNotification(List<string> destination, int order, ClientSettings_Notification setting, SkillQualification skillQualification)
        {
            if (setting.Name == "EMP Task And Skill Qualification - Trainee")
            {
                NotificationMethod method = getNotificationMethod();
                _contentGenerator = getContentGenerator(method);

                EmpTaskQualitification_TraineeModel empTaskQualitification_TraineeModel = new EmpTaskQualitification_TraineeModel(skillQualification.Employee.Person.FirstName, skillQualification.Employee.Person.LastName, skillQualification.EnablingObjective.FullNumber, skillQualification.EnablingObjective.Description, skillQualification.SkillQualification_Evaluator_Links.Count() > 0 ? String.Join(Environment.NewLine, skillQualification.SkillQualification_Evaluator_Links.Select(r => r.Evaluator.Person.FirstName + " " + r.Evaluator.Person.LastName).ToList()) : "");

                var step = setting.Steps.Where(r => r.Order == order).First();
                var template = step.Template;

                string content = _contentGenerator.GetContent(setting.Name + order.ToString(), template, empTaskQualitification_TraineeModel);

                return new Notifications.EmailNotification(content, "EMP Skill Qualification - Trainee - Notification", method, destination);
            }
            else
            {
                throw new ArgumentException("Invalid Notification Name");
            }
        }



    }
}
