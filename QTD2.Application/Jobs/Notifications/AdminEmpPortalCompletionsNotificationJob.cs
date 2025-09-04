using QTD2.Infrastructure.Database.Interfaces;
using Quartz;
using System.Linq;
using Quartz.Spi;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using QTD2.Domain.Entities.Core;
using Microsoft.Extensions.Logging;
using QTD2.Domain.Interfaces.Service.Core;
using RazorEngine.Compilation.ImpromptuInterface.InvokeExt;
using RazorEngine.Text;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Identity.Client;
using QTD2.Infrastructure.Helpers;

namespace QTD2.Application.Jobs.Notifications
{
    public class AdminEmpPortalCompletionsNotificationJob : IJob
    {
        public bool RunAtStartup => false;

        private readonly IInstanceFetcher _instanceFetcher;
        private ILogger<AdminEmpPortalCompletionsNotificationJob> _logger;
        private IDbContextBuilder _dbContextBuilder;

        public AdminEmpPortalCompletionsNotificationJob(
            IInstanceFetcher instanceFetcher, 
            ILogger<AdminEmpPortalCompletionsNotificationJob> logger,
            IDbContextBuilder dbContextBuilder
            )
        {
            _instanceFetcher = instanceFetcher;
            _logger = logger;
            _dbContextBuilder = dbContextBuilder;
        }

        async System.Threading.Tasks.Task IJob.Execute(IJobExecutionContext context)
        {
            await ExecuteAsync();
        }

        public async System.Threading.Tasks.Task ExecuteAsync()
        {
            var instances = await _instanceFetcher.GetAllActiveInstancesAsync();
            foreach (var instance in instances)
            {
                try
                {
                    _logger.LogInformation($"Admin Email scheduler for {instance.DatabaseName} beginning");
                    var db = _dbContextBuilder.BuildQtdContext(instance.DatabaseName);

                    var adminEmpNotificationSettings = (await db.ClientSettings_Notifications.Include("Steps.CustomSettings").Where(n => n.Name == "Admin - Employee Portal Completions").ToListAsync()).First();
                    if (adminEmpNotificationSettings.Enabled == false) continue;

                    foreach (var step in adminEmpNotificationSettings.Steps)
                    {
                        var emailFrequency = step.CustomSettings.Where(r => r.Key == "Email Frequency").FirstOrDefault().Value;
                        var timeOfDay = step.CustomSettings.Where(r => r.Key == "Time of Day").FirstOrDefault().Value;
                        var timeOfDay_hours = int.Parse(timeOfDay.Split(':')[0]);
                        var timeOfDay_minutes = int.Parse(timeOfDay.Split(':')[1]);
                        var dayOfWeek = Enum.Parse<DayOfWeek>(step.CustomSettings.Where(r => r.Key == "Day of Week").FirstOrDefault().Value);
                        var dayOfMonth = int.Parse(step.CustomSettings.Where(r => r.Key == "Day # of Month").FirstOrDefault().Value);

                        var clientSettings_GeneralSetting = (await db.ClientSettings_GeneralSettings.ToListAsync()).First();
                        var defaultTimeZone = clientSettings_GeneralSetting.DefaultTimeZone ?? "Central Standard Time";
                        var dateRange = NotificationDateRangeHelper.CalculatePriorDateRange(emailFrequency, timeOfDay_hours, timeOfDay_minutes, dayOfWeek, dayOfMonth, defaultTimeZone);
                        _logger.LogInformation($"Admin Email scheduler for {instance.DatabaseName} running for range {dateRange[0]} - {dateRange[1]}");
                        var startUTCDateTime = dateRange[0];
                        var endUTCDateTime = dateRange[1];

                        var recentNotifications = await db.Notifications.Where(x => x is Domain.Entities.Core.AdminEMPCompletionNotification && x.DueDate > endUTCDateTime && x.ClientSettingsNotificationStepId == step.Id).ToListAsync();

                        if (!recentNotifications.Any())
                        {
                            _logger.LogInformation($"Admin Email scheduler creating notifications for {instance.DatabaseName}");
                            var itemDictionary = new Dictionary<int, List<Domain.Entities.Core.AdminEMPCompletionNotificationItem>>();
                            await AddTestAdminEmailNotifications(db, startUTCDateTime, endUTCDateTime, itemDictionary);
                            await AddOnlineCourseAdminEmailNotifications(db, startUTCDateTime, endUTCDateTime, itemDictionary);
                            await AddProcedureReviewAdminEmailNotifications(db, startUTCDateTime, endUTCDateTime, itemDictionary);
                            await AddTaskQualAdminEmailNotifications(db, startUTCDateTime, endUTCDateTime, itemDictionary);
                            await AddDifSurveyAdminEmailNotifications(db, startUTCDateTime, endUTCDateTime, itemDictionary);
                            await AddEvaluationsAdminEmailNotifications(db, startUTCDateTime, endUTCDateTime, itemDictionary);

                            var activityNotifications = await db.ActivityNotifications.Include("PersonActivityNotifications.Person").ToListAsync();
                            var persons = activityNotifications.SelectMany(an => an.PersonActivityNotifications.Select(c => c.Person)).Distinct().ToList();

                            var adminEMPCompletionNotifications = new List<AdminEMPCompletionNotification>();
                            foreach (var person in persons)
                            {
                                var activityNotificationIds = person.PersonActivityNotifications.Select(an => an.ActivityNotificationId).ToList();
                                var itemsToAdd = itemDictionary.Where(i => activityNotificationIds.Contains(i.Key)).SelectMany(i => i.Value.Select(list => list.Copy<AdminEMPCompletionNotificationItem>(""))).ToList();
                                AdminEMPCompletionNotification notification = new AdminEMPCompletionNotification(itemsToAdd, DateTime.UtcNow, person.Id, step.Id);
                                adminEMPCompletionNotifications.Add(notification);
                            }
                            db.Notifications.AddRange(adminEMPCompletionNotifications);
                            await db.SaveChangesAsync();
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"Admin Email scheduler for {instance.DatabaseName} failed {e}", e);
                }
            }
        }
        public async System.Threading.Tasks.Task AddTestAdminEmailNotifications(Data.QTDContext db, DateTime fromDate, DateTime toDate, Dictionary<int, List<Domain.Entities.Core.AdminEMPCompletionNotificationItem>> itemDictionary)
        {
            itemDictionary[1] = new List<AdminEMPCompletionNotificationItem>();
            var classRosters = await db.ClassSchedule_Roster.Include("Employee.Person").Include("Test").Where(x => x.CompletedDate > fromDate && x.CompletedDate <= toDate).ToListAsync();
            foreach (var classRoster in classRosters)
            {
                var name = string.IsNullOrEmpty(classRoster.Employee?.Person?.MiddleName) ? $"{classRoster.Employee?.Person.FirstName}  {classRoster.Employee?.Person.LastName}" : $"{classRoster.Employee?.Person?.FirstName} {classRoster.Employee?.Person?.MiddleName} { classRoster.Employee?.Person.LastName}";
                itemDictionary[1].Add(new AdminEMPCompletionNotificationItem("Tests", classRoster.CompletedDate.GetValueOrDefault(), name, classRoster.Id,classRoster.Test?.TestTitle));
            }
        }

        public async System.Threading.Tasks.Task AddOnlineCourseAdminEmailNotifications(Data.QTDContext db, DateTime fromDate, DateTime toDate, Dictionary<int, List<Domain.Entities.Core.AdminEMPCompletionNotificationItem>> itemDictionary)
        {
            itemDictionary[2] = new List<AdminEMPCompletionNotificationItem>();
            var onlineCourses = await db.ClassScheduleEmployees.Include("Employee.Person").Include("ClassSchedule.ILA").Where(x => x.CompletionDate > fromDate && x.CompletionDate <= toDate).ToListAsync();
            foreach (var onlineCourse in onlineCourses)
            {
                var name = string.IsNullOrEmpty(onlineCourse.Employee?.Person?.MiddleName) ? $"{onlineCourse.Employee?.Person.FirstName}  {onlineCourse.Employee?.Person.LastName}" : $"{onlineCourse.Employee?.Person?.FirstName} {onlineCourse.Employee?.Person?.MiddleName} { onlineCourse.Employee?.Person.LastName}";
                itemDictionary[2].Add(new AdminEMPCompletionNotificationItem("Online Courses", onlineCourse.CompletionDate.GetValueOrDefault(), name, onlineCourse.Id, onlineCourse?.ClassSchedule?.ILA?.Name));
            }
        }
        public async System.Threading.Tasks.Task AddProcedureReviewAdminEmailNotifications(Data.QTDContext db, DateTime fromDate, DateTime toDate, Dictionary<int, List<Domain.Entities.Core.AdminEMPCompletionNotificationItem>> itemDictionary)
        {
            itemDictionary[3] = new List<AdminEMPCompletionNotificationItem>();
            var procedureReviewEmployees = await db.ProcedureReview_Employees.Include("Employee.Person").Include("ProcedureReview").Where(x => x.CompletedDate > fromDate && x.CompletedDate <= toDate).ToListAsync();
            foreach (var procedureReviewEmployee in procedureReviewEmployees)
            {
                var name = string.IsNullOrEmpty(procedureReviewEmployee.Employee?.Person?.MiddleName) ? $"{procedureReviewEmployee.Employee?.Person.FirstName}  {procedureReviewEmployee.Employee?.Person.LastName}" : $"{procedureReviewEmployee.Employee?.Person?.FirstName} {procedureReviewEmployee.Employee?.Person?.MiddleName} { procedureReviewEmployee.Employee?.Person.LastName}";
                itemDictionary[3].Add(new AdminEMPCompletionNotificationItem("Procedure Review", procedureReviewEmployee.CompletedDate.GetValueOrDefault(), name, procedureReviewEmployee.Id, procedureReviewEmployee.ProcedureReview.ProcedureReviewTitle));
            }
        }
        public async System.Threading.Tasks.Task AddTaskQualAdminEmailNotifications(Data.QTDContext db, DateTime fromDate, DateTime toDate, Dictionary<int, List<Domain.Entities.Core.AdminEMPCompletionNotificationItem>> itemDictionary)
        {
            itemDictionary[4] = new List<AdminEMPCompletionNotificationItem>();
            var taskQualifications = await db.TaskQualifications.Include("Employee.Person").Where(x => x.TaskQualificationDate > fromDate && x.TaskQualificationDate <= toDate).ToListAsync();
            var taskIds = taskQualifications.Select(m => m.TaskId).Distinct().ToList();
            var tasks = await db.Tasks.Include("SubdutyArea.DutyArea").Where(x => taskIds.Contains(x.Id)).ToListAsync();
            foreach (var taskQualification in taskQualifications)
            {
                taskQualification.Task = tasks.FirstOrDefault(x=>x.Id==taskQualification.TaskId);
                var name = string.IsNullOrEmpty(taskQualification.Employee?.Person?.MiddleName) ? $"{taskQualification.Employee?.Person.FirstName}  {taskQualification.Employee?.Person.LastName}" : $"{taskQualification.Employee?.Person?.FirstName} {taskQualification.Employee?.Person?.MiddleName} { taskQualification.Employee?.Person.LastName}";
                itemDictionary[4].Add(new AdminEMPCompletionNotificationItem("Task & Skill Qualifications", taskQualification.TaskQualificationDate.GetValueOrDefault(), name, taskQualification.Id,taskQualification?.Task?.FullNumber));
            }
        }
        public async System.Threading.Tasks.Task AddDifSurveyAdminEmailNotifications(Data.QTDContext db, DateTime fromDate, DateTime toDate, Dictionary<int, List<Domain.Entities.Core.AdminEMPCompletionNotificationItem>> itemDictionary)
        {
            itemDictionary[5] = new List<AdminEMPCompletionNotificationItem>();
            var difSurveyEmployees = await db.DIFSurvey_Employee.Include("Employee.Person").Include("DIFSurvey").Where(x => x.CompletedDate > fromDate && x.CompletedDate <= toDate).ToListAsync();
            foreach (var dIFSurveyEmployee in difSurveyEmployees)
            {
                var name = string.IsNullOrEmpty(dIFSurveyEmployee.Employee?.Person?.MiddleName) ? $"{dIFSurveyEmployee.Employee?.Person.FirstName}  {dIFSurveyEmployee.Employee?.Person.LastName}" : $"{dIFSurveyEmployee.Employee?.Person?.FirstName} {dIFSurveyEmployee.Employee?.Person?.MiddleName} { dIFSurveyEmployee.Employee?.Person.LastName}";
                itemDictionary[5].Add(new AdminEMPCompletionNotificationItem("DIF Surveys", dIFSurveyEmployee.CompletedDate.GetValueOrDefault(), name, dIFSurveyEmployee.Id,dIFSurveyEmployee?.DIFSurvey?.Title));
            }
        }
        public async System.Threading.Tasks.Task AddEvaluationsAdminEmailNotifications(Data.QTDContext db, DateTime fromDate, DateTime toDate, Dictionary<int, List<Domain.Entities.Core.AdminEMPCompletionNotificationItem>> itemDictionary)
        {
            itemDictionary[7] = new List<AdminEMPCompletionNotificationItem>();
            var classEvalRosters = await db.ClassSchedule_Evaluation_Roster.Include("Employee.Person").Include("StudentEvaluationInfo").Where(x => x.CompletedDate > fromDate && x.CompletedDate <= toDate).ToListAsync();
            foreach (var classEvalRoster in classEvalRosters)
            {
                var name = string.IsNullOrEmpty(classEvalRoster.Employee?.Person?.MiddleName) ? $"{classEvalRoster.Employee?.Person.FirstName}  {classEvalRoster.Employee?.Person.LastName}" : $"{classEvalRoster.Employee?.Person?.FirstName} {classEvalRoster.Employee?.Person?.MiddleName} { classEvalRoster.Employee?.Person.LastName}";
                itemDictionary[7].Add(new AdminEMPCompletionNotificationItem("Student Evaluations", classEvalRoster.CompletedDate.GetValueOrDefault(), name, classEvalRoster.Id, classEvalRoster?.StudentEvaluationInfo?.Title));
            }
        }
    }
}
