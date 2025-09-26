using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MediatR;
using QTD2.Application.Interfaces.Services.Authentication;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Application.Services.Shared;
using QTD2.Application.Services.Summary;
using QTD2.Infrastructure.Authorization.Handlers.Authentication;
using QTD2.Infrastructure.Authorization.Handlers.Core;
using QTD2.Infrastructure.Database;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.Database.Settings;
using QTD2.Application.EventHandlers.Core;
using QTD2.Data.Persistence;
using QTD2.Domain.Persistence;
using Microsoft.Extensions.Hosting;
using QTD2.Application.Jobs.Notifications;
using QTD2.Domain.Events.Core;
using QTD2.Application.Services.Authentication;
using QTD2.Application.EventHandlers;
using QTD2.Domain.ClassScheduleEmployee.GradeEvaluation;
using System;

namespace QTD2.Application.Startup
{
    public static class DependencyInjection
    {
        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<List<DbContextConfiguration>>(options => configuration.GetSection("DbContexts").Bind(options));

            services.AddTransient<IDbContextBuilder, DefaultDbContextBuilder>();
            services.AddTransient<IDatabaseManager, DefaultDatabaseManager>();
            services.AddTransient<IDatabaseResolver, DefaultDatabaseResolver>();
        }

        public static void AddQTDDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
        }

        public static void AddAuthenticationDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<Domain.Interfaces.Service.Authentication.IClientService, Domain.Services.Authentication.ClientService>();
            services.AddTransient<Domain.Interfaces.Validation.Authentication.IClientValidation, Domain.Entities.Authentication.Validations.ClientValidation>();

            services.AddTransient<Domain.Interfaces.Service.Authentication.IInstanceService, Domain.Services.Authentication.InstanceService>();
            services.AddTransient<Domain.Interfaces.Validation.Authentication.IInstanceValidation, Domain.Entities.Authentication.Validations.InstanceValidation>();

            services.AddTransient<Domain.Interfaces.Service.Authentication.IInstanceSettingService, Domain.Services.Authentication.InstanceSettingService>();
            services.AddTransient<Domain.Interfaces.Validation.Authentication.IInstanceSettingValidation, Domain.Entities.Authentication.Validations.InstanceSettingValidation>();

            services.AddTransient<Domain.Interfaces.Service.Authentication.IEventLogService, Domain.Services.Authentication.EventLogService>();
            services.AddTransient<Domain.Interfaces.Validation.Authentication.IEventLogValidation, Domain.Entities.Authentication.Validations.EventLogValidation>();

            services.AddTransient<Domain.Interfaces.Service.Authentication.IIdentityProviderService, Domain.Services.Authentication.IdentityProviderService>();
            services.AddTransient<Domain.Interfaces.Validation.Authentication.IIdentityProviderValidation, Domain.Entities.Authentication.Validations.IdentityProviderValidation>();

            services.AddTransient<Domain.Interfaces.Service.Authentication.IInstanceIdentityProviderLinkService, Domain.Services.Authentication.InstanceIdentityProviderLinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Authentication.IInstanceIdentityProviderLinkValidation, Domain.Entities.Authentication.Validations.InstanceIdentityProviderLinkValidation>();
            
            services.AddTransient<Domain.Interfaces.Service.Authentication.IAuthenticationSettingService, Domain.Services.Authentication.AuthenticationSettingService>();
            services.AddTransient<Domain.Interfaces.Validation.Authentication.IAuthenticationSettingValidation, Domain.Entities.Authentication.Validations.AuthenticationSettingValidation>();

            services.AddTransient<Domain.Interfaces.Service.Authentication.IAdminMessageAuthService, Domain.Services.Authentication.AdminMessageAuthService>();
            services.AddTransient<Domain.Interfaces.Validation.Authentication.IAdminMessageAuthValidation, Domain.Entities.Authentication.Validations.AdminMessageAuthValidation>();

        }

        public static void AddQTDDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            // ------------------ Domain Service and Validations DI Start --------------------- //
            services.AddTransient<Domain.Interfaces.Service.Core.IToolCategory_StatusHistoryService, Domain.Services.Core.ToolCategory_StatusHistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IToolCategory_StatusHistoryValidation, Domain.Entities.Core.Validations.ToolCategory_StatusHistoryValidation>();
            services.AddTransient<Domain.Interfaces.Service.Core.IILACertificationLinkService, Domain.Services.Core.ILACertificationLinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILACertificationLinkValidation, Domain.Entities.Core.Validations.ILACertificationLinkValidation>();
            services.AddTransient<Domain.Interfaces.Service.Core.IILACertificationSubRequirementLinkService, Domain.Services.Core.ILACertificationSubRequirementLinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILACertificationSubRequirementLinkValidation, Domain.Entities.Core.Validations.ILACertificationSubRequirementLinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IProcedureReview_EmployeeService, Domain.Services.Core.ProcedureReview_EmployeeService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IProcedureReview_EmployeeValidation, Domain.Entities.Core.Validations.ProcedureReview_EmployeeValidation>();
            services.AddTransient<Domain.Interfaces.Service.Core.IProcedureReviewService, Domain.Services.Core.ProcedureReviewService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IProcedureReviewValidation, Domain.Entities.Core.Validations.ProcedureReviewValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITaskReQualificationEmp_SignOffService, Domain.Services.Core.TaskReQualificationEmp_SignOffService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITaskReQualificationEmp_SignOffValidation, Domain.Entities.Core.Validations.TaskReQualificationEmp_SignOffValidation>();
            services.AddTransient<Domain.Interfaces.Service.Core.ITaskReQualificationEmp_QuestionAnswerService, Domain.Services.Core.TaskReQualificationEmp_QuestionAnswerService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITaskReQualificationEmp_QuestionAnswerValidation, Domain.Entities.Core.Validations.TaskReQualificationEmp_QuestionAnswerValidation>();
            services.AddTransient<Domain.Interfaces.Service.Core.ITaskReQualificationEmp_StepsService, Domain.Services.Core.TaskReQualificationEmp_StepsService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITaskReQualificationEmp_StepsValidation, Domain.Entities.Core.Validations.TaskReQualificationEmp_StepsValidation>();
            services.AddTransient<Domain.Interfaces.Service.Core.ITaskReQualificationEmp_SuggestionService, Domain.Services.Core.TaskReQualificationEmp_SuggestionService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITaskReQualificationEmp_SuggestionValidation, Domain.Entities.Core.Validations.TaskReQualificationEmp_SuggestionValidation>();
            services.AddTransient<Domain.Interfaces.Service.Core.IILACertificationLinkService, Domain.Services.Core.ILACertificationLinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILACertificationLinkValidation, Domain.Entities.Core.Validations.ILACertificationLinkValidation>();
            services.AddTransient<Domain.Interfaces.Service.Core.IILACertificationSubRequirementLinkService, Domain.Services.Core.ILACertificationSubRequirementLinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILACertificationSubRequirementLinkValidation, Domain.Entities.Core.Validations.ILACertificationSubRequirementLinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IProcedureReview_EmployeeService, Domain.Services.Core.ProcedureReview_EmployeeService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IProcedureReview_EmployeeValidation, Domain.Entities.Core.Validations.ProcedureReview_EmployeeValidation>();
            services.AddTransient<Domain.Interfaces.Service.Core.IProcedureReviewService, Domain.Services.Core.ProcedureReviewService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IProcedureReviewValidation, Domain.Entities.Core.Validations.ProcedureReviewValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ICertificationService, Domain.Services.Core.CertificationService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ICertificationValidation, Domain.Entities.Core.Validations.CertificationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ICertification_HistoryService, Domain.Services.Core.Certification_HistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ICertification_HistoryValidation, Domain.Entities.Core.Validations.Certification_HistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ICertifyingBodyService, Domain.Services.Core.CertifyingBodyService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ICertifyingBodyValidation, Domain.Entities.Core.Validations.CertifyingBodyValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IDocumentService, Domain.Services.Core.DocumentService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IDocumentValidation, Domain.Entities.Core.Validations.DocumentValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IDocumentTypeService, Domain.Services.Core.DocumentTypeService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IDocumentTypeValidation, Domain.Entities.Core.Validations.DocumentTypeValidation>();

            //services.AddTransient<Domain.Interfaces.Service.Core.ICertifyingBody_HistoryService, Domain.Services.Core.CertifyingBody_HistoryService>();
            //services.AddTransient<Domain.Interfaces.Validation.Core.ICertifyingBody_HistoryValidation, Domain.Entities.Core.Validations.CertifyingBody_HistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IPersonService, Domain.Services.Core.PersonService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IPersonValidation, Domain.Entities.Core.Validations.PersonValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClientUserService, Domain.Services.Core.ClientUserService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClientUserValidation, Domain.Entities.Core.Validations.ClientUserValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEmployeeService, Domain.Services.Core.EmployeeService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEmployeeValidation, Domain.Entities.Core.Validations.EmployeeValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEmployeeCertificationService, Domain.Services.Core.EmployeeCertificationService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEmployeeCertificationValidation, Domain.Entities.Core.Validations.EmployeeCertificationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEmployeeOrganizationService, Domain.Services.Core.EmployeeOrganizationService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEmployeeOrganizationValidation, Domain.Entities.Core.Validations.EmployeeOrganizationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEmployeePositionService, Domain.Services.Core.EmployeePositionService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEmployeePositionValidation, Domain.Entities.Core.Validations.EmployeePositionValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IOrganizationService, Domain.Services.Core.OrganizationService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IOrganizationValidation, Domain.Entities.Core.Validations.OrganizationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITrainingProgramService, Domain.Services.Core.TrainingProgramService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITrainingProgramValidation, Domain.Entities.Core.Validations.TrainingProgramValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IPositionService, Domain.Services.Core.PositionService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IPositionValidation, Domain.Entities.Core.Validations.PositionValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IDutyAreaService, Domain.Services.Core.DutyAreaService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IDutyAreaValidation, Domain.Entities.Core.Validations.DutyAreaValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEnablingObjectiveService, Domain.Services.Core.EnablingObjectiveService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEnablingObjectiveValidation, Domain.Entities.Core.Validations.EnablingObjectiveValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEnablingObjective_CategoryService, Domain.Services.Core.EnablingObjective_CategoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEnablingObjective_CategoryValidation, Domain.Entities.Core.Validations.EnablingObjective_CategoryValidation>();

            //services.AddTransient<Domain.Interfaces.Service.Core.IEnablingObjective_Procedure_LinkService, Domain.Services.Core.EnablingObjective_Procedure_LinkService>();
            //services.AddTransient<Domain.Interfaces.Validation.Core.IEnablingObjective_Procedure_LinkValidation, Domain.Entities.Core.Validations.EnablingObjective_Procedure_LinkValidation>();

            //services.AddTransient<Domain.Interfaces.Service.Core.IEnablingObjective_SaftyHazard_LinkService, Domain.Services.Core.EnablingObjective_SaftyHazard_LinkService>();
            //services.AddTransient<Domain.Interfaces.Validation.Core.IEnablingObjective_SaftyHazard_LinkValidation, Domain.Entities.Core.Validations.EnablingObjective_SaftyHazard_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClassSchedule_SelfRegistrationOptionsService, Domain.Services.Core.ClassSchedule_SelfRegistrationOptionsService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClassSchedule_SelfRegistrationValidation, Domain.Entities.Core.Validations.ClassSchedule_SelfRegistrationValidation>();


            services.AddTransient<Domain.Interfaces.Service.Core.IEnablingObjective_SubCategoryService, Domain.Services.Core.EnablingObjective_SubCategoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEnablingObjective_SubCategoryValidation, Domain.Entities.Core.Validations.EnablingObjective_SubCategoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEnablingObjective_TopicService, Domain.Services.Core.EnablingObjective_TopicService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEnablingObjective_TopicValidation, Domain.Entities.Core.Validations.EnablingObjective_TopicValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IProcedureService, Domain.Services.Core.ProcedureService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IProcedureValidation, Domain.Entities.Core.Validations.ProcedureValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IProcedure_EnablingObjective_LinkService, Domain.Services.Core.Procedure_EnablingObjective_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IProcedure_EnablingObjective_LinkValidation, Domain.Entities.Core.Validations.Procedure_EnablingObjective_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IProcedure_IssuingAuthorityService, Domain.Services.Core.Procedure_IssuingAuthorityService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IProcedure_IssuingAuthorityValidation, Domain.Entities.Core.Validations.Procedure_IssuingAuthorityValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IProcedure_SaftyHazard_LinkService, Domain.Services.Core.Procedure_SaftyHazard_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IProcedure_SaftyHazard_LinkValidation, Domain.Entities.Core.Validations.Procedure_SaftyHazard_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISaftyHazardService, Domain.Services.Core.SaftyHazardService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISaftyHazardValidation, Domain.Entities.Core.Validations.SaftyHazardValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISaftyHazard_AbatementService, Domain.Services.Core.SaftyHazard_AbatementService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISaftyHazard_AbatementValidation, Domain.Entities.Core.Validations.SaftyHazard_AbatementValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISaftyHazard_CategoryService, Domain.Services.Core.SaftyHazard_CategoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISaftyHazard_CategoryValidation, Domain.Entities.Core.Validations.SaftyHazard_CategoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISaftyHazard_ControlService, Domain.Services.Core.SaftyHazard_ControlService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISaftyHazard_ControlValidation, Domain.Entities.Core.Validations.SaftyHazard_ControlValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISubdutyAreaService, Domain.Services.Core.SubdutyAreaService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISubdutyAreaValidation, Domain.Entities.Core.Validations.SubdutyAreaValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITaskService, Domain.Services.Core.TaskService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITaskValidation, Domain.Entities.Core.Validations.TaskValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITask_EnablingObjective_LinkService, Domain.Services.Core.Task_EnablingObjective_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITask_EnablingObjective_LinkValidation, Domain.Entities.Core.Validations.Task_EnablingObjective_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.INotificationRecipietService, Domain.Services.Core.NotificationRecipietService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.INotificationRecipietValidation, Domain.Entities.Core.Validations.NotificationRecipietValidation>();


            //services.AddTransient<Domain.Interfaces.Service.Core.ITask_Procedure_LinkService, Domain.Services.Core.Task_Procedure_LinkService>();
            //services.AddTransient<Domain.Interfaces.Validation.Core.ITask_Procedure_LinkValidation, Domain.Entities.Core.Validations.Task_Procedure_LinkValidation>();

            //services.AddTransient<Domain.Interfaces.Service.Core.ITask_SaftyHazard_LinkService, Domain.Services.Core.Task_SaftyHazard_LinkService>();
            //services.AddTransient<Domain.Interfaces.Validation.Core.ITask_SaftyHazard_LinkValidation, Domain.Entities.Core.Validations.Task_SaftyHazard_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITask_StepService, Domain.Services.Core.Task_StepService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITask_StepValidation, Domain.Entities.Core.Validations.Task_StepValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITask_ToolService, Domain.Services.Core.Task_ToolService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITask_ToolValidation, Domain.Entities.Core.Validations.Task_ToolValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IToolService, Domain.Services.Core.ToolService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IToolValidation, Domain.Entities.Core.Validations.ToolValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IToolGroupService, Domain.Services.Core.ToolGroupService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IToolGroupValidation, Domain.Entities.Core.Validations.ToolGroupValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IToolGroup_ToolService, Domain.Services.Core.ToolGroup_ToolService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IToolGroup_ToolValidation, Domain.Entities.Core.Validations.ToolGroup_ToolValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_TaskService, Domain.Services.Core.Version_TaskService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_TaskValidation, Domain.Entities.Core.Validations.Version_TaskValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_Task_QuestionService, Domain.Services.Core.Version_Task_QuestionService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_Task_QuestionValidation, Domain.Entities.Core.Validations.Version_Task_QuestionValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_ProcedureService, Domain.Services.Core.Version_ProcedureService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_ProcedureValidation, Domain.Entities.Core.Validations.Version_ProcedureValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_Task_Procedure_LinkService, Domain.Services.Core.Version_Task_Procedure_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_Task_Procedure_LinkValidation, Domain.Entities.Core.Validations.Version_Task_Procedure_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_ToolService, Domain.Services.Core.Version_ToolService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_ToolValidation, Domain.Entities.Core.Validations.Version_ToolValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_Task_Tool_LinkService, Domain.Services.Core.Version_Task_Tool_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_Task_Tool_LinkValidation, Domain.Entities.Core.Validations.Version_Task_Tool_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_Procedure_Tool_LinkService, Domain.Services.Core.Version_Procedure_Tool_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_Procedure_Tool_LinkValidation, Domain.Entities.Core.Validations.Version_Procedure_Tool_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_EnablingObjectiveService, Domain.Services.Core.Version_EnablingObjectiveService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_EnablingObjectiveValidation, Domain.Entities.Core.Validations.Version_EnablingObjectiveValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_EnablingObjective_Tool_LinkService, Domain.Services.Core.Version_EnablingObjective_Tool_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_EnablingObjective_Tool_LinkValidation, Domain.Entities.Core.Validations.Version_EnablingObjective_Tool_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_SaftyHazardService, Domain.Services.Core.Version_SaftyHazardService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_SaftyHazardValidation, Domain.Entities.Core.Validations.Version_SaftyHazardValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_SaftyHazard_AbatementService, Domain.Services.Core.Version_SaftyHazard_AbatementService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_SaftyHazard_AbatementValidation, Domain.Entities.Core.Validations.Version_SaftyHazard_AbatementValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_SaftyHazard_ControlService, Domain.Services.Core.Version_SaftyHazard_ControlService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_SaftyHazard_ControlValidation, Domain.Entities.Core.Validations.Version_SaftyHazard_ControlValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_Task_EnablingObjective_LinkService, Domain.Services.Core.Version_Task_EnablingObjective_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_Task_EnablingObjective_LinkValidation, Domain.Entities.Core.Validations.Version_Task_EnablingObjective_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_Task_SaftyHazard_LinkService, Domain.Services.Core.Version_Task_SaftyHazard_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_Task_SaftyHazard_LinkValidation, Domain.Entities.Core.Validations.Version_Task_SaftyHazard_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_Task_StepService, Domain.Services.Core.Version_Task_StepService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_Task_StepValidation, Domain.Entities.Core.Validations.Version_Task_StepValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_Procedure_SaftyHazard_LinkService, Domain.Services.Core.Version_Procedure_SaftyHazard_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_Procedure_SaftyHazard_LinkValidation, Domain.Entities.Core.Validations.Version_Procedure_SaftyHazard_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_EnablingObjective_SaftyHazard_LinkService, Domain.Services.Core.Version_EnablingObjective_SaftyHazard_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_EnablingObjective_SaftyHazard_LinkValidation, Domain.Entities.Core.Validations.Version_EnablingObjective_SaftyHazard_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_EnablingObjective_Procedure_LinkService, Domain.Services.Core.Version_EnablingObjective_Procedure_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_EnablingObjective_Procedure_LinkValidation, Domain.Entities.Core.Validations.Version_EnablingObjective_Procedure_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEmployee_TaskService, Domain.Services.Core.Employee_TaskService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEmployee_TaskValidation, Domain.Entities.Core.Validations.Employee_TaskValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITimesheetService, Domain.Services.Core.TimesheetService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITimesheetValidation, Domain.Entities.Core.Validations.TimesheetValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITask_QuestionService, Domain.Services.Core.Task_QuestionService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITask_QuestionValidation, Domain.Entities.Core.Validations.Task_QuestionValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITask_PositionService, Domain.Services.Core.Task_PositionService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITask_PositionValidation, Domain.Entities.Core.Validations.Task_PositionValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IProviderLevelService, Domain.Services.Core.ProviderLevelService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IProviderLevelValidation, Domain.Entities.Core.Validations.ProviderLevelValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IProviderService, Domain.Services.Core.ProviderService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IProviderValidation, Domain.Entities.Core.Validations.ProviderValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IILA_TopicService, Domain.Services.Core.ILA_TopicService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILA_TopicValidation, Domain.Entities.Core.Validations.ILA_TopicValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IScormUploadService, Domain.Services.Core.ScormUploadService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IScormUploadValidation, Domain.Entities.Core.Validations.ScormUploadValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IDeliveryMethodService, Domain.Services.Core.DeliveryMethodService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IDeliveryMethodValidation, Domain.Entities.Core.Validations.DeliveryMethodValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITrainingTopicService, Domain.Services.Core.TrainingTopicService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITrainingTopicValidation, Domain.Entities.Core.Validations.TrainingTopicValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.INercStandardService, Domain.Services.Core.NercStandardService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.INercStandardValidation, Domain.Entities.Core.Validations.NercStandardValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITraineeEvaluationTypeService, Domain.Services.Core.TraineeEvaluationTypeService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITraineeEvaluationTypeValidation, Domain.Entities.Core.Validations.TraineeEvaluationTypeValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IMetaILAService, Domain.Services.Core.MetaILAService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IMetaILAValidation, Domain.Entities.Core.Validations.MetaILAValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISegmentService, Domain.Services.Core.SegmentService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISegmentValidation, Domain.Entities.Core.Validations.SegmentValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IAssessmentToolService, Domain.Services.Core.AssessmentToolService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IAssessmentToolValidation, Domain.Entities.Core.Validations.AssessmentToolValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IRR_IssuingAuthorityService, Domain.Services.Core.RR_IssuingAuthorityService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IRR_IssuingAuthorityValidation, Domain.Entities.Core.Validations.RR_IssuingAuthorityValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IRegulatoryRequirementService, Domain.Services.Core.RegulatoryRequirementService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IRegulatoryRequirementValidation, Domain.Entities.Core.Validations.RegulatoryRequirementValidation>();

            //services.AddTransient<Domain.Interfaces.Service.Core.IRR_SafetyHazard_LinkService, Domain.Services.Core.RR_SafetyHazard_LinkService>();
            //services.AddTransient<Domain.Interfaces.Validation.Core.IRR_SafetyHazard_LinkValidation, Domain.Entities.Core.Validations.RR_SafetyHazard_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IILAService, Domain.Services.Core.ILAService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILAValidation, Domain.Entities.Core.Validations.ILAValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IILA_NercStandard_LinkService, Domain.Services.Core.ILA_NercStandard_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILA_NercStandard_LinkValidation, Domain.Entities.Core.Validations.ILA_NercStandard_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IILA_SafetyHazard_LinkService, Domain.Services.Core.ILA_SafetyHazard_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILA_SafetyHazard_LinkValidation, Domain.Entities.Core.Validations.ILA_SafetyHazard_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IILA_Segment_LinkService, Domain.Services.Core.ILA_Segment_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILA_Segment_LinkValidation, Domain.Entities.Core.Validations.ILA_Segment_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IILAResourceService, Domain.Services.Core.ILAResourceService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILAResourceValidation, Domain.Entities.Core.Validations.ILAResourceValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IILACollaboratorService, Domain.Services.Core.ILACollaboratorService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILACollaboratorValidation, Domain.Entities.Core.Validations.ILACollaboratorValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IILA_Position_LinkService, Domain.Services.Core.ILA_Position_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILA_Position_LinkValidation, Domain.Entities.Core.Validations.ILA_Position_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IRR_Task_LinkService, Domain.Services.Core.RR_Task_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IRR_Task_LinkValidation, Domain.Entities.Core.Validations.RR_Task_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IILA_TaskObjective_LinkService, Domain.Services.Core.ILA_TaskObjective_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILA_TaskObjective_LinkValidation, Domain.Entities.Core.Validations.ILA_TaskObjective_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IILA_EnablingObjective_LinkService, Domain.Services.Core.ILA_EnablingObjective_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILA_EnablingObjective_LinkValidation, Domain.Entities.Core.Validations.ILA_EnablingObjective_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IILA_Procedure_LinkService, Domain.Services.Core.ILA_Procedure_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILA_Procedure_LinkValidation, Domain.Entities.Core.Validations.ILA_Procedure_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IILA_TrainingTopic_LinkService, Domain.Services.Core.ILA_TrainingTopic_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILA_TrainingTopic_LinkValidation, Domain.Entities.Core.Validations.ILA_TrainingTopic_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IILA_RegRequirement_LinkService, Domain.Services.Core.ILA_RegRequirement_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILA_RegRequirement_LinkValidation, Domain.Entities.Core.Validations.ILA_RegRequirement_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IILA_AssessmentTool_LinkService, Domain.Services.Core.ILA_AssessmentTool_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILA_AssessmentTool_LinkValidation, Domain.Entities.Core.Validations.ILA_AssessmentTool_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IRegRequirement_EO_LinkService, Domain.Services.Core.RegRequirement_EO_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IRegRequirement_EO_LinkValidation, Domain.Entities.Core.Validations.RegRequirement_EO_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IProcedure_RR_LinkService, Domain.Services.Core.Procedure_RR_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IProcedure_RR_LinkValidation, Domain.Entities.Core.Validations.Procedure_RR_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IProcedure_ILA_LinkService, Domain.Services.Core.Procedure_ILA_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IProcedure_ILA_LinkValidation, Domain.Entities.Core.Validations.Procedure_ILA_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISaftyHazard_RR_LinkService, Domain.Services.Core.SaftyHazard_RR_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISaftyHazard_RR_LinkValidation, Domain.Entities.Core.Validations.SaftyHazard_RR_LinkValidation>();

            //services.AddTransient<Domain.Interfaces.Service.Core.ISafetyHazard_Procedure_LinkService, Domain.Services.Core.SafetyHazard_Procedure_LinkService>();
            //services.AddTransient<Domain.Interfaces.Validation.Core.ISafetyHazard_Procedure_LinkValidation, Domain.Entities.Core.Validations.SafetyHazard_Procedure_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITrainingTopic_CategoryService, Domain.Services.Core.TrainingTopic_CategoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITrainingTopic_CategoryValidation, Domain.Entities.Core.Validations.TrainingTopic_CategoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.INERCTargetAudienceService, Domain.Services.Core.NERCTargetAudienceService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.INERCTargetAudienceValidation, Domain.Entities.Core.Validations.NERCTargetAudienceValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IRatingScaleService, Domain.Services.Core.RatingScaleService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IRatingScaleValidation, Domain.Entities.Core.Validations.RatingScaleValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IStudentEvaluationFormService, Domain.Services.Core.StudentEvaluationFormService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IStudentEvaluationFormValidation, Domain.Entities.Core.Validations.StudentEvaluationFormValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IStudentEvaluationAvailabilityService, Domain.Services.Core.StudentEvaluationAvailabilityService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IStudentEvaluationAvailabilityValidation, Domain.Entities.Core.Validations.StudentEvaluationAvailabilityValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IILA_NERCAudience_LinkService, Domain.Services.Core.ILA_NERCAudience_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILA_NERCAudience_LinkValidation, Domain.Entities.Core.Validations.ILA_NERCAudience_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IILA_StudentEvaluation_LinkService, Domain.Services.Core.ILA_StudentEvaluation_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILA_StudentEvaluation_LinkValidation, Domain.Entities.Core.Validations.ILA_StudentEvaluation_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ICoverSheetTypeService, Domain.Services.Core.CoverSheetTypeService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ICoverSheetTypeValidation, Domain.Entities.Core.Validations.CoverSheetTypeValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IStudentEvaluationQuestionService, Domain.Services.Core.StudentEvaluationQuestionService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IStudentEvaluationQuestionValidation, Domain.Entities.Core.Validations.StudentEvaluationQuestionValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IMeta_ILAMembers_LinkService, Domain.Services.Core.Meta_ILAMembers_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IMeta_ILAMembers_LinkValidation, Domain.Entities.Core.Validations.Meta_ILAMembers_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.INercStandardMemberService, Domain.Services.Core.NercStandardMemberService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.INercStandardMemberValidation, Domain.Entities.Core.Validations.NercStandardMemberValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ICollaboratorInvitationService, Domain.Services.Core.CollaboratorInvitationService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ICollaboratorInvitationValidation, Domain.Entities.Core.Validations.CollaboratorInvitationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ICoversheetService, Domain.Services.Core.CoversheetService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ICoversheetValidation, Domain.Entities.Core.Validations.CoversheetValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ICustomEnablingObjectiveService, Domain.Services.Core.CustomEnablingObjectiveService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ICustomEnablingObjectiveValidation, Domain.Entities.Core.Validations.CustomEnablingObjectiveValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISegmentObjective_LinkService, Domain.Services.Core.SegmentObjective_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISegmentObjective_LinkValidation, Domain.Entities.Core.Validations.SegmentObjective_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IStudentEvaluationAudienceService, Domain.Services.Core.StudentEvaluationAudienceService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IStudentEvaluationAudienceValidation, Domain.Entities.Core.Validations.StudentEvaluationAudienceValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IILACustomObjective_LinkService, Domain.Services.Core.ILACustomObjective_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILACustomObjective_LinkValidation, Domain.Entities.Core.Validations.ILACustomObjective_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IILA_PreRequisite_LinkService, Domain.Services.Core.ILA_PreRequisite_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILA_PreRequisite_LinkValidation, Domain.Entities.Core.Validations.ILA_PreRequisite_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITaxonomyLevelService, Domain.Services.Core.TaxonomyLevelService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITaxonomyLevelValidation, Domain.Entities.Core.Validations.TaxonomyLevelValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITestStatusService, Domain.Services.Core.TestStatusService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITestStatusValidation, Domain.Entities.Core.Validations.TestStatusValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITestService, Domain.Services.Core.TestService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITestValidation, Domain.Entities.Core.Validations.TestValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITestTypeService, Domain.Services.Core.TestTypeService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITestTypeValidation, Domain.Entities.Core.Validations.TestTypeValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITestSettingService, Domain.Services.Core.TestSettingService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITestSettingValidation, Domain.Entities.Core.Validations.TestSettingValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITestItemTypeService, Domain.Services.Core.TestItemTypeService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITestItemTypeValidation, Domain.Entities.Core.Validations.TestItemTypeValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IILA_UploadService, Domain.Services.Core.ILA_UploadService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILA_UploadValidation, Domain.Entities.Core.Validations.ILA_UploadValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITestItemService, Domain.Services.Core.TestItemService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITestItemValidation, Domain.Entities.Core.Validations.TestItemValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITestItemTrueFalseService, Domain.Services.Core.TestItemTrueFalseService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITestItemTrueFalseValidation, Domain.Entities.Core.Validations.TestItemTrueFalseValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITestItemMatchService, Domain.Services.Core.TestItemMatchService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITestItemMatchValidation, Domain.Entities.Core.Validations.TestItemMatchValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITestItemMCQService, Domain.Services.Core.TestItemMCQService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITestItemMCQValidation, Domain.Entities.Core.Validations.TestItemMCQValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITestItemFillBlankService, Domain.Services.Core.TestItemFillBlankService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITestItemFillBlankValidation, Domain.Entities.Core.Validations.TestItemFillBlankValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IILATraineeEvaluationService, Domain.Services.Core.ILATraineeEvaluationService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILATraineeEvaluationValidation, Domain.Entities.Core.Validations.ILATraineeEvaluationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITestItemShortAnswerService, Domain.Services.Core.TestItemShortAnswerService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITestItemShortAnswerValidation, Domain.Entities.Core.Validations.TestItemShortAnswerValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITest_Item_LinkService, Domain.Services.Core.Test_Item_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITest_Item_LinkValidation, Domain.Entities.Core.Validations.Test_Item_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IQTDUserService, Domain.Services.Core.QTDUserService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IQTDUserValidation, Domain.Entities.Core.Validations.QTDUserValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IProc_IssuingAuthority_HistoryService, Domain.Services.Core.Proc_IssuingAuthority_HistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IProc_IssuingAuthority_HistoryValidation, Domain.Entities.Core.Validations.Proc_IssuingAuthority_HistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IProcedure_Task_LinkService, Domain.Services.Core.Procedure_Task_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IProcedure_Task_LinkValidation, Domain.Entities.Core.Validations.Procedure_Task_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IProcedure_StatusHistoryService, Domain.Services.Core.Procedure_StatusHistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IProcedure_StatusHistoryValidation, Domain.Entities.Core.Validations.Procedure_StatusHistoryValidation>();

            //services.AddTransient<Domain.Interfaces.Service.Core.IRR_Procedure_LinkService, Domain.Services.Core.RR_Procedure_LinkService>();
            //services.AddTransient<Domain.Interfaces.Validation.Core.IRR_Procedure_LinkValidation, Domain.Entities.Core.Validations.RR_Procedure_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IRR_StatusHistoryService, Domain.Services.Core.RR_StatusHistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IRR_StatusHistoryValidation, Domain.Entities.Core.Validations.RR_StatusHistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISafetyHazard_EO_LinkService, Domain.Services.Core.SafetyHazard_EO_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISafetyHazard_EO_LinkValidation, Domain.Entities.Core.Validations.SafetyHazard_EO_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISafetyHazard_Task_LinkService, Domain.Services.Core.SafetyHazard_Task_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISafetyHazard_Task_LinkValidation, Domain.Entities.Core.Validations.SafetyHazard_Task_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISafetyHazard_SetService, Domain.Services.Core.SafetyHazard_SetService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISafetyHazard_SetValidation, Domain.Entities.Core.Validations.SafetyHazard_SetValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISafetyHazard_Set_LinkService, Domain.Services.Core.SafetyHazard_Set_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISafetyHazard_Set_LinkValidation, Domain.Entities.Core.Validations.SafetyHazard_Set_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISafetyHazard_HistoryService, Domain.Services.Core.SafetyHazard_HistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISafetyHazard_HistoryValidation, Domain.Entities.Core.Validations.SafetyHazard_HistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISafetyHazard_CategoryHistoryService, Domain.Services.Core.SafetyHazard_CategoryHistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISafetyHazard_CategoryHistoryValidation, Domain.Entities.Core.Validations.SafetyHazard_CategoryHistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITask_ReferenceService, Domain.Services.Core.Task_ReferenceService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITask_ReferenceValidation, Domain.Entities.Core.Validations.Task_ReferenceValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITask_Reference_LinkService, Domain.Services.Core.Task_Reference_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITask_Reference_LinkValidation, Domain.Entities.Core.Validations.Task_Reference_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITask_ILA_LinkService, Domain.Services.Core.Task_ILA_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITask_ILA_LinkValidation, Domain.Entities.Core.Validations.Task_ILA_LinkValidation>();

            //services.AddTransient<Domain.Interfaces.Service.Core.ITask_RR_LinkService, Domain.Services.Core.Task_RR_LinkService>();
            //services.AddTransient<Domain.Interfaces.Validation.Core.ITask_RR_LinkValidation, Domain.Entities.Core.Validations.Task_RR_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITask_CollaboratorInvitationService, Domain.Services.Core.Task_CollaboratorInvitationService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITask_CollaboratorInvitationValidation, Domain.Entities.Core.Validations.Task_CollaboratorInvitationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITask_Collaborator_LinkService, Domain.Services.Core.Task_Collaborator_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITask_Collaborator_LinkValidation, Domain.Entities.Core.Validations.Task_Collaborator_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IDiscussionQuestionService, Domain.Services.Core.DiscussionQuestionService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IDiscussionQuestionValidation, Domain.Entities.Core.Validations.DiscussionQuestionsValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEnablingObjective_CategoryHistoryService, Domain.Services.Core.EnablingObjective_CategoryHistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEnablingObjective_CategoryHistoryValidation, Domain.Entities.Core.Validations.EnablingObjective_CategoryHistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IRR_IssuingAuthority_StatusHistoryService, Domain.Services.Core.RR_IssuingAuthority_StatusHistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IRR_IssuingAuthority_StatusHistoryValidation, Domain.Entities.Core.Validations.RR_IssuingAuthority_StatusHistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEnablingObjective_TopicHistoryService, Domain.Services.Core.EnablingObjective_TopicHistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEnablingObjective_TopicHistoryValidation, Domain.Entities.Core.Validations.EnablingObjective_TopicHistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEnablingObjective_SubCategoryHistoryService, Domain.Services.Core.EnablingObjective_SubCategoryHistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEnablingObjective_SubCategoryHistoryValidation, Domain.Entities.Core.Validations.EnablingObjective_SubCategoryHistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEnablingObjectiveHistoryService, Domain.Services.Core.EnablingObjectiveHistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEnablingObjectiveHistoryValidation, Domain.Entities.Core.Validations.EnablingObjectiveHistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IILAHistoryService, Domain.Services.Core.ILAHistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILAHistoryValidation, Domain.Entities.Core.Validations.ILAHistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IToolCategoryService, Domain.Services.Core.ToolCategoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IToolCategoryValidation, Domain.Entities.Core.Validations.ToolCategoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISafetyHazard_Tool_LinkService, Domain.Services.Core.SafetyHazard_Tool_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISafetyHazard_Tool_LinkValidation, Domain.Entities.Core.Validations.SafetyHazard_Tool_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITool_StatusHistoryService, Domain.Services.Core.Tool_StatusHistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITool_StatusHistoryValidation, Domain.Entities.Core.Validations.Tool_StatusHistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructor_Service, Domain.Services.Core.Instructor_Service>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructor_Validation, Domain.Entities.Core.Validations.InstructorValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructor_CategoryService, Domain.Services.Core.Instructor_CategoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructor_CategoryValidation, Domain.Entities.Core.Validations.Instructor_CategoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructor_HistoryService, Domain.Services.Core.Instructor_HistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructor_HistoryValidation, Domain.Entities.Core.Validations.Instructor_HistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructor_CategoryHistoryService, Domain.Services.Core.Instructor_CategoryHistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructor_CategoryHistoryValidation, Domain.Entities.Core.Validations.Instructor_CategoryHistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITask_HistoryService, Domain.Services.Core.Task_HistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITask_HistoryValidation, Domain.Entities.Core.Validations.Task_HistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ILocation_Service, Domain.Services.Core.Location_Service>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ILocation_Validation, Domain.Entities.Core.Validations.LocationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ILocation_CategoryService, Domain.Services.Core.Location_CategoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ILocation_CategoryValidation, Domain.Entities.Core.Validations.Location_CategoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ILocation_HistoryService, Domain.Services.Core.Location_HistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ILocation_HistoryValidation, Domain.Entities.Core.Validations.Location_HistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ILocation_CategoryHistoryService, Domain.Services.Core.Location_CategoryHistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ILocation_CategoryHistoryValidation, Domain.Entities.Core.Validations.Location_CategoryHistoryValidation>();


            services.AddTransient<Domain.Interfaces.Service.Core.ITask_MetaTask_LinkService, Domain.Services.Core.Task_MetaTask_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITask_MetaTask_LinkValidation, Domain.Entities.Core.Validations.Task_MetaTask_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITask_SuggestionService, Domain.Services.Core.Task_SuggestionService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITask_SuggestionValidation, Domain.Entities.Core.Validations.Task_SuggestionValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITrainingGroup_CategoryService, Domain.Services.Core.TrainingGroup_CategoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITrainingGroup_CategoryValidation, Domain.Entities.Core.Validations.TrainingGroup_CategoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITrainingGroupService, Domain.Services.Core.TrainingGroupService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITrainingGroupValidation, Domain.Entities.Core.Validations.TrainingGroupValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITask_TrainingGroupService, Domain.Services.Core.Task_TrainingGroupService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITask_TrainingGroupValidation, Domain.Entities.Core.Validations.Task_TrainingGroupValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_ILAService, Domain.Services.Core.Version_ILAService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_ILAValidation, Domain.Entities.Core.Validations.Version_ILAValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_Task_ILA_LinkService, Domain.Services.Core.Version_Task_ILA_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_Task_ILA_LinkValidation, Domain.Entities.Core.Validations.Version_Task_ILA_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_RegulatoryRequirementService, Domain.Services.Core.Version_RegulatoryRequirementService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_RegulatoryRequirementValidation, Domain.Entities.Core.Validations.Version_RegulatoryRequirementValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_Task_RR_LinkService, Domain.Services.Core.Version_Task_RR_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_Task_RR_LinkValidation, Domain.Entities.Core.Validations.Version_Task_RR_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_TrainingGroupService, Domain.Services.Core.Version_TrainingGroupService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_TrainingGroupValidation, Domain.Entities.Core.Validations.Version_TrainingGroupValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_Task_TrainingGroupService, Domain.Services.Core.Version_Task_TrainingGroupService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_Task_TrainingGroupValidation, Domain.Entities.Core.Validations.Version_Task_TrainingGroupValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IPositionHistoryService, Domain.Services.Core.PositionHistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IPositionHistoryValidation, Domain.Entities.Core.Validations.PositionHistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IPosition_TaskService, Domain.Services.Core.Position_TaskService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IPosition_TaskValidation, Domain.Entities.Core.Validations.Position_TaskValidation>();


            services.AddTransient<Domain.Interfaces.Service.Core.IPosition_SQService, Domain.Services.Core.Position_SQService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IPositions_SQValidation, Domain.Entities.Core.Validations.Position_SQValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IPosition_EmployeeService, Domain.Services.Core.Position_EmployeeService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IPosition_EmployeeValidation, Domain.Entities.Core.Validations.Position_EmployeeValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEnablingObjective_MetaEO_LinkService, Domain.Services.Core.EnablingObjective_MetaEO_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEnablingObjective_MetaEO_LinkValidation, Domain.Entities.Core.Validations.EnablingObjective_MetaEO_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITestItem_HistoryService, Domain.Services.Core.TestItem_HistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITestItem_HistoryValidation, Domain.Entities.Core.Validations.TestItem_HistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_EnablingObjective_TaskService, Domain.Services.Core.Version_EnablingObjective_TaskService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_EnablingObjective_TaskValidation, Domain.Entities.Core.Validations.Version_EnablingObjective_TaskValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_EnablingObjective_ILALinkService, Domain.Services.Core.Version_EnablingObjective_ILALinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_EnablingObjective_ILALinkValidation, Domain.Entities.Core.Validations.Version_EnablingObjective_ILALinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_EnablingObjective_RRLinkService, Domain.Services.Core.Version_EnablingObjective_RRLinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_EnablingObjective_RRLinkValidation, Domain.Entities.Core.Validations.Version_EnablingObjective_RRLinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_EnablingObjective_MetaEOLinkService, Domain.Services.Core.Version_EnablingObjective_MetaEOLinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_EnablingObjective_MetaEOLinkValidation, Domain.Entities.Core.Validations.Version_EnablingObjective_MetaEOLinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEnablingObjective_StepService, Domain.Services.Core.EnablingObjective_StepService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEnablingObjective_StepValidation, Domain.Entities.Core.Validations.EnablingObjective_StepValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEnablingObjective_Employee_LinkService, Domain.Services.Core.EnablingObjective_Employee_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEnablingObjective_Employee_LinkValidation, Domain.Entities.Core.Validations.EnablingObjective_Employee_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_PositionService, Domain.Services.Core.Version_PositionService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_PositionValidation, Domain.Entities.Core.Validations.Version_PositionValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_EnablingObjective_Position_LinkService, Domain.Services.Core.Version_EnablingObjective_Position_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_EnablingObjective_Position_LinkValidation, Domain.Entities.Core.Validations.Version_EnablingObjective_Position_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_EmployeeService, Domain.Services.Core.Version_EmployeeService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_EmployeeValidation, Domain.Entities.Core.Validations.Version_EmployeeValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_EnablingObjective_Employee_LinkService, Domain.Services.Core.Version_EnablingObjective_Employee_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_EnablingObjective_Employee_LinkValidation, Domain.Entities.Core.Validations.Version_EnablingObjective_Employee_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEnablingObjective_QuestionService, Domain.Services.Core.EnablingObjective_QuestionService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEnablingObjective_QuestionValidation, Domain.Entities.Core.Validations.EnablingObjective_QuestionValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_EnablingObjective_QuestionService, Domain.Services.Core.Version_EnablingObjective_QuestionService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_EnablingObjective_QuestionValidation, Domain.Entities.Core.Validations.Version_EnablingObjective_QuestionValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_TestItemsService, Domain.Services.Core.Version_TestItemsService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_TestItemsValidation, Domain.Entities.Core.Validations.Version_TestItemsValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_Task_SuggestionService, Domain.Services.Core.Version_Task_SuggestionService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_Task_SuggestionValidation, Domain.Entities.Core.Validations.Version_Task_SuggestionValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_Task_Position_LinkService, Domain.Services.Core.Version_Task_Position_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_Task_Position_LinkValidation, Domain.Entities.Core.Validations.Version_Task_Position_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_EnablingObjective_StepService, Domain.Services.Core.Version_EnablingObjective_StepService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_EnablingObjective_StepValidation, Domain.Entities.Core.Validations.Version_EnablingObjective_StepValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEnablingObjective_SuggestionService, Domain.Services.Core.EnablingObjective_SuggestionService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEnablingObjective_SuggestionValidation, Domain.Entities.Core.Validations.EnablingObjective_SuggestionValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEnablingObjective_ToolService, Domain.Services.Core.EnablingObjective_ToolService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEnablingObjective_ToolValidation, Domain.Entities.Core.Validations.EnablingObjective_ToolValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_EnablingObjective_SuggestionsService, Domain.Services.Core.Version_EnablingObjective_SuggestionsService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_EnablingObjective_SuggestionsValidation, Domain.Entities.Core.Validations.Version_EnablingObjective_SuggestionsValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEmployeeCertificationHistoryService, Domain.Services.Core.EmployeeCertificationHistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEmployeeCertificationHistoryValidation, Domain.Entities.Core.Validations.EmployeeCertificationHistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ICertificationIssuingAuthorityService, Domain.Services.Core.CertificationIssuingAuthorityService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ICertificationIssuingAuthorityValidation, Domain.Entities.Core.Validations.CertificationIssuingAuthorityValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IActivityNotificationService, Domain.Services.Core.ActivityNotificationService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IActivityNotificationValidation, Domain.Entities.Core.Validations.ActivityNotificationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IIDPScheduleService, Domain.Services.Core.IDPScheduleService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IIDPScheduleValidation, Domain.Entities.Core.Validations.IDPScheduleValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEmployeeDocumentService, Domain.Services.Core.EmployeeDocumentService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEmployeeDocumentValidation, Domain.Entities.Core.Validations.EmployeeDocumentValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEmployeeHistoryService, Domain.Services.Core.EmployeeHistoryServicecs>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEmployeeHistoryValidation, Domain.Entities.Core.Validations.EmployeeHistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClientSettings_NotificationService, Domain.Services.Core.ClientSettings_NotificationService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClientSettings_NotificationValidation, Domain.Entities.Core.Validations.ClientSettings_NotificationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClientUserSettings_GeneralSettingService, Domain.Services.Core.ClientUserSettings_GeneralSettingService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClientUserSettings_GeneralSettingValidation, Domain.Entities.Core.Validations.ClientUserSettings_GeneralSettingValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITrainingProgramTypeService, Domain.Services.Core.TrainingProgramTypeService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITrainingProgramTypeValidation, Domain.Entities.Core.Validations.TrainingProgramTypeValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITrainingProgram_HistoryService, Domain.Services.Core.TrainingProgram_HistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITrainingProgram_HistoryValidation, Domain.Entities.Core.Validations.TrainingProgram_HistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITrainingPrograms_ILA_LinkService, Domain.Services.Core.TrainingPrograms_ILA_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITrainingProgram_ILA_LinkValidation, Domain.Entities.Core.Validations.TrainingPrograms_ILA_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_TrainingProgramService, Domain.Services.Core.Version_TrainingProgramService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_TrainingProgram, Domain.Entities.Core.Validations.Version_TrainingProgramValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_TrainingProgram_ILA_LinkService, Domain.Services.Core.Version_TrainingProgram_ILA_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_TrainingProgram_ILA_LinkValidation, Domain.Entities.Core.Validations.Version_TrainingProgram_ILA_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IRatingScaleNService, Domain.Services.Core.RatingScaleNService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IRatingScaleNValidation, Domain.Entities.Core.Validations.RatingScaleNValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IStudentEvaluationService, Domain.Services.Core.StudentEvaluationService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IStudentEvaluationValidation, Domain.Entities.Core.Validations.StudentEvaluationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IStudentEvaluationHistoryService, Domain.Services.Core.StudentEvaluationHistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IStudentEvaluationHistoryValidation, Domain.Entities.Core.Validations.StudentEvaluationHistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IQuestionBankService, Domain.Services.Core.QuestionBankService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IQuestionBankValidation, Domain.Entities.Core.Validations.QuestionBankValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IQuestionBankHistoryService, Domain.Services.Core.QuestionBankHistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IQuestionBankHistoryValidation, Domain.Entities.Core.Validations.QuestionBankHistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IStudentEvaluation_QuestionService, Domain.Services.Core.StudentEvaluation_QuestionService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IStudentEvaluation_QuestionValidation, Domain.Entities.Core.Validations.StudentEvaluation_QuestionValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClassSchedule_Evaluation_RosterService, Domain.Services.Core.ClassSchedule_Evaluation_RosterService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClassSchedule_Evaluation_RosterValidation, Domain.Entities.Core.Validations.ClassSchedule_Evaluation_RosterValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IRatingScaleExpandedService, Domain.Services.Core.RatingScaleExpandedService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IRatingScaleExpandedValidation, Domain.Entities.Core.Validations.RatingScaleExpandedValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_TestStausService, Domain.Services.Core.Version_TestStausService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_TestStausValidation, Domain.Entities.Core.Validations.Version_TestStausValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_TestService, Domain.Services.Core.Version_TestService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_TestValidation, Domain.Entities.Core.Validations.Version_TestValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITest_HistoryService, Domain.Services.Core.Test_HistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITest_HistoryValidation, Domain.Entities.Core.Validations.Test_HistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IDutyArea_HistoryService, Domain.Services.Core.DutyArea_HistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IDutyArea_HistoryValidation, Domain.Entities.Core.Validations.DutyArea_HistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISubDutyArea_HistoryService, Domain.Services.Core.SubDutyArea_HistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISubDutyArea_HistoryValidation, Domain.Entities.Core.Validations.SubDutyArea_HistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEvaluationMethodService, Domain.Services.Core.EvaluationMethodService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEvaluationMethodValidation, Domain.Entities.Core.Validations.EvaluationMethodValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITaskQualificationService, Domain.Services.Core.TaskQualificationService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITaskQualificationValidation, Domain.Entities.Core.Validations.TaskQualificationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITaskQualification_Evaluator_LinkService, Domain.Services.Core.TaskQualification_Evaluator_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITaskQualification_Evaluator_LinkValidation, Domain.Entities.Core.Validations.TaskQualification_Evaluator_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITaskQualificationStatusService, Domain.Services.Core.TaskQualificationStatusService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITaskQualificationStatusValidation, Domain.Entities.Core.Validations.TaskQualificationStatusValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITQEmpSettingService, Domain.Services.Core.TQEmpSettingService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITQEmpSettingValidation, Domain.Entities.Core.Validations.TQEmpSettingValidation>();


            services.AddTransient<Domain.Interfaces.Service.Core.IClassScheduleService, Domain.Services.Core.ClassScheduleService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClassScheduleValidation, Domain.Entities.Core.Validations.ClassScheduleValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IPublicClassScheduleRequestService, Domain.Services.Core.PublicClassScheduleRequestService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IPublicClassScheduleRequestValidation, Domain.Entities.Core.Validations.PublicClassScheduleRequestValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClassScheduleHistoryService, Domain.Services.Core.ClassScheduleHistoryService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClassScheduleHistoryValidation, Domain.Entities.Core.Validations.ClassScheduleHistoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ICBTService, Domain.Services.Core.CBTService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ICBTValidation, Domain.Entities.Core.Validations.CBTReleaseEMPSettingsValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEvaluationReleaseEMPSettingsService, Domain.Services.Core.EvaluationReleaseEMPSettingsService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEvaluationReleaseEMPSettingsValidation, Domain.Entities.Core.Validations.EvaluationReleaseEMPSettingsValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITestReleaseEMPSettingsService, Domain.Services.Core.TestReleaseEMPSettingsService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITestReleaseEMPSettingsValidation, Domain.Entities.Core.Validations.TestReleaseEMPSettingsValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITestReleaseEMPSetting_Retake_LinkService, Domain.Services.Core.TestReleaseEMPSetting_Retake_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITestReleaseEMPSetting_Retake_LinkValidation, Domain.Entities.Core.Validations.TestReleaseEMPSetting_Retake_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISelfRegistrationOptionsService, Domain.Services.Core.SelfRegistrationOptionsService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISelfRegistrationOptionsValidation, Domain.Entities.Core.Validations.SelfRegistrationOptionsValidation>();


            services.AddTransient<Domain.Interfaces.Service.Core.IClassScheduleEmployeeService, Domain.Services.Core.ClassScheduleEmployeeService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClassSchedule_EmployeeValidation, Domain.Entities.Core.Validations.ClassScheduleEmployeeValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClassSchedule_RosterService, Domain.Services.Core.ClassSchedule_RosterService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClassSchedule_RosterValidation, Domain.Entities.Core.Validations.ClassSchedule_RosterValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITQILAEmpSettingService, Domain.Services.Core.TQILAEmpSettingService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITQILAEmpSettingValidation, Domain.Entities.Core.Validations.TQILAEmpSettingValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IILA_Evaluator_LinkService, Domain.Services.Core.ILA_Evaluator_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILA_Evaluator_LinkValidation, Domain.Entities.Core.Validations.ILA_Evaluator_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClassSchedule_Roster_StatusesService, Domain.Services.Core.ClassSchedule_Roster_StatusesService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClassSchedule_Roster_StatusesValidation, Domain.Entities.Core.Validations.ClassSchedule_Roster_StatusesValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClassSchedule_RecurrenceService, Domain.Services.Core.ClassSchedule_RecurrenceService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClassSchedule_RecurrenceValidation, Domain.Entities.Core.Validations.ClassSchedule_RecurrenceValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClassSchedule_StudentEvaluations_LinkService, Domain.Services.Core.ClassSchedule_StudentEvaluations_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClassSchedule_StudentEvaluations_LinkValidation, Domain.Entities.Core.Validations.ClassSchedule_StudentEvaluations_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IStudentEvaluationWithoutEmpService, Domain.Services.Core.StudentEvaluationWithoutEmpService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IStudentEvaluationWithoutEmpValidation, Domain.Entities.Core.Validations.StudentEvaluationWithoutEmpValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IIDPService, Domain.Services.Core.IDPService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IIDPValidation, Domain.Entities.Core.Validations.IDPValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IIDP_ReviewService, Domain.Services.Core.IDP_ReviewService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IIDP_ReviewValidation, Domain.Entities.Core.Validations.IDP_ReviewValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IIDP_ReviewStatusService, Domain.Services.Core.IDP_ReviewStatusService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IIDP_ReviewStatusValidation, Domain.Entities.Core.Validations.IDP_ReviewStatusValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClientUserSettings_Dashboard_SettingService, Domain.Services.Core.ClientUserSettings_Dashboard_SettingService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClientUserSettings_Dashboard_SettingValidation, Domain.Entities.Core.Validations.ClientUserSettings_Dashboard_SettingValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IDashboard_SettingService, Domain.Services.Core.Dashboard_SettingService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IDashboardSettingValidation, Domain.Entities.Core.Validations.DashboardSettingValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClientUserSettings_GeneralSettingService, Domain.Services.Core.ClientUserSettings_GeneralSettingService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClientUserSettings_GeneralSettingValidation, Domain.Entities.Core.Validations.ClientUserSettings_GeneralSettingValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.INotificationService, Domain.Services.Core.NotificationService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.INotificationValidation, Domain.Entities.Core.Validations.NotificationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClientSettings_LabelReplacementsService, Domain.Services.Core.ClientSettings_LabelReplacementsService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClientUserSettings_LabelReplacementValidation, Domain.Entities.Core.Validations.ClientUserSettings_LabelReplacementValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClientSettings_LicenseService, Domain.Services.Core.ClientSettings_LicenseService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClientUserSettings_LicenseValidation, Domain.Entities.Core.Validations.ClientUserSettings_LicenseValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEmpTestService, Domain.Services.Core.EmpTestService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEmpTestValidation, Domain.Entities.Core.Validations.EmpTestValidation>();


            services.AddTransient<Domain.Interfaces.Service.Core.IRatingScaleExpandedService, Domain.Services.Core.RatingScaleExpandedService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IRatingScaleExpandedValidation, Domain.Entities.Core.Validations.RatingScaleExpandedValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_DesignDefaultViewService, Domain.Services.Core.InstructorWorkbook_DesignDefaultViewService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_DesignDefaultViewValidation, Domain.Entities.Core.Validations.InstructorWorkbook_DesignDefaultViewValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILA_DesignService, Domain.Services.Core.InstructorWorkbook_ILA_DesignService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILA_DesignValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILA_DesignValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILA_DevelopService, Domain.Services.Core.InstructorWorkbook_ILA_DevelopService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILA_DevelopValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILA_DevelopValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILA_ImplementService, Domain.Services.Core.InstructorWorkbook_ILA_ImplementService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILA_ImplementValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILA_ImplementValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILADesign_DelieveryMethodsService, Domain.Services.Core.InstructorWorkbook_ILADesign_DelieveryMethodsService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILADesign_DelieveryMethodsValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILADesign_DelieveryMethodsvalidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILADesign_EnablingObjectivesService, Domain.Services.Core.InstructorWorkbook_ILADesign_EnablingObjectivesService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILADesign_EnablingObjectivesValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILADesign_EnablingObjectivesValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILADesign_NERCService, Domain.Services.Core.InstructorWorkbook_ILADesign_NERCService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILADesign_NERCValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILADesign_NERCValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILADesign_PrerequistieService, Domain.Services.Core.InstructorWorkbook_ILADesign_PrerequistieService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILADesign_PrerequistieValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILADesign_PrerequistieValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILADesign_ProceduresService, Domain.Services.Core.InstructorWorkbook_ILADesign_ProceduresService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILADesign_ProceduresValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILADesign_ProceduresValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILADesign_ResourcesService, Domain.Services.Core.InstructorWorkbook_ILADesign_ResourcesService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILADesign_ResourcesValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILADesign_ResourcesValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILADesign_ResourcesService, Domain.Services.Core.InstructorWorkbook_ILADesign_ResourcesService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILADesign_ResourcesValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILADesign_ResourcesValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILADesign_SafetyHazardsService, Domain.Services.Core.InstructorWorkbook_ILADesign_SafetyHazardsService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILADesign_SafetyHazardsValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILADesign_SafetyHazardsValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILADesign_SegmentsService, Domain.Services.Core.InstructorWorkbook_ILADesign_SegmentsService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILADesign_SegmentsValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILADesign_SegmentsValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILADesign_TargetAudienceService, Domain.Services.Core.InstructorWorkbook_ILADesign_TargetAudienceService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILADesign_TargetAudienceValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILADesign_TargetAudienceValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILADesign_TasksService, Domain.Services.Core.InstructorWorkbook_ILADesign_TasksService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILADesign_TasksValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILADesign_TasksValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILADesign_TrainingTopicsService, Domain.Services.Core.InstructorWorkbook_ILADesign_TrainingTopicsService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILADesign_TrainingTopicsValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILADesign_TrainingTopicsValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILADesignReviewersService, Domain.Services.Core.InstructorWorkbook_ILADesignReviewersService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILADesignReviewersValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILADesignReviewersvalidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILADevelopReviewersService, Domain.Services.Core.InstructorWorkbook_ILADevelopReviewersService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILADevelopReviewersValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILADevelopReviewersValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILAEvaluationService, Domain.Services.Core.InstructorWorkbook_ILAEvaluationService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILAEvaluationValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILAEvaluationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILAEvaluation_DefaultViewService, Domain.Services.Core.InstructorWorkbook_ILAEvaluation_DefaultViewService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILAEvaluation_DefaultViewValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILAEvaluation_DefaultViewValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILAEvaluation_TestAnalysisService, Domain.Services.Core.InstructorWorkbook_ILAEvaluation_TestAnalysisService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILAEvaluation_TestAnalysisValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILAEvaluation_TestAnalysisValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILAEvaluation_TrainingIssuesService, Domain.Services.Core.InstructorWorkbook_ILAEvaluation_TrainingIssuesService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILAEvaluation_TrainingIssuesValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILAEvaluation_TrainingIssuesValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILAImplement_ClassScheduleService, Domain.Services.Core.InstructorWorkbook_ILAImplement_ClassScheduleService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILAImplement_ClassScheduleValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILAImplement_ClassScheduleValidation>();


            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILAImplementReviewersService, Domain.Services.Core.InstructorWorkbook_ILAImplementReviewersService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILAImplementReviewersValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILAImplementReviewersValidation>();


            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ILAPhasesService, Domain.Services.Core.InstructorWorkbook_ILAPhasesService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ILAPhasesValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ILAPhasesValidation>();


            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_PhasesService, Domain.Services.Core.InstructorWorkbook_PhasesService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_PhasesValidation, Domain.Entities.Core.Validations.InstructorWorkbook_PhasesValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ProspectiveILAService, Domain.Services.Core.InstructorWorkbook_ProspectiveILAService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ProspectiveILAValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ProspectiveILAValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ProspectiveILA_ArchivesService, Domain.Services.Core.InstructorWorkbook_ProspectiveILA_ArchivesService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ProspectiveILA_ArchivesValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ProspectiveILA_ArchivesValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_ProspectiveILA_SnapshotService, Domain.Services.Core.InstructorWorkbook_ProspectiveILA_SnapshotService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_ProspectiveILA_SnapshotValidation, Domain.Entities.Core.Validations.InstructorWorkbook_ProspectiveILA_SnapshotValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_Segments_LinkObjectivesService, Domain.Services.Core.InstructorWorkbook_Segments_LinkObjectivesService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_Segments_LinkObjectivesValidation, Domain.Entities.Core.Validations.InstructorWorkbook_Segments_LinkObjectivesValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_Segments_NercStandardsService, Domain.Services.Core.InstructorWorkbook_Segments_NercStandardsService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_Segments_NercStandardsValidation, Domain.Entities.Core.Validations.InstructorWorkbook_Segments_NercStandardsValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_TrainingTopicsService, Domain.Services.Core.InstructorWorkbook_TrainingTopicsService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_TrainingTopicsValidation, Domain.Entities.Core.Validations.InstructorWorkbook_TrainingTopicsValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IInstructorWorkbook_TrainingTopicsHeadingService, Domain.Services.Core.InstructorWorkbook_TrainingTopicsHeadingService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IInstructorWorkbook_TrainingTopicsHeadingValidation, Domain.Entities.Core.Validations.InstructorWorkbook_TrainingTopicsHeadingValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IDIFSurveyService, Domain.Services.Core.DIFSurveyService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IDIFSurveyValidation, Domain.Entities.Core.Validations.DIFSurveyValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IDIFSurvey_DevStatusService, Domain.Services.Core.DIFSurvey_DevStatusService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IDIFSurvey_DevStatusValidation, Domain.Entities.Core.Validations.DIFSurvery_DevStatusValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IDIFSurvey_EmployeeService, Domain.Services.Core.DIFSurvey_EmployeeService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IDIFSurvey_EmployeeValidation, Domain.Entities.Core.Validations.DIFSurvey_EmployeeValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IDIFSurvey_Employee_StatusService, Domain.Services.Core.DIFSurvey_Employee_StatusService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IDIFSurvey_Employee_StatusValidation, Domain.Entities.Core.Validations.DIFSurvey_Employee_StatusValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IDIFSurvey_Employee_ResponseService, Domain.Services.Core.DIFSurvey_Employee_ResponseService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IDIFSurvey_Employee_ResponseValidation, Domain.Entities.Core.Validations.DIFSurvey_Employee_ResponseValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IDIFSurvey_TaskService, Domain.Services.Core.DIFSurvey_TaskService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IDIFSurvey_TaskValidation, Domain.Entities.Core.Validations.DIFSurvey_TaskValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IDIFSurvey_Task_StatusService, Domain.Services.Core.DIFSurvey_Task_StatusService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IDIFSurvey_Task_StatusValidation, Domain.Entities.Core.Validations.DIFSurvey_Task_StatusValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IDIFSurvey_Task_TrainingFrequencyService, Domain.Services.Core.DIFSurvey_Task_TrainingFrequencyService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IDIFSurvey_Task_TrainingFrequencyValidation, Domain.Entities.Core.Validations.DIFSurvey_Task_TrainingFrequencyValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISimulatorScenarioService, Domain.Services.Core.SimulatorScenarioService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISimulatorScenarioValidation, Domain.Entities.Core.Validations.SimulatorScenarioValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISimulatorScenario_DifficultyService, Domain.Services.Core.SimulatorScenario_DifficultyService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISimulatorScenario_DifficultyValidation, Domain.Entities.Core.Validations.SimulatorScenario_DifficultyValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISimulatorScenario_PositionService, Domain.Services.Core.SimulatorScenario_PositionService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISimulatorScenario_PositonValidation, Domain.Entities.Core.Validations.SimulatorScenario_PositonValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISimulatorScenario_TaskService, Domain.Services.Core.SimulatorScenario_TaskService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISimulatorScenario_TaskValidation, Domain.Entities.Core.Validations.SimulatorScenario_TaskValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISimulatorScenario_EnablingObjectiveService, Domain.Services.Core.SimulatorScenario_EnablingObjectiveService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISimulatorScenario_EnablingObjectiveValidation, Domain.Entities.Core.Validations.SimulatorScenario_EnablingObjectiveValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISimulatorScenario_ProcedureService, Domain.Services.Core.SimulatorScenario_ProcedureService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISimulatorScenario_ProcedureValidation, Domain.Entities.Core.Validations.SimulatorScenario_ProcedureValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISimulatorScenario_Task_CriteriaService, Domain.Services.Core.SimulatorScenario_Task_CriteriaService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISimulatorScenario_Task_CriteriaValidation, Domain.Entities.Core.Validations.SimulatorScenario_Task_CriteriaValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISimulatorScenario_PrerequisiteService, Domain.Services.Core.SimulatorScenario_PrerequisiteService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISimulatorScenario_PrerequisiteValidation, Domain.Entities.Core.Validations.SimulatorScenario_PrerequisiteValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISimulatorScenario_CollaboratorService, Domain.Services.Core.SimulatorScenario_CollaboratorService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISimulatorScenario_CollaboratorValidation, Domain.Entities.Core.Validations.SimulatorScenario_CollaboratorValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISimulatorScenario_CollaboratorPermissionService, Domain.Services.Core.SimulatorScenario_CollaboratorPermissionService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISimulatorScenario_CollaboratorPermissionValidation, Domain.Entities.Core.Validations.SimulatorScenario_CollaboratorPermissionValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISimulatorScenario_StatusService, Domain.Services.Core.SimulatorScenario_StatusService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISimulatorScenario_StatusValidation, Domain.Entities.Core.Validations.SimulatorScenario_StatusValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISimulatorScenario_ILAService, Domain.Services.Core.SimulatorScenario_ILAService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISimulatorScenario_ILAValidation, Domain.Entities.Core.Validations.SimulatorScenario_ILAValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClassSchedule_TQEMPSettingsService, Domain.Services.Core.ClassSchedule_TQEMPSettingsService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClassSchedule_TQEMPSettingsValidation, Domain.Entities.Core.Validations.ClassSchedule_TQEMPSettingsValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClassSchedule_Evaluator_LinksService, Domain.Services.Core.ClassSchedule_Evaluator_LinksService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClassSchedule_Evaluator_LinksValidation, Domain.Entities.Core.Validations.ClassSchedule_Evaluator_LinksValidation>();

            // ------------------ Domain Service and Validations DI End ---------------------  //

            services.AddTransient<Domain.Interfaces.Service.Core.IReportService, Domain.Services.Core.ReportService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IReportValidation, Domain.Entities.Core.Validations.ReportValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IReportSkeletonService, Domain.Services.Core.ReportSkeletonService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IReportSkeletonValidation, Domain.Entities.Core.Validations.ReportSkeletonValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IReportSkeletonCategoriesService, Domain.Services.Core.ReportSkeletonCategoriesService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IReportSkeletonCategoryValidation, Domain.Entities.Core.Validations.ReportSkeletonCategoryValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IRatingScaleExpandedService, Domain.Services.Core.RatingScaleExpandedService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IRatingScaleExpandedValidation, Domain.Entities.Core.Validations.RatingScaleExpandedValidation>();

            // ------------------ Domain Service and Validations DI End ---------------------  //
            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_Task_MetaTask_LinkService, Domain.Services.Core.Version_Task_MetaTask_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_Task_MetaTask_LinkValidation, Domain.Entities.Core.Validations.Version_Task_MetaTask_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IMetaILAConfigurationPublishOptionService, Domain.Services.Core.MetaILAConfigurationPublishOptionService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IMetaILAConfigurationPublishOptionValidation, Domain.Entities.Core.Validations.MetaILAConfigurationPublishOptionValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IVersion_MetaILAService, Domain.Services.Core.Version_MetaILAService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IVersion_MetaILAValidation, Domain.Entities.Core.Validations.Version_MetaILAValidation>();

            services.AddTransient<Domain.Interfaces.Validation.Core.IMetaILA_EmployeeValidation, Domain.Entities.Core.Validations.MetaILA_EmployeeValidation>();
            services.AddTransient<Domain.Interfaces.Service.Core.IMetaILA_EmployeeService, Domain.Services.Core.MetaILA_EmployeeService>();
            services.AddTransient<Domain.Interfaces.Service.Core.IMetaILA_StatusService, Domain.Services.Core.MetaILA_StatusService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IMetaILA_StatusValidation, Domain.Entities.Core.Validations.MetaILA_StatusValidation>();
            services.AddTransient<Domain.Interfaces.Service.Core.IILA_PerformTraineeEvalService, Domain.Services.Core.ILA_PerformTraineeEvalService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILA_PerformTraineeEvalValidation, Domain.Entities.Core.Validations.ILA_PerformTraineeEvalValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ICertificationSubRequirementService, Domain.Services.Core.CertificationSubRequirementService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ICertificationSubRequirementValidation, Domain.Entities.Core.Validations.CertificationSubRequirementValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ICBT_ScormRegistrationService, Domain.Services.Core.CBT_ScormRegistrationService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ICBT_ScormRegistrationValidation, Domain.Entities.Core.Validations.CBT_ScormRegistrationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IMetaILA_SummaryTestService, Domain.Services.Core.MetaILA_SummaryTestService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IMetaILA_SummaryTestValidation, Domain.Entities.Core.Validations.MetaILA_SummaryTestValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IMetaILA_Employee_MemberLinkFufillmentService, Domain.Services.Core.MetaILA_Employee_MemberLinkFufillmentService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IMetaILA_Employee_MemberLinkFufillmentValidation, Domain.Entities.Core.Validations.MetaILA_Employee_MemberLinkFufillmentValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITrainingProgramReviewService, Domain.Services.Core.TrainingProgramReviewService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITrainingProgramReviewsValidation, Domain.Entities.Core.Validations.TrainingProgramReviewsValidation>();


            services.AddTransient<Domain.Interfaces.Service.Core.IILA_Topic_LinkService, Domain.Services.Core.ILA_Topic_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IILA_Topic_LinkValidation, Domain.Entities.Core.Validations.ILA_Topic_LinkValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClassSchedule_Roster_ResponseService, Domain.Services.Core.ClassSchedule_Roster_ResponseService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClassSchedule_Roster_ResponseValidation, Domain.Entities.Core.Validations.ClassSchedule_Roster_ResponseValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClientSettings_FeatureService, Domain.Services.Core.ClientSettings_FeatureService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClientSettings_FeatureValidation, Domain.Entities.Core.Validations.ClientSettings_FeatureValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IEmpSettingsReleaseTypeService, Domain.Services.Core.EmpSettingsReleaseTypeService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IEmpSettingsReleaseTypeValidation, Domain.Entities.Core.Validations.EmpSettingsReleaseTypeValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITaskListReviewService, Domain.Services.Core.TaskListReviewService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITaskListReviewValidation, Domain.Entities.Core.Validations.TaskListReviewValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITaskListReview_GeneralReviewerService, Domain.Services.Core.TaskListReview_GeneralReviewerService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITaskListReview_GeneralReviewerValidation, Domain.Entities.Core.Validations.TaskListReview_GeneralReviewerValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITaskListReview_TypeService, Domain.Services.Core.TaskListReview_TypeService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITaskListReview_TypeValidation, Domain.Entities.Core.Validations.TaskListReview_TypeValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITaskListReview_StatusService, Domain.Services.Core.TaskListReview_StatusService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITaskListReview_StatusValidation, Domain.Entities.Core.Validations.TaskListReview_StatusValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITaskReviewService, Domain.Services.Core.TaskReviewService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITaskReviewValidation, Domain.Entities.Core.Validations.TaskReviewValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITaskReview_StatusService, Domain.Services.Core.TaskReview_StatusService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITaskReview_StatusValidation, Domain.Entities.Core.Validations.TaskReview_StatusValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITaskReview_ReviewerService, Domain.Services.Core.TaskReview_ReviewerService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITaskReview_ReviewerValidation, Domain.Entities.Core.Validations.TaskReview_ReviewerValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITaskReview_FindingService, Domain.Services.Core.TaskReview_FindingService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITaskReview_FindingValidation, Domain.Entities.Core.Validations.TaskReview_FindingValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IActionItemService, Domain.Services.Core.ActionItemService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IActionItemValidation, Domain.Entities.Core.Validations.ActionItemValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IActionItem_PriorityService, Domain.Services.Core.ActionItem_PriorityService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IActionItem_PriorityValidation, Domain.Entities.Core.Validations.ActionItem_PriorityValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IActionItem_SubDuty_OperationService, Domain.Services.Core.ActionItem_SubDuty_OperationService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IActionItem_SubDuty_OperationValidation, Domain.Entities.Core.Validations.ActionItem_SubDuty_OperationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IActionItem_Step_OperationService, Domain.Services.Core.ActionItem_Step_OperationService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IActionItem_Step_OperationValidation, Domain.Entities.Core.Validations.ActionItem_Step_OperationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IActionItem_QuestionAndAnswer_OperationService, Domain.Services.Core.ActionItem_QuestionAndAnswer_OperationService>(); services.AddTransient<Domain.Interfaces.Validation.Core.IActionItem_QuestionAndAnswer_OperationValidation, Domain.Entities.Core.Validations.ActionItem_QuestionAndAnswer_OperationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IActionItem_Suggestion_OperationService, Domain.Services.Core.ActionItem_Suggestion_OperationService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IActionItem_Suggestion_Operationvalidation, Domain.Entities.Core.Validations.ActionItem_Suggestion_OperationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IActionItem_Tool_OperationService, Domain.Services.Core.ActionItem_Tool_OperationService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IActionItem_Tool_OperationValidation, Domain.Entities.Core.Validations.ActionItem_Tool_OperationValidation>();


            services.AddTransient<Domain.Interfaces.Service.Core.IActionItem_EnablingObjective_OperationService, Domain.Services.Core.ActionItem_EnablingObjective_OperationService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IActionItem_EnablingObjective_OperationValidation, Domain.Entities.Core.Validations.ActionItem_EnablingObjective_OperationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IActionItem_Procedure_OperationService, Domain.Services.Core.ActionItem_Procedure_OperationService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IActionItem_Procedure_OperationValidation, Domain.Entities.Core.Validations.ActionItem_Procedure_OperationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IActionItem_RegulatoryRequirement_OperationService, Domain.Services.Core.ActionItem_RegulatoryRequirement_OperationService>(); services.AddTransient<Domain.Interfaces.Validation.Core.IActionItem_RegulatoryRequirement_OperationValidation, Domain.Entities.Core.Validations.ActionItem_RegulatoryRequirement_OperationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IActionItem_SafetyHazard_OperationService, Domain.Services.Core.ActionItem_SafetyHazard_OperationService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IActionItem_SafetyHazard_OperationValidation, Domain.Entities.Core.Validations.ActionItem_SafetyHazard_OperationValidation>();


            services.AddTransient<Domain.Interfaces.Service.Core.IActionItem_OperationType_LinksService, Domain.Services.Core.ActionItem_OperationType_LinksService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IActionItem_OperationType_LinksValidation, Domain.Entities.Core.Validations.ActionItem_OperationType_LinksValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IActionItem_OperationTypesService, Domain.Services.Core.ActionItem_OperationTypesService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IActionItem_OperationTypesValidation, Domain.Entities.Core.Validations.ActionItem_OperationTypesValidation>();

            services.AddTransient<Domain.Certifications.Interfaces.ICertificationCalculatorRequestRecordHelper, Domain.Certifications.Implimentations.CertificationCalculatorRequestRecordHelper>();

            services.AddTransient<Domain.Certifications.Implimentations.FulfillmentCalculators.NercCertificationFulfillmentCalculator>();
            services.AddTransient<Domain.Certifications.Implimentations.FulfillmentCalculators.BasicCertificationFulfillmentCalculator>();
            services.AddTransient<Domain.Certifications.Implimentations.FulfillmentCalculators.EmergencyResponseCertificationFulfillmentCalculator>();

            services.AddTransient<Domain.Certifications.Factories.Interfaces.ICertificationFulfillmentCalculatorFactory, Domain.Certifications.Factories.Implimentations.CertificationFulfillmentCalculatorFactory>();
           
            services.AddTransient<Domain.Interfaces.Service.Core.ITrainingIssueService, Domain.Services.Core.TrainingIssueService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITrainingIssueValidation, Domain.Entities.Core.Validations.TrainingIssueValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITrainingIssue_StatusService, Domain.Services.Core.TrainingIssue_StatusService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITrainingIssue_Status_Validation, Domain.Entities.Core.Validations.TrainingIssue_StatusValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITrainingIssue_SeverityService, Domain.Services.Core.TrainingIssue_SeverityService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITrainingIssue_Severity_Validation, Domain.Entities.Core.Validations.TrainingIssue_SeverityValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITrainingIssue_DriverTypeService, Domain.Services.Core.TrainingIssue_DriverTypeService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITrainingIssue_DriverType_Validation, Domain.Entities.Core.Validations.TrainingIssue_DriverTypeValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITrainingIssue_DriverSubTypeService, Domain.Services.Core.TrainingIssue_DriverSubTypeService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITrainingIssue_DriverSubType_Validation, Domain.Entities.Core.Validations.TrainingIssue_DriverSubTypeValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITrainingIssue_DataElementService, Domain.Services.Core.TrainingIssue_DataElementService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITrainingIssue_DataElement_Validation, Domain.Entities.Core.Validations.TrainingIssue_DataElementValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITrainingIssue_ActionItemService, Domain.Services.Core.TrainingIssue_ActionItemService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITrainingIssue_ActionItem_Validation, Domain.Entities.Core.Validations.TrainingIssue_ActionItemValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITrainingIssue_ActionItemPriorityService, Domain.Services.Core.TrainingIssue_ActionItemPriorityService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITrainingIssue_ActionItemPriority_Validation, Domain.Entities.Core.Validations.TrainingIssue_ActionItemPriorityValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITrainingIssue_ActionItemStatusService, Domain.Services.Core.TrainingIssue_ActionItemStatusService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITrainingIssue_ActionItemStatus_Validation, Domain.Entities.Core.Validations.TrainingIssue_ActionItemStatusValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClassSchedule_Roster_TimeRecordService, Domain.Services.Core.ClassSchedule_Roster_TimeRecordService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClassSchedule_Roster_TimeRecordValidation, Domain.Entities.Core.Validations.ClassSchedule_Roster_TimeRecordValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClassSchedule_TestReleaseEMPSettingsService, Domain.Services.Core.ClassSchedule_TestReleaseEMPSettingsService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClassSchedule_TestReleaseEMPSettingsValidation, Domain.Entities.Core.Validations.ClassSchedule_TestReleaseEMPSettingsValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClassSchedule_TestReleaseEMPSetting_Retake_LinksService, Domain.Services.Core.ClassSchedule_TestReleaseEMPSetting_Retake_LinksService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClassSchedule_TestReleaseEMPSetting_Retake_LinksValidation, Domain.Entities.Core.Validations.ClassSchedule_TestReleaseEMPSetting_Retake_LinksValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClassSchedule_TQEMPSettingsService, Domain.Services.Core.ClassSchedule_TQEMPSettingsService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClassSchedule_TQEMPSettingsValidation, Domain.Entities.Core.Validations.ClassSchedule_TQEMPSettingsValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IClassSchedule_Evaluator_LinksService, Domain.Services.Core.ClassSchedule_Evaluator_LinksService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClassSchedule_Evaluator_LinksValidation, Domain.Entities.Core.Validations.ClassSchedule_Evaluator_LinksValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IPersonActivityNotificationService, Domain.Services.Core.PersonActivityNotificationService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IPersonActivityNotificationValidation, Domain.Entities.Core.Validations.PersonActivityNotificationValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ITrainingProgramReview_TrainingIssue_LinkService, Domain.Services.Core.TrainingProgramReview_TrainingIssue_LinkService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ITrainingProgramReview_TrainingIssue_LinkValidation, Domain.Entities.Core.Validations.TrainingProgramReview_TrainingIssue_LinkValidation>();
            services.AddTransient<Domain.Interfaces.Service.Core.IAdminMessageService, Domain.Services.Core.AdminMessageService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IAdminMessageValidation, Domain.Entities.Core.Validations.AdminMessageValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.IAdminMessage_QTDUserService, Domain.Services.Core.AdminMessage_QTDUserService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IAdminMessage_QTDUserValidation, Domain.Entities.Core.Validations.AdminMessage_QTDUserValidation>();
            
            services.AddTransient<Domain.Interfaces.Service.Core.IClassScheduleEmployee_ILACertificationLink_PartialCreditService, Domain.Services.Core.ClassScheduleEmployee_ILACertificationLink_PartialCreditService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClassScheduleEmployee_ILACertificationLink_PartialCreditValidation, Domain.Entities.Core.Validations.ClassScheduleEmployee_ILACertificationLink_PartialCreditValidation>();
            services.AddTransient<Domain.Interfaces.Service.Core.IClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditService, Domain.Services.Core.ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.IClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditValidation, Domain.Entities.Core.Validations.ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditValidation>();

            services.AddTransient<Domain.Interfaces.Validation.Core.ITaskListReview_PositionLinkValidation, Domain.Entities.Core.Validations.TaskListReview_PositionLinkValidation>();
            services.AddTransient<Domain.Interfaces.Service.Core.ITaskListReview_PositionLinkService, Domain.Services.Core.TaskListReview_PositionLinkService>();

            services.AddTransient<Domain.Interfaces.Validation.Core.ISkillQualificationValidation, Domain.Entities.Core.Validations.SkillQualificationValidation>();
            services.AddTransient<Domain.Interfaces.Service.Core.ISkillQualificationService, Domain.Services.Core.SkillQualificationService>();

            services.AddTransient<Domain.Interfaces.Validation.Core.ISkillQualificationStatusValidation, Domain.Entities.Core.Validations.SkillQualificationStatusValidation>();
            services.AddTransient<Domain.Interfaces.Service.Core.ISkillQualificationStatusService, Domain.Services.Core.SkillQualificationStatusService>();

            services.AddTransient<Domain.Interfaces.Validation.Core.ISkillQualification_Evaluator_LinkValidation, Domain.Entities.Core.Validations.SkillQualification_Evaluator_LinkValidation>();
            services.AddTransient<Domain.Interfaces.Service.Core.ISkillQualification_Evaluator_LinkService, Domain.Services.Core.SkillQualification_Evaluator_LinkService>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISkillQualificationEmpSettingService, Domain.Services.Core.SkillQualificationEmpSettingService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISkillQualificationEmpSettingValidation, Domain.Entities.Core.Validations.SkillQualificationEmpSettingValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISkillQualificationEmp_SignOffService, Domain.Services.Core.SkillQualificationEmp_SignOffService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISkillQualificationEmp_SignOffValidation, Domain.Entities.Core.Validations.SkillQualificationEmp_SignOffValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISkillReQualificationEmp_QuestionAnswerService, Domain.Services.Core.SkillReQualificationEmp_QuestionAnswerService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISkillReQualificationEmp_QuestionAnswerValidation, Domain.Entities.Core.Validations.SkillReQualificationEmp_QuestionAnswerValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISkillReQualificationEmp_SuggestionService, Domain.Services.Core.SkillReQualificationEmp_SuggestionService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISkillReQualificationEmp_SuggestionValidation, Domain.Entities.Core.Validations.SkillReQualificationEmp_SuggestionValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISkillReQualificationEmp_StepService, Domain.Services.Core.SkillReQualificationEmp_StepService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISkillReQualificationEmp_StepValidation, Domain.Entities.Core.Validations.SkillReQualificationEmp_StepValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISimulatorScenario_ScriptService, Domain.Services.Core.SimulatorScenario_ScriptService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISimulatorScenario_ScriptValidation, Domain.Entities.Core.Validations.SimulatorScenario_ScriptValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISimulatorScenario_EventService, Domain.Services.Core.SimulatorScenario_EventService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISimulatorScenario_EventValidation, Domain.Entities.Core.Validations.SimulatorScenario_EventValidation>();

            services.AddTransient<Domain.Interfaces.Service.Core.ISimulatorScenario_Script_CriteriaService, Domain.Services.Core.SimulatorScenario_Script_CriteriaService>();
            services.AddTransient<Domain.Interfaces.Validation.Core.ISimulatorScenario_Script_CriteriaValidation, Domain.Entities.Core.Validations.SimulatorScenario_Script_CriteriaValidation>();

            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnNewEmployeeAdded>), typeof(OnEmployeeCreatedEventHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnCbtCreated>), typeof(OnCbtCreatedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnClassSchedule_Delete>), typeof(OnClassSchedule_Delete_Handler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnClassSchedule_Create>), typeof(OnClassSchedule_Create_Handler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnClassSchedule_StudentEvaluations_StudentEvaluationReleased>), typeof(OnClassSchedule_StudentEvaluations_EvaluationReleasedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnClassScheduleEmployeeCreated>), typeof(OnClassScheduleEmployeeCreatedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnClientSetting_Notification_Disabled>), typeof(OnClientSetting_Notification_DisabledHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnClientSetting_Notification_Enabled>), typeof(OnClientSetting_Notification_EnabledHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnEmployeeCertificationHistoryCreated>), typeof(OnEmployeeCertificationHistoryCreatedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnIDP_ReviewCreated>), typeof(OnIDP_ReviewCreatedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnPretestCompleted>), typeof(OnPretestCompletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnProcedureReview_EmployeeCreated>), typeof(OnProcedureReview_EmployeeCreatedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnProcedureReviewPublished>), typeof(OnProcedureReviewPublishedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnSelfRegistrationApproved>), typeof(OnSelfRegistrationApprovedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnSelfRegistrationDenied>), typeof(OnSelfRegistrationDeniedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnTaskQualificationReleased>), typeof(OnTaskQualificationReleasedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnTaskQualification_Evalutor_LinkCreated>), typeof(OnTaskQualification_Evalutor_LinkCreatedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnTestReleased>), typeof(OnTestReleasedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnClassSchedule_Employee_Enrolled>), typeof(ClassSchedule_Employee_EnrolledHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnClassSchedule_Employee_Unenrolled>), typeof(ClassSchedule_Employee_UnenrolledHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnIDPAdded>), typeof(OnIDPAddedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnClassSchedule_Roster_TestCompleted>), typeof(OnClassSchedule_Roster_TestCompletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnClassSchedule_Employee_Completed>), typeof(ClassSchedule_Employee_CompletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.MetaIla_Employee_Enrolled>), typeof(MetaIla_Employee_EnrolledEventHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnTaskQualificationCompleted>), typeof(OnTaskQualificationCompleted_Handler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnMetaILA_MemberLink_Deleted>), typeof(OnMetaILA_MemberLink_Deleted_Handler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.On_DIFSurvey_Employee_Response_Updated>), typeof(On_DIFSurvey_Employee_Response_UpdatedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnEmployee_Deactivated>), typeof(OnEmployee_Deactivated_Handler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnSimulatorScenario_Collaborators_Updated>), typeof(OnSimulatorScenario_Collaborators_UpdatedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnSimulatorScenario_ILAs_Updated>), typeof(OnSimulatorScenario_ILAs_UpdatedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnSimulatorScenario_Position_Deleted>), typeof(OnSimulatorScenario_Position_DeletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnSimulatorScenario_Prerequisites_Updated>), typeof(OnSimulatorScenario_Prerequisites_UpdatedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnSimulatorScenario_Published>), typeof(OnSimulatorScenario_PublishedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnSimulatorScenario_Task_Criteria_Deleted>), typeof(OnSimulatorScenario_Task_Criteria_DeletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnILA_Deleted>), typeof(OnILA_Deleted_Handler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnCBT_ScormUpload_Connect>), typeof(OnCBT_ScormUpload_ConnectHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnCBT_ScormUpload_Disconnect>), typeof(OnCBT_ScormUpload_DisconnectHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnILA_StudentEvaluation_Link_Added>), typeof(OnILA_StudentEvaluation_Link_AddedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnILA_StudentEvaluation_Link_Removed>), typeof(OnILA_StudentEvaluation_Link_RemovedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnTestDeleted>), typeof(OnTestDeletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnCertificationDeleted>), typeof(OnCertificationDeletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnTaskDeleted>), typeof(OnTaskDeletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnTrainingProgramReview_Deleted>), typeof(OnTrainingProgramReview_DeletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnEmployee_Deleted>), typeof(OnEmployee_DeletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnTaskQualification_Deleted>), typeof(OnTaskQualification_DeletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnClassSchedule_Employee_Deleted>), typeof(OnClassSchedule_Employee_DeletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnClassSchedule_Roster_Deleted>), typeof(OnClassSchedule_Roster_DeletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnTaskListReview_Deleted>), typeof(OnTaskListReview_DeletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnTool_Deleted>), typeof(OnTool_DeletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnILA_TaskObjectiveLink_Unlinking>), typeof(OnILA_TaskObjectiveLink_UnlinkingHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnILA_EnablingObjectiveLink_Unlinking>), typeof(OnILA_EnablingObjectiveLink_UnlinkingHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnILACustomEOlink_Unlinking>), typeof(OnILACustomEOlink_UnlinkingHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnEnablingObjectiveDeleted>), typeof(OnEnablingObjectiveDeletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnClassSchedule_Evaluator_Link>), typeof(OnClassSchedule_Evaluator_LinkHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnClassSchedule_Evaluator_Unlink>), typeof(OnClassSchedule_Evaluator_UnlinkHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnDutyAreaDeleted>), typeof(OnDutyAreaDeletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnSubDutyAreaDeleted>), typeof(OnSubDutyAreaDeletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnEnablingObjectiveRemovedSQStatus>), typeof(OnEnablingObjectiveRemovedSQStatusHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnCBT_ScormRegistration_Completed>), typeof(OnCBT_ScormRegistration_CompletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnTrainingIssue_DataElementUpdate>), typeof(OnTrainingIssue_DataElementUpdateHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnILATraineeEvaluation_Updated>), typeof(OnILATraineeEvaluation_UpdatedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnILA_Deactivated>), typeof(OnILA_DeactivatedHandler));

            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnPublicClassScheduleRequestAccepted>), typeof(OnPublicClassScheduleRequestAcceptedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnPublicClassScheduleRequestRejected>), typeof(OnPublicClassScheduleRequestRejectedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnlaILACertificationLinkDeleted>), typeof(OniLACertificationLinkDeletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnPublicClassScheduleRequestSubmitted>), typeof(OnPublicClassScheduleRequestSubmittedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnClassSchedule_Update>), typeof(OnClassSchedule_UpdateHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnOrganizationDeleted>), typeof(OnOrganizationDeleted_Handler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnILA_Activated>), typeof(OnILA_ActivatedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnTaskReview_Deleted>), typeof(OnTaskReviewDeletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnSkillQualificationCompleted>), typeof(OnSkillQualificationCompletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnSimulatorScenario_Script_Deleted>), typeof(OnSimulatorScenario_Script_DeletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnSimulatorScenario_Event_Deleted>), typeof(OnSimulatorScenario_Event_DeletedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnSkillQualificationReleased>), typeof(OnSkillQualificationReleasedHandler));
            services.AddTransient(typeof(INotificationHandler<QTD2.Domain.Events.Core.OnSkillQualification_Evalutor_LinkCreated>), typeof(OnSkillQualification_Evalutor_LinkCreatedHandler));
            // ------------------ Domain Service and Validations DI End --------------------- //


        }

        public static void AddAuthenticationDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var provider = services.BuildServiceProvider();
            var authConfig = provider.GetService<IOptions<List<DbContextConfiguration>>>().Value.Where(r => r.Name == DbContextNames.QTDAuthenticationContext).First();

            switch (authConfig.Provider)
            {
                case SupportedProviders.Sqlite:
                    services.AddDbContext<Data.QTDAuthenticationContext>(options => options.UseSqlite(authConfig.ConnectionString, b => b.MigrationsAssembly("QTD2.Data.Migrations.Sqlite")));
                    break;
                case SupportedProviders.SqlServer:
                    services.AddDbContext<Data.QTDAuthenticationContext>(options => options.UseSqlServer(authConfig.ConnectionString, b => { b.UseCompatibilityLevel(120); b.MigrationsAssembly("QTD2.Data"); b.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(50), errorNumbersToAdd: null); }).UseLazyLoadingProxies(false));
                    break;
                default:
                    throw new System.Exception("Unknown provider");
            }
        }

        public static void AddAuthenticationDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<Domain.Interfaces.Repository.Authentication.IClientRepository, Data.Repository.Authentication.ClientRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Authentication.IInstanceRepository, Data.Repository.Authentication.InstanceRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Authentication.IInstanceSettingRepository, Data.Repository.Authentication.InstanceSettingRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Authentication.IEventLogRepository, Data.Repository.Authentication.EventLogRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Authentication.IIdentityProviderRepository, Data.Repository.Authentication.IdentityProviderRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Authentication.IInstanceIdentityProviderLinkRepository, Data.Repository.Authentication.InstanceIdentityProviderLinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Authentication.IAuthenticationSettingRepository, Data.Repository.Authentication.AuthenticationSettingRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Authentication.IAdminMessageAuthRepository, Data.Repository.Authentication.AdminMessageAuthRepository>();

        }

        public static void AddQTDDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(provider => provider.GetService<IDatabaseResolver>().BuildQtdContextAsync().Result);
            services.AddScoped<IMainUnitOfWork, MainUnitOfWork>();

            //Add services
            services.AddTransient<Domain.Interfaces.Repository.Core.IToolCategory_StatusHistoryRepository, Data.Repository.Core.ToolCategory_StatusHistoryRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IProcedureReview_EmployeeRepository, Data.Repository.Core.ProcedureReview_EmployeeRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILACertificationLinkRepository, Data.Repository.Core.ILACertificationLinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILACertificationSubRequirementLinkRepository, Data.Repository.Core.ILACertificationSubRequirementLinkRepository>();


            services.AddTransient<Domain.Interfaces.Repository.Core.IProcedureReviewRepository, Data.Repository.Core.ProcedureReviewRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITaskReQualificationEmp_SignOffRepository, Data.Repository.Core.TaskReQualificationEmp_SignOffRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.ITaskReQualificationEmp_QuestionAnswerRepository, Data.Repository.Core.TaskReQualificationEmp_QuestionAnswerRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.INotificationRecipietRepository, Data.Repository.Core.NotificationRecipietRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.ITaskReQualificationEmp_StepsRepository, Data.Repository.Core.TaskReQualificationEmp_StepsRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.ITaskReQualificationEmp_SuggestionRepository, Data.Repository.Core.TaskReQualificationEmp_SuggestionRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILACertificationLinkRepository, Data.Repository.Core.ILACertificationLinkRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IILACertificationSubRequirementLinkRepository, Data.Repository.Core.ILACertificationSubRequirementLinkRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IProcedureReview_EmployeeRepository, Data.Repository.Core.ProcedureReview_EmployeeRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IProcedureReviewRepository, Data.Repository.Core.ProcedureReviewRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ICertificationRepository, Data.Repository.Core.CertificationRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ICertification_HistoryRepository, Data.Repository.Core.Certification_HistoryRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.ICertifyingBodyRepository, Data.Repository.Core.CertifyingBodyRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ICertifyingBody_HistoryRepository, Data.Repository.Core.CertifyingBody_HistoryRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IClassSchedule_SelfRegistrationRepository, Data.Repository.Core.ClassShedule_SelfRegistrationRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IDocumentRepository, Data.Repository.Core.DocumentRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IDocumentTypeRepository, Data.Repository.Core.DocumentTypeRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IPersonRepository, Data.Repository.Core.PersonRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IClientUserRepository, Data.Repository.Core.ClientUserRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEmployeeCertificationRepository, Data.Repository.Core.EmployeeCertificationRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEmployeeOrganizationRepository, Data.Repository.Core.EmployeeOrganizationRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEmployeePositionRepository, Data.Repository.Core.EmployeePositionRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEmployeeRepository, Data.Repository.Core.EmployeeRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IOrganizationRepository, Data.Repository.Core.OrganizationRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IPositionRepository, Data.Repository.Core.PositionRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITrainingProgramRepository, Data.Repository.Core.TrainingProgramRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IScormUploadRepository, Data.Repository.Core.ScormUploadRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IDutyAreaRepository, Data.Repository.Core.DutyAreaRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEnablingObjectiveRepository, Data.Repository.Core.EnablingObjectiveRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEnablingObjective_CategoryRepository, Data.Repository.Core.EnablingObjective_CategoryRepository>();
            //services.AddTransient<Domain.Interfaces.Repository.Core.IEnablingObjective_Procedure_LinkRepository, Data.Repository.Core.EnablingObjective_Procedure_LinkRepository>();
            //services.AddTransient<Domain.Interfaces.Repository.Core.IEnablingObjective_SaftyHazard_LinkRepository, Data.Repository.Core.EnablingObjective_SaftyHazard_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEnablingObjective_SubCategoryRepository, Data.Repository.Core.EnablingObjective_SubCategoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEnablingObjective_TopicRepository, Data.Repository.Core.EnablingObjective_TopicRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IProcedureRepository, Data.Repository.Core.ProcedureRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IProcedure_EnablingObjective_LinkRepository, Data.Repository.Core.Procedure_EnablingObjective_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IProcedure_IssuingAuthorityRepository, Data.Repository.Core.Procedure_IssuingAuthorityRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IProcedure_SaftyHazard_LinkRepository, Data.Repository.Core.Procedure_SaftyHazard_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISaftyHazardRepository, Data.Repository.Core.SaftyHazardRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISaftyHazard_AbatementRepository, Data.Repository.Core.SaftyHazard_AbatementRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISaftyHazard_ControlRepository, Data.Repository.Core.SaftyHazard_ControlRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISaftyHazard_CategoryRepository, Data.Repository.Core.SaftyHazard_CategoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISubdutyAreaRepository, Data.Repository.Core.SubdutyAreaRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITaskRepository, Data.Repository.Core.TaskRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITask_EnablingObjective_LinkRepository, Data.Repository.Core.Task_EnablingObjective_LinkRepository>();
            //services.AddTransient<Domain.Interfaces.Repository.Core.ITask_Procedure_LinkRepository, Data.Repository.Core.Task_Procedure_LinkRepository>();
            //services.AddTransient<Domain.Interfaces.Repository.Core.ITask_SaftyHazard_LinkRepository, Data.Repository.Core.Task_SaftyHazard_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITask_StepRepository, Data.Repository.Core.Task_StepRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITask_ToolRepository, Data.Repository.Core.Task_ToolRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IToolRepository, Data.Repository.Core.ToolRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IToolGroupRepository, Data.Repository.Core.ToolGroupRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IToolGroup_ToolRepository, Data.Repository.Core.ToolGroup_ToolRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_TaskRepository, Data.Repository.Core.Version_TaskRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_Task_QuestionRepository, Data.Repository.Core.Version_Task_QuestionRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_ProcedureRepository, Data.Repository.Core.Version_ProcedureRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_Task_Procedure_LinkRepository, Data.Repository.Core.Version_Task_Procedure_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_ToolRepository, Data.Repository.Core.Version_ToolRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_Task_Tool_LinkRepository, Data.Repository.Core.Version_Task_Tool_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_Task_StepRepository, Data.Repository.Core.Version_Task_StepRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_Procedure_Tool_LinkRepository, Data.Repository.Core.Version_Procedure_Tool_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_EnablingObjectiveRepository, Data.Repository.Core.Version_EnablingObjectiveRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_EnablingObjective_Tool_LinkRepository, Data.Repository.Core.Version_EnablingObjective_Tool_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_SaftyHazardRepository, Data.Repository.Core.Version_SaftyHazardRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_SaftyHazard_AbatementRepository, Data.Repository.Core.Version_SaftyHazard_AbatementRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_SaftyHazard_ControlRepository, Data.Repository.Core.Version_SaftyHazard_ControlRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_Task_EnablingObjective_LinkRepository, Data.Repository.Core.Version_Task_EnablingObjective_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_Task_SaftyHazard_LinkRepository, Data.Repository.Core.Version_Task_SaftyHazard_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_Procedure_SaftyHazard_LinkRepository, Data.Repository.Core.Version_Procedure_SaftyHazard_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_EnablingObjective_SaftyHazard_LinkRepository, Data.Repository.Core.Version_EnablingObjective_SaftyHazard_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_EnablingObjective_Procedure_LinkRepository, Data.Repository.Core.Version_EnablingObjective_Procedure_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_Procedure_EnablingObjective_LinkRepository, Data.Repository.Core.Version_Procedure_EnablingObjective_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEmployee_TaskRepository, Data.Repository.Core.Employee_TaskRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITimesheetRepository, Data.Repository.Core.TimesheetRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITask_QuestionRepository, Data.Repository.Core.Task_QuestionRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITask_PositionRepository, Data.Repository.Core.Task_PositionRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IProviderLevelRepository, Data.Repository.Core.ProviderLevelRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IProviderRepository, Data.Repository.Core.ProviderRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILA_TopicRepository, Data.Repository.Core.ILA_TopicRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IDeliveryMethodRepository, Data.Repository.Core.DeliveryMethodRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITrainingTopicRepository, Data.Repository.Core.TrainingTopicRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.INercStandardRepository, Data.Repository.Core.NercStandardRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITraineeEvaluationTypeRepository, Data.Repository.Core.TraineeEvaluationTypeRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IMetaILARepository, Data.Repository.Core.MetaILARepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IMetaILA_EmployeeRepository, Data.Repository.Core.MetaILA_EmployeeRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IMetaILA_Employee_MemberLinkFufillmentRepository, Data.Repository.Core.MetaILA_Employee_MemberLinkFufillmentRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISegmentRepository, Data.Repository.Core.SegmentRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IAssessmentToolRepository, Data.Repository.Core.AssessmentToolRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IRR_IssuingAuthorityRepository, Data.Repository.Core.RR_IssuingAuthorityRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IRegulatoryRequirementRepository, Data.Repository.Core.RegulatoryRequirementRepository>();
            //services.AddTransient<Domain.Interfaces.Repository.Core.IRR_SafetyHazard_LinkRepository, Data.Repository.Core.RR_SafetyHazard_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILARepository, Data.Repository.Core.ILARepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILA_NercStandard_LinkRepository, Data.Repository.Core.ILA_NercStandard_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILA_SafetyHazard_LinkRepository, Data.Repository.Core.ILA_SafetyHazard_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILA_Segment_LinkRepository, Data.Repository.Core.ILA_Segment_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILACollaboratorRepository, Data.Repository.Core.ILACollaboratorRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILA_Position_LinkRepository, Data.Repository.Core.ILA_Position_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IRR_Task_LinkRepository, Data.Repository.Core.RR_Task_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILA_TaskObjective_LinkRepository, Data.Repository.Core.ILA_TaskObjective_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILA_EnablingObjective_LinkRepository, Data.Repository.Core.ILA_EnablingObjective_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILA_Procedure_LinkRepository, Data.Repository.Core.ILA_Procedure_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILA_TrainingTopic_LinkRepository, Data.Repository.Core.ILA_TrainingTopic_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILA_RegRequirement_LinkRepository, Data.Repository.Core.ILA_RegRequirement_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILA_AssessmentTool_LinkRepository, Data.Repository.Core.ILA_AssessmentTool_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILAResourceRepository, Data.Repository.Core.ILAResourceRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IRegRequirement_EO_LinkRepository, Data.Repository.Core.RegRequirement_EO_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IProcedure_ILA_LinkRepository, Data.Repository.Core.Procedure_ILA_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IProcedure_RR_LinkRepository, Data.Repository.Core.Procedure_RR_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISaftyHazard_RR_LinkRepository, Data.Repository.Core.SaftyHazard_RR_LinkRepository>();
            //services.AddTransient<Domain.Interfaces.Repository.Core.ISafetyHazard_Procedure_LinkRepository, Data.Repository.Core.SafetyHazard_Procedure_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITrainingTopic_CategoryRepository, Data.Repository.Core.TrainingTopic_CategoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.INERCTargetAudienceRepository, Data.Repository.Core.NERCTargetAudienceRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IRatingScaleRepository, Data.Repository.Core.RatingScaleRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IStudentEvaluationFormRepository, Data.Repository.Core.StudentEvaluationFormRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IStudentEvaluationAvailabilityRepository, Data.Repository.Core.StudentEvaluationAvailabilityRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILA_NERCAudience_LinkRepository, Data.Repository.Core.ILA_NERCAudience_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILA_StudentEvaluation_LinkRepository, Data.Repository.Core.ILA_StudentEvaluation_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ICoverSheetTypeRepository, Data.Repository.Core.CoverSheetTypeRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IStudentEvaluationQuestionRepository, Data.Repository.Core.StudentEvaluationQuestionRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IMeta_ILAMembers_LinkRepository, Data.Repository.Core.Meta_ILAMembers_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.INercStandardMemberRepository, Data.Repository.Core.NercStandardMemberRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ICollaboratorInvitationRepository, Data.Repository.Core.CollaboratorInvitationRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ICoversheetRepository, Data.Repository.Core.CoversheetRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ICustomEnablingObjectiveRepository, Data.Repository.Core.CustomEnablingObjectiveRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISegmentObjective_LinkRepository, Data.Repository.Core.SegmentObjective_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IStudentEvaluationAudienceRepository, Data.Repository.Core.StudentEvaluationAudienceRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILACustomObjective_LinkRepository, Data.Repository.Core.ILACustomObjective_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILA_PreRequisite_LinkRepository, Data.Repository.Core.ILA_PreRequisite_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILA_UploadRepository, Data.Repository.Core.ILA_UploadRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITaxonomyLevelRepository, Data.Repository.Core.TaxonomyLevelRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITestStatusRepository, Data.Repository.Core.TestStatusRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITestRepository, Data.Repository.Core.TestRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITestTypeRepository, Data.Repository.Core.TestTypeRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITestSettingRepository, Data.Repository.Core.TestSettingRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITestItemTypeRepository, Data.Repository.Core.TestItemTypeRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITestItemRepository, Data.Repository.Core.TestItemRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITestItemTrueFalseRepository, Data.Repository.Core.TestItemTrueFalseRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITestItemMatchRepository, Data.Repository.Core.TestItemMatchRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITestItemMCQRepository, Data.Repository.Core.TestItemMCQRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITestItemFillBlankRepository, Data.Repository.Core.TestItemFillBlankRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILATraineeEvaluationRepository, Data.Repository.Core.ILATraineeEvaluationRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITestItemShortAnswerRepository, Data.Repository.Core.TestItemShortAnswerRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITest_Item_LinkRepository, Data.Repository.Core.Test_Item_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IProc_IssuingAuthority_HistoryRepository, Data.Repository.Core.Proc_IssuingAuthority_HistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IProcedure_Task_LinkRepository, Data.Repository.Core.Procedure_Task_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IProcedure_StatusHistoryRepository, Data.Repository.Core.Procedure_StatusHistoryRepository>();
            //services.AddTransient<Domain.Interfaces.Repository.Core.IRR_Procedure_LinkRepository, Data.Repository.Core.RR_Procedure_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IRR_StatusHistoryRepository, Data.Repository.Core.RR_StatusHistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISafetyHazard_EO_LinkRepository, Data.Repository.Core.SafetyHazard_EO_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISafetyHazard_Task_LinkRepository, Data.Repository.Core.SafetyHazard_Task_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISafetyHazard_SetRepository, Data.Repository.Core.SafetyHazard_SetRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISafetyHazard_Set_LinkRepository, Data.Repository.Core.SafetyHazard_Set_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISafetyHazard_HistoryRepository, Data.Repository.Core.SafetyHazard_HistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISafetyHazard_CategoryHistoryRepository, Data.Repository.Core.SafetyHazard_CategoryHistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITask_ReferenceRepository, Data.Repository.Core.Task_ReferenceRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITask_Reference_LinkRepository, Data.Repository.Core.Task_Reference_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITask_ILA_LinkRepository, Data.Repository.Core.Task_ILA_LinkRepository>();
            //services.AddTransient<Domain.Interfaces.Repository.Core.ITask_RR_LinkRepository, Data.Repository.Core.Task_RR_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITask_CollaboratorInvitationRepository, Data.Repository.Core.Task_CollaboratorInvitationRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITask_Collaborator_LinkRepository, Data.Repository.Core.Task_Collaborator_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILA_UploadRepository, Data.Repository.Core.ILA_UploadRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IDiscussionQuestionRepository, Data.Repository.Core.DiscussionQuestionRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEnablingObjective_CategoryHistoryRepository, Data.Repository.Core.EnablingObjective_CategoryHistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IRR_IssuingAuthority_StatusHistoryRepository, Data.Repository.Core.RR_IssuingAuthority_StatusHistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEnablingObjective_TopicHistoryRepository, Data.Repository.Core.EnablingObjective_TopicHistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEnablingObjective_SubCategoryHistoryRepository, Data.Repository.Core.EnablingObjective_SubCategoryHistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEnablingObjectiveHistoryRepository, Data.Repository.Core.EnablingObjectiveHistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILAHistoryRepository, Data.Repository.Core.ILAHistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IToolCategoryRepository, Data.Repository.Core.ToolCategoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISafetyHazard_Tool_LinkRepository, Data.Repository.Core.SafetyHazard_Tool_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITool_StatusHistoryRepository, Data.Repository.Core.Tool_StatusHistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructor_Repository, Data.Repository.Core.Instructor_Repository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructor_CategoryRepository, Data.Repository.Core.Instructor_CategoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructor_HistoryRepository, Data.Repository.Core.Instructor_HistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructor_CategoryHistoryRepository, Data.Repository.Core.Instructor_CategoryHistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITask_HistoryRepository, Data.Repository.Core.Task_HistoryRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.ILocation_Repository, Data.Repository.Core.Location_Repository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ILocation_CategoryRepository, Data.Repository.Core.Location_CategoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ILocation_HistoryRepository, Data.Repository.Core.Location_HistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ILocation_CategoryHistoryRepository, Data.Repository.Core.Location_CategoryHistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITask_MetaTask_LinkRepository, Data.Repository.Core.Task_MetaTask_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITask_SuggestionRepository, Data.Repository.Core.Task_SuggestionRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITrainingGroup_CategoryRepository, Data.Repository.Core.TrainingGroup_CategoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITrainingGroupRepository, Data.Repository.Core.TrainingGroupRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITask_TrainingGroupRepository, Data.Repository.Core.Task_TrainingGroupRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_Task_ILA_LinkRepository, Data.Repository.Core.Version_Task_ILA_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_ILARepository, Data.Repository.Core.Version_ILARepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_RegulatoryRequirementRepository, Data.Repository.Core.Version_RegulatoryRequirementRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_Task_RR_LinkRepository, Data.Repository.Core.Version_Task_RR_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_TrainingGroupRepository, Data.Repository.Core.Version_TrainingGroupRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_Task_TrainingGroupRepository, Data.Repository.Core.Version_Task_TrainingGroupRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IPositionHistoryRepository, Data.Repository.Core.PositionHistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IPosition_TaskRepository, Data.Repository.Core.Position_TaskRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IPositions_SQRepository, Data.Repository.Core.Positions_SQRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IPosition_EmployeeRepository, Data.Repository.Core.Position_EmployeeRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEnablingObjective_MetaEO_LinkRepository, Data.Repository.Core.EnablingObjective_MetaEO_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITestItem_HistoryRepository, Data.Repository.Core.TestItem_HistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_EnablingObjective_TaskRepository, Data.Repository.Core.Version_EnablingObjective_TaskRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_EnablingObjective_ILALinkRepository, Data.Repository.Core.Version_EnablingObjective_ILALinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_EnablingObjective_RRLinkRepository, Data.Repository.Core.Version_EnablingObjective_RRLinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_EnablingObjective_MetaEOLinkRepository, Data.Repository.Core.Version_EnablingObjective_MetaEOLinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEnablingObjective_StepRepository, Data.Repository.Core.EnablingObjective_StepRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEnablingObjective_Employee_LinkRepository, Data.Repository.Core.EnablingObjective_Employee_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_PositionRepository, Data.Repository.Core.Version_PositionRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_EnablingObjective_Position_LinkRepository, Data.Repository.Core.Version_EnablingObjective_Position_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_EmployeeRepository, Data.Repository.Core.Version_EmployeeRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_EnablingObjective_Employee_LinkRepository, Data.Repository.Core.Version_EnablingObjective_Employee_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEnablingObjective_QuestionRepository, Data.Repository.Core.EnablingObjective_QuestionRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_EnablingObjective_QuestionRepository, Data.Repository.Core.Version_EnablingObjective_QuestionRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_TestItemsRepository, Data.Repository.Core.Version_TestItemsRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_Task_SuggestionRepository, Data.Repository.Core.Version_Task_SuggestionRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_Task_Position_LinkRepository, Data.Repository.Core.Version_Task_Position_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_EnablingObjective_StepRepository, Data.Repository.Core.Version_EnablingObjective_StepRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEnablingObjective_SuggestionRepository, Data.Repository.Core.EnablingObjective_SuggestionRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEnablingObjective_ToolRepository, Data.Repository.Core.EnablingObjective_ToolRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_EnablingObjective_SuggestionsRepository, Data.Repository.Core.Version_EnablingObjective_SuggestionsRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEmployeeCertificationHistoryRepository, Data.Repository.Core.EmployeeCretificationHistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ICertificationIssuingAuthorityRepository, Data.Repository.Core.CertificationIssuingAuthorityRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IActivityNotificationRepository, Data.Repository.Core.ActivityNotificationRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEmployeeDocumentRepository, Data.Repository.Core.EmployeeDocumentRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEmployeeHistoryRepository, Data.Repository.Core.EmployeeHistoryRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IClientSettings_NotificationRepository, Data.Repository.Core.ClientSettings_NotificationRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IClientUserSettings_GeneralSettingRepository, Data.Repository.Core.ClientUserSettings_GeneralSettingRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IClientUserSettings_Dashboard_SettingRepository, Data.Repository.Core.ClientUserSettings_Dashboard_SettingRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IDashboard_SettingRepository, Data.Repository.Core.Dashboard_SettingRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IClientSettings_LabelReplacementsRepository, Data.Repository.Core.ClientSettings_LabelReplacementsRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IClientSettings_LicenseRepository, Data.Repository.Core.ClientSettings_LicenseRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.ITrainingProgramTypeRepository, Data.Repository.Core.TrainingProgramTypeRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITrainingPrograms_ILA_LinkRepository, Data.Repository.Core.TrainingPrograms_ILA_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITrainingProgram_HistoryRepository, Data.Repository.Core.TrainingProgram_HistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_TrainingProgramRepository, Data.Repository.Core.Version_TrainingProgramRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_TrainingProgram_ILA_LinkRepository, Data.Repository.Core.Version_TrainingProgram_ILA_LinkRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IRatingScaleExpandedRepository, Data.Repository.Core.RatingScaleExpandedRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IRatingScaleNRepository, Data.Repository.Core.RatingScaleNRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IStudentEvaluationRepository, Data.Repository.Core.StudentEvaluationRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IStudentEvaluationHistoryRepository, Data.Repository.Core.StudentEvaluationHistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IQuestionBankRepository, Data.Repository.Core.QuestionBankRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IQuestionBankHistoryRepository, Data.Repository.Core.QuestionBankHistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IStudentEvaluation_QuestionRepository, Data.Repository.Core.StudentEvaluation_QuestionRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_TestStausRepository, Data.Repository.Core.Version_TestStausRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_TestRepository, Data.Repository.Core.Version_TestRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITest_HistoryRepository, Data.Repository.Core.Test_HistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IDutyArea_HistoryRepository, Data.Repository.Core.DutyArea_HistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISubDutyArea_HistoryRepository, Data.Repository.Core.SubDutyArea_HistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IQTDUserRepository, Data.Repository.Core.QTDUserRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IClassSchedule_Roster_ResponseRepository, Data.Repository.Core.ClassSchedule_Roster_ResponseRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IEvaluationMethodRepository, Data.Repository.Core.EvaluationMethodRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITaskQualificationRepository, Data.Repository.Core.TaskQualificationRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITaskQualification_Evaluator_LinkRepository, Data.Repository.Core.TaskQualification_Evaluator_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITaskQualificationStatusRepository, Data.Repository.Core.TaskQualificationStatusRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITQEmpSettingRepository, Data.Repository.Core.TQEmpSettingRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IClassScheduleRepository, Data.Repository.Core.ClassScheduleRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IClassScheduleHistoryRepository, Data.Repository.Core.ClassScheduleHistoryRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ICBTRepository, Data.Repository.Core.CBTRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEvaluationReleaseEMPSettingsRepository, Data.Repository.Core.EvaluationReleaseEMPSettingsRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITestReleaseEMPSettingsRepository, Data.Repository.Core.TestReleaseEMPSettingsRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITestReleaseEMPSetting_Retake_LinkRepository, Data.Repository.Core.TestReleaseEMPSetting_Retake_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISelfRegistrationOptionsRepository, Data.Repository.Core.SelfRegistrationOptionsRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IClassScheduleEmployeeRepository, Data.Repository.Core.ClassScheduleEmployeeRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IClassSchedule_RosterRepository, Data.Repository.Core.ClassSchedule_RosterRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITQILAEmpSettingRepository, Data.Repository.Core.TQILAEmpSettingRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILA_Evaluator_LinkRepository, Data.Repository.Core.ILA_Evaluator_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IClassSchedule_Roster_StatusesRepository, Data.Repository.Core.ClassSchedule_Roster_StatusesRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IClassSchedule_RecurrenceRepository, Data.Repository.Core.ClassSchedule_RecurrenceRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IClassSchedule_StudentEvaluations_LinkRepository, Data.Repository.Core.ClassSchedule_StudentEvaluations_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IStudentEvaluationWithoutEmpRepository, Data.Repository.Core.StudentEvaluationWithoutEmpRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IEmpTestRepository, Data.Repository.Core.EmpTestRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IReportRepository, Data.Repository.Core.ReportRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IReportSkeletonRepository, Data.Repository.Core.ReportSkeletonRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IReportSkeletonCategoryRepository, Data.Repository.Core.ReportSkeletonCategoryRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IIDPRepository, Data.Repository.Core.IDPRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IIDPScheduleRepository, Data.Repository.Core.IDPScheduleRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IIDP_ReviewStatusRepository, Data.Repository.Core.IDP_ReviewStatusRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IIDP_ReviewRepository, Data.Repository.Core.IDP_ReviewRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IClassSchedule_Evaluation_RosterRepository, Data.Repository.Core.ClassSchedule_Evaluation_RosterRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IRatingScaleExpandedRepository, Data.Repository.Core.RatingScaleExpandedRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_Task_MetaTask_LinkRepository, Data.Repository.Core.Version_Task_MetaTask_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IMetaILAConfigurationPublishOptionRepository, Data.Repository.Core.MetaILAConfigurationPublishOptionRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IVersion_MetaILARepository, Data.Repository.Core.Version_MetaILARepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IMetaILA_StatusRepository, Data.Repository.Core.MetaILA_StatusRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILA_PerformTraineeEvalRepository, Data.Repository.Core.ILA_PerformTraineeEvalRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ICertificationSubRequirementRepository, Data.Repository.Core.CertificationSubRequirementRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ICBT_ScormRegistrationRepository, Data.Repository.Core.CBT_ScormRegistrationRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.INotificationRepository, Data.Repository.Core.NotificationRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IMetaILA_SummaryTestRepository, Data.Repository.Core.MetaILA_SummaryTestRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITrainingProgramReviewsRepository, Data.Repository.Core.TrainingProgramReviewsRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_DesignDefaultViewRepository, Data.Repository.Core.InstructorWorkbook_DesignDefaultViewRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILA_DesignRepository, Data.Repository.Core.InstructorWorkbook_ILA_DesignRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILA_DevelopRepository, Data.Repository.Core.InstructorWorkbook_ILA_DevelopRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILA_ImplementRepository, Data.Repository.Core.InstructorWorkbook_ILA_ImplementRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILADesign_DelieveryMethodsRepository, Data.Repository.Core.InstructorWorkbook_ILADesign_DelieveryMethodsRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILADesign_EnablingObjectivesRepository, Data.Repository.Core.InstructorWorkbook_ILADesign_EnablingObjectivesRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILADesign_NERCRepository, Data.Repository.Core.InstructorWorkbook_ILADesign_NERCRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILADesign_PrerequistieRepository, Data.Repository.Core.InstructorWorkbook_ILADesign_PrerequistieRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILADesign_ProceduresRepository, Data.Repository.Core.InstructorWorkbook_ILADesign_ProceduresRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILADesign_ResourcesRepository, Data.Repository.Core.InstructorWorkbook_ILADesign_ResourcesRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILADesign_SafetyHazardsRepository, Data.Repository.Core.InstructorWorkbook_ILADesign_SafetyHazardsRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILADesign_SegmentsRepository, Data.Repository.Core.InstructorWorkbook_ILADesign_SegmentsRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILADesign_TargetAudienceRepository, Data.Repository.Core.InstructorWorkbook_ILADesign_TargetAudienceRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILADesign_TasksRepository, Data.Repository.Core.InstructorWorkbook_ILADesign_TasksRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILADesign_TrainingTopicsRepository, Data.Repository.Core.InstructorWorkbook_ILADesign_TrainingTopicsRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILADesignReviewersRepository, Data.Repository.Core.InstructorWorkbook_ILADesignReviewersRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILADevelopReviewersRepository, Data.Repository.Core.InstructorWorkbook_ILADevelopReviewersRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILAEvaluationRepository, Data.Repository.Core.InstructorWorkbook_ILAEvaluationRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILAEvaluation_DefaultViewRepository, Data.Repository.Core.InstructorWorkbook_ILAEvaluation_DefaultViewRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILAEvaluation_TestAnalysisRepository, Data.Repository.Core.InstructorWorkbook_ILAEvaluation_TestAnalysisRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILAEvaluation_TrainingIssuesRepository, Data.Repository.Core.InstructorWorkbook_ILAEvaluation_TrainingIssuesRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILAImplement_ClassSchedulerepository, Data.Repository.Core.InstructorWorkbook_ILAImplement_ClassScheduleRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILAImplementReviewersRepository, Data.Repository.Core.InstructorWorkbook_ILAImplementReviewersRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ILAPhasesRepository, Data.Repository.Core.InstructorWorkbook_ILAPhasesRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_PhasesRepository, Data.Repository.Core.InstructorWorkbook_PhasesRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ProspectiveILARepository, Data.Repository.Core.InstructorWorkbook_ProspectiveILARepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ProspectiveILA_ArchivesRepository, Data.Repository.Core.InstructorWorkbook_ProspectiveILA_ArchivesRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_ProspectiveILA_SnapshotRepository, Data.Repository.Core.InstructorWorkbook_ProspectiveILA_SnapshotRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_Segments_LinkObjectivesRepository, Data.Repository.Core.InstructorWorkbook_Segments_LinkObjectivesRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_Segments_NercStandardsRepository, Data.Repository.Core.InstructorWorkbook_Segments_NercStandardsRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_TrainingTopicsRepository, Data.Repository.Core.InstructorWorkbook_TrainingTopicsRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IInstructorWorkbook_TrainingTopicsHeadingRepository, Data.Repository.Core.InstructorWorkbook_TrainingTopicsHeadingRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IILA_Topic_LinkRepository, Data.Repository.Core.ILA_Topic_LinkRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IDIFSurveyRepository, Data.Repository.Core.DIFSurveyRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IDIFSurvey_DevStatusRepository, Data.Repository.Core.DIFSurvey_DevStatusRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IDIFSurvey_EmployeeRepository, Data.Repository.Core.DIFSurvey_EmployeeRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IDIFSurvey_Employee_StatusRepository, Data.Repository.Core.DIFSurvey_Employee_StatusRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IDIFSurvey_Employee_ResponseRepository, Data.Repository.Core.DIFSurvey_Employee_ResponseRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IDIFSurvey_TaskRepository, Data.Repository.Core.DIFSurvey_TaskRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IDIFSurvey_Task_StatusRepository, Data.Repository.Core.DIFSurvey_Task_StatusRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IDIFSurvey_Task_TrainingFrequencyRepository, Data.Repository.Core.DIFSurvey_Task_TrainingFrequencyRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IClientSettings_FeatureRepository, Data.Repository.Core.ClientSettings_FeatureRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IEmpSettingsReleaseTypeRepository, Data.Repository.Core.EmpSettingsReleaseTypeRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.ITaskListReviewRepository, Data.Repository.Core.TaskListReviewRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITaskListReview_GeneralReviewerRepository, Data.Repository.Core.TaskListReview_GeneralReviewerRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITaskListReview_TypeRepository, Data.Repository.Core.TaskListReview_TypeRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITaskListReview_StatusRepository, Data.Repository.Core.TaskListReview_StatusRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITaskReviewRepository, Data.Repository.Core.TaskReviewRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITaskReview_StatusRepository, Data.Repository.Core.TaskReview_StatusRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITaskReview_ReviewerRepository, Data.Repository.Core.TaskReview_ReviewerRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITaskReview_FindingRepository, Data.Repository.Core.TaskReview_FindingRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IActionItemRepository, Data.Repository.Core.ActionItemRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IActionItem_PriorityRepository, Data.Repository.Core.ActionItem_PriorityRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IActionItem_SubDuty_OperationRepository, Data.Repository.Core.ActionItem_SubDuty_OperationRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IActionItem_Step_OperationRepository, Data.Repository.Core.ActionItem_Step_OperationRepository>(); services.AddTransient<Domain.Interfaces.Repository.Core.IActionItem_QuestionAndAnswer_OperationRepository, Data.Repository.Core.ActionItem_QuestionAndAnswer_OperationRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IActionItem_Suggestion_OperationRepository, Data.Repository.Core.ActionItem_Suggestion_OperationRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IActionItem_Tool_OperationRepository, Data.Repository.Core.ActionItem_Tool_OperationRepository>(); services.AddTransient<Domain.Interfaces.Repository.Core.IActionItem_EnablingObjective_OperationRepository, Data.Repository.Core.ActionItem_EnablingObjective_OperationRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IActionItem_Procedure_OperationRepository, Data.Repository.Core.ActionItem_Procedure_OperationRepository>(); services.AddTransient<Domain.Interfaces.Repository.Core.IActionItem_RegulatoryRequirement_OperationRepository, Data.Repository.Core.ActionItem_RegulatoryRequirement_OperationRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IActionItem_OperationType_LinksRepository, Data.Repository.Core.ActionItem_OperationType_LinksRepository>(); services.AddTransient<Domain.Interfaces.Repository.Core.IActionItem_OperationTypesRepository, Data.Repository.Core.ActionItem_OperationTypesRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IActionItem_OperationType_LinksRepository, Data.Repository.Core.ActionItem_OperationType_LinksRepository>(); services.AddTransient<Domain.Interfaces.Repository.Core.IActionItem_SafetyHazard_OperationRepository, Data.Repository.Core.ActionItem_SafetyHazard_OperationRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.ISimulatorScenarioRepository, Data.Repository.Core.SimulatorScenarioRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISimulatorScenario_DifficultyRepository, Data.Repository.Core.SimulatorScenario_DifficultyRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISimulatorScenario_PositionRepository, Data.Repository.Core.SimulatorScenario_PositionRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISimulatorScenario_TaskRepository, Data.Repository.Core.SimulatorScenario_TaskRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISimulatorScenario_EnablingObjectiveRepository, Data.Repository.Core.SimulatorScenario_EnablingObjectiveRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISimulatorScenario_ProcedureRepository, Data.Repository.Core.SimulatorScenario_ProcedureRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISimulatorScenario_Task_CriteriaRepository, Data.Repository.Core.SimulatorScenario_Task_CriteriaRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISimulatorScenario_ILARepository, Data.Repository.Core.SimulatorScenario_ILARepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISimulatorScenario_CollaboratorRepository, Data.Repository.Core.SimulatorScenario_CollaboratorRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISimulatorScenario_CollaboratorPermissionRepository, Data.Repository.Core.SimulatorScenario_CollaboratorPermissionRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISimulatorScenario_PrerequisiteRepository, Data.Repository.Core.SimulatorScenario_PrerequisiteRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISimulatorScenario_StatusRepository, Data.Repository.Core.SimulatorScenario_StatusRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISimulatorScenario_PrerequisiteRepository, Data.Repository.Core.SimulatorScenario_PrerequisiteRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.ITrainingIssueRepository, Data.Repository.Core.TrainingIssueRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITrainingIssue_StatusRepository, Data.Repository.Core.TrainingIssue_StatusRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITrainingIssue_SeverityRepository, Data.Repository.Core.TrainingIssue_SeverityRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITrainingIssue_DriverTypeRepository, Data.Repository.Core.TrainingIssue_DriverTypeRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITrainingIssue_DriverSubTypeRepository, Data.Repository.Core.TrainingIssue_DriverSubTypeRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITrainingIssue_DataElementRepository, Data.Repository.Core.TrainingIssue_DataElementRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITrainingIssue_ActionItemRepository, Data.Repository.Core.TrainingIssue_ActionItemRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITrainingIssue_ActionItemPriorityRepository, Data.Repository.Core.TrainingIssue_ActionItemPriorityRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ITrainingIssue_ActionItemStatusRepository, Data.Repository.Core.TrainingIssue_ActionItemStatusRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IClassSchedule_Roster_TimeRecordRepository, Data.Repository.Core.ClassSchedule_Roster_TimeRecordReporsitory>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IClassSchedule_TQEMPSettingsRepository, Data.Repository.Core.ClassSchedule_TQEMPSettingsRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IClassSchedule_Evaluator_LinksRepository, Data.Repository.Core.ClassSchedule_Evaluator_LinksRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IClassSchedule_TestReleaseEMPSettingsRepository, Data.Repository.Core.ClassSchedule_TestReleaseEMPSettingsRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IClassSchedule_TestReleaseEMPSetting_Retake_LinksRepository, Data.Repository.Core.ClassSchedule_TestReleaseEMPSetting_Retake_LinksRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IPersonActivityNotificationRepository, Data.Repository.Core.PersonActivityNotificationRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.ITrainingProgramReview_TrainingIssue_LinkRepository, Data.Repository.Core.TrainingProgramReview_TrainingIssue_LinkRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.IPublicClassScheduleRequestRepository, Data.Repository.Core.PublicClassScheduleRequestRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IAdminMessageRepository, Data.Repository.Core.AdminMessageRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IAdminMessage_QTDUserRepository, Data.Repository.Core.AdminMessage_QTDUserRepository>();
           
            services.AddTransient<Domain.Interfaces.Repository.Core.IClassScheduleEmployee_ILACertificationLink_PartialCreditRepository, Data.Repository.Core.ClassScheduleEmployee_ILACertificationLink_PartialCreditRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.IClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditRepository, Data.Repository.Core.ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.ITaskListReview_PositionLinkRepository, Data.Repository.Core.TaskListReview_PositionLinkRepository>();

            services.AddTransient<Domain.Interfaces.Repository.Core.ISkillQualificationRepository, Data.Repository.Core.SkillQualificationRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISkillQualificationStatusRepository, Data.Repository.Core.SkillQualificationStatusRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISkillQualification_Evaluator_LinkRepository, Data.Repository.Core.SkillQualification_Evaluator_LinkRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISkillQualificationEmpSettingRepository, Data.Repository.Core.SkillQualificationEmpSettingRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISkillQualificationEmp_SignOffRepository, Data.Repository.Core.SkillQualificationEmp_SignOffRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISkillReQualificationEmp_QuestionAnswerRepository, Data.Repository.Core.SkillReQualificationEmp_QuestionAnswerRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISkillReQualificationEmp_SuggestionRepository, Data.Repository.Core.SkillReQualificationEmp_SuggestionRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISkillReQualificationEmp_StepRepository, Data.Repository.Core.SkillReQualificationEmp_StepRepository>();
           
            services.AddTransient<Domain.Interfaces.Repository.Core.ISimulatorScenario_ScriptRepository, Data.Repository.Core.SimulatorScenario_ScriptRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISimulatorScenario_EventRepository, Data.Repository.Core.SimulatorScenario_EventRepository>();
            services.AddTransient<Domain.Interfaces.Repository.Core.ISimulatorScenario_Script_CriteriaRepository, Data.Repository.Core.SimulatorScenario_Script_CriteriaRepository>();


        }
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(typeof(WkHtmlToPdfDotNet.Contracts.ITools), typeof(WkHtmlToPdfDotNet.PdfTools));
            services.AddSingleton(typeof(WkHtmlToPdfDotNet.Contracts.IConverter), new WkHtmlToPdfDotNet.SynchronizedConverter(new WkHtmlToPdfDotNet.PdfTools()));
            services.AddSingleton<QTD2.Infrastructure.Exporting.Interfaces.ICSVExporter, QTD2.Infrastructure.Exporting.CSVExporter>();
            services.AddSingleton<IAuthorizationHandler, ClientAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstanceAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, UserAuthorizationHandler>();

            services.AddTransient<Infrastructure.Notification.Interfaces.INotificationFactory, Infrastructure.Notification.DefaultNotificationFactory>();
            services.AddTransient<Infrastructure.Notification.Interfaces.INotifierFactory, Infrastructure.Notification.DefaultNotifierFactory>();
            services.AddTransient<Infrastructure.Notification.Content.IContentGeneratorFactory, Infrastructure.Notification.Content.DefaultContentGeneratorFactory>();
            services.AddTransient<Infrastructure.JWT.IJWTBuilder, Infrastructure.JWT.JWTBuilder>();
            services.AddTransient<Infrastructure.Hashing.Interfaces.IHasher, Infrastructure.Hashing.HashIdsHasher>();
            services.AddSingleton(typeof(Infrastructure.Identity.Settings.ClaimsBuilderOptions));

            services.AddTransient<Infrastructure.Helpers.Interfaces.IImageProvider, Infrastructure.Helpers.Implementations.ImageProvider>();
        }

        public static void AddAuthInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<Infrastructure.Identity.Interfaces.IClaimsBuilderFactory, Infrastructure.Identity.AuthClaimsBuilderFactory>();
            services.AddScoped<Infrastructure.Database.Interfaces.IInstanceFetcher, Infrastructure.Database.LocalInstanceFetcherService>();

            services.AddTransient<Infrastructure.Metrics.Interfaces.IMetricLogger, Infrastructure.Metrics.MetricLogger.RemoteHostedMetricLogger>();
        }

        public static void AddQTDInstanceFetcher(this IServiceCollection services, IConfiguration configuration)
        {
            QTD2.Infrastructure.QTDSettings.QTDSettings settings = new Infrastructure.QTDSettings.QTDSettings();
            configuration.GetSection("QTDSettings").Bind(settings);

            if (settings.UseRemote)
            {
                services.AddScoped<IInstanceFetcher, RemoteInstanceFetcherService>();
            }
            else
            {
                services.AddScoped<IInstanceFetcher, LocalInstanceFetcherService>();
            }
        }

        public static void AddQTDInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<Infrastructure.Identity.Interfaces.IClaimsBuilderFactory, Infrastructure.Identity.QTDClaimsBuilderFactory>();
            services.AddTransient<Infrastructure.Reports.Interfaces.IReportExporterFactory, Infrastructure.Reports.Export.DefaultReportExporterFactory>();
            services.AddTransient<Infrastructure.Reports.Interfaces.IReportGenerator, Infrastructure.Reports.Generation.ReportGenerator>();
            services.AddTransient<Infrastructure.Reports.Interfaces.IReportModelFactory, Infrastructure.Reports.Generation.ReportModelFactory>();
            services.AddTransient<Infrastructure.Reports.Interfaces.IReportContentGenerator, Infrastructure.Reports.Generation.ReportContentGenerator>();

            services.AddTransient<Infrastructure.Reports.Generation.Generators.Helpers.Interfaces.ICertificationReportHelper, Infrastructure.Reports.Generation.Generators.Helpers.CertificationReportHelper>();

            services.AddTransient<Infrastructure.Reports.Generation.Generators.RecordOfTaskEOQualificationGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.AnnualPositionsTaskListReviewGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TaskByPositionModelGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.IDPReviewCompletionHistoryModelGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.ReportScheduleByClassModelGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.ILAByProvidersModelGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TasksByTaskGroupModelGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.StudentEvaluationFormModelGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.MyDataPositionDetailsGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EmployeeByPositionGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.MyDataPositionLinkagesGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EmployeeByOrganizationGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EmployeePositionHistoryGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EmployeeCertificationHistoryGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EmployeeTrainingNeedsAssessmentGenerators>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.ILACompletionHistoryGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.ClassesByILAGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.ILALessonPlanGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TaskQualificationEvaluatorsGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.StudentEvalutationResultsGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TaskRequalificationByPositionGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TrainingQualificationRecordsGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TestItemsGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TestSpecificationsGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TaskRequalificationByEmployeeGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.ListOfTaskEvaluatorsGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.StudentEvalutationResultsInstructorGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.ListOfCertifiedOperatorsGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TrainingSummaryByPositionGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.OJTGuideByPositionGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.OJTGuideByTaskGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.OJTGuideByILAGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TaskQualificationSheetsByPositionGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TaskQualificationSheetsByTaskGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TaskQualificationSheetsByILAGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.ProcedureReviewCompletionHistoryGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TrainingProgramReviewGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EnablingObjectivesByPositionGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EnablingObjectivesByTaskGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.ClassRosterGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TaskandEOByILAGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EOPHoursForDesignatedYearsGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TaskHistoryByPositionGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TrainingMaterialForTaskEOByPositionsGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.InitialTrainingByPositionGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EmployeeTrainingStatusGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TrainingModuleCompletionHistoryGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TrainingModuleCompletionHistoryByEmployeeGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TrainingProgramCompletionHistoryGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.ClassSignInSheetGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.DIFSurveyBlankFormGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.DIFSurveyIndividualFeedbackGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.DIFSurveyAggregateResultsGenerator>();

            services.AddTransient<Infrastructure.Reports.Generation.Generators.YearToDateHoursForCertifiedEmployeesGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TotalNERCCEHsForTheYearToDateGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EmployeeTrainingTowardNercRecertificationGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EmployeeTrainingTowardCertAndAllRequiredTrainingGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EmployeeDelinquencyReportGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.ProcedureReviewCompletionHistorybyEmployeeGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TasksMetbyPositionGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TasksMetbyEmployeeGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EmployeeActiveInactiveHistoryGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.ReliabilityRelatedTaskImpactMatrixR5Generator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EMPTestSummarybyClassesGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EmployeeTrainingTowardCertAndAllRequiredTrainingSummaryGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EMPTaskQualificationDetailsGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EmployeeCourseScheduleforGivenYearGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EmployeeIDPCompletionStatusReportGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TaskHistoryByTaskGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EmployeeTrainingTowardProceduresAndRegulatoryRequirementsGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.ProcedureAndRegulatoryRequirementTrainingSummarybyPositionGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.SummaryOfTaskQualificationBySubDutyAreaGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EmployeeTaskQualificationDatesByTaskGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EmployeeTaskQualificationRecordsForGivenPositionGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EMPTestStatisticsGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.SCORMCompletionSummaryByClassesGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.SCORMTestCompletionStatisticsGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.OJTTrainingLogGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.ProceduresByIssuingAuthorityGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.ProceduresByTaskGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EnablingObjectivesNotLinkedToTaskGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EnablingObjectivesNotLinkedToILAGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TasksByDutyAreaGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EnablingObjectivesByCategoryGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TestReportPaperBasedVersionGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TasksByEnablingObjectivesGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.IlasByTaskGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.IlasByEnablingObjectiveGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.IlasByPositionGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.SkillQualificationTrainingGuideByPositionOrSkillGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.SkillQualificationAssessmentByPositionOrTaskGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.SafetyHazardsByPositionMatrixGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.SafetyHazardsByCategoryGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.ClassCertificatesGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.PretestAndFinalTestComparisonGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.CertifiedEmployeesforGivenCertificateGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TasksNotLinkedToILAGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TasksNotLinkedToPositionGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TasksByProcedureGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.ILAsByMetaILAGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TasksByPositionMatrixGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TestListGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.SafetyHazardsByTaskGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EnablingObjectivesByPositionMatrixGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.EnablingObjectivesbySafetyHazardGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.NERCILAApplicationReportVersionGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TrainingIssuesListGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TrainingIssueDetailsGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TrainingIssuesActionItemsGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.TrainingProgramQualificationCardGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.ProceduresByEnablingObjectivesGenerator>();
            services.AddTransient<Infrastructure.Reports.Generation.Generators.ILAsBySafetyHazardGenerator>();

            //Add Authorization Handler
            services.AddSingleton<IAuthorizationHandler, ToolCategory_StatusHistoryAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler, ILACertificationLinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TaskReQualificationEmp_SignOffAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler, TaskReQualificationEmp_QuestionAnswerAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler, TaskReQualificationEmp_StepsAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ILACertificationLinkAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler, ILACertificationSubRequirementLinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ProcedureReview_EmployeeAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TaskReQualificationEmp_SuggestionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ProcedureReview_EmployeeAuthorizationHandler>();


            services.AddSingleton<IAuthorizationHandler, ProcedureReviewAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler, PersonAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, CertificationAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, CertifyingBodyAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ClientUserAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, DutyAreaAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EmployeeAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EmployeeCertificationCertificationAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EmployeeOrganizationAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EmployeePositionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EnablingObjective_CategoryAuthorizationHandler>();
            //services.AddSingleton<IAuthorizationHandler, EnablingObjective_Procedure_LinkAuthorizationHandler>();
            //services.AddSingleton<IAuthorizationHandler, EnablingObjective_SaftyHazard_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EnablingObjective_SubCategoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EnablingObjective_TopicAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EnablingObjectiveAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, OrganizationAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, PositionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Procedure_EnablingObjective_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Procedure_IssuingAuthorityAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Procedure_SaftyHazard_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ProcedureAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SaftyHazard_AbatementAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SaftyHazard_CategoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SaftyHazard_ControlAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SaftyHazardAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SubdutyAreaAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Task_EnablingObjective_LinkAuthorizationHandler>();
            //services.AddSingleton<IAuthorizationHandler, Task_Procedure_LinkAuthorizationHandler>();
            //services.AddSingleton<IAuthorizationHandler, Task_SaftyHazard_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Task_StepAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Task_ToolAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TaskAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ToolAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ToolGroup_ToolAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ToolGroupAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TrainingProgramAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_TaskAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_Task_QuestionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_ProcedureAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_Task_Procedure_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_ToolAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_Task_Tool_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_Procedure_Tool_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_EnablingObjective_Tool_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_EnablingObjectiveAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_SaftyHazardAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_SaftyHazard_AbatementAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_SaftyHazard_ControlAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_Task_EnablingObjective_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_Task_SaftyHazard_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_Procedure_SaftyHazard_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_EnablingObjective_SaftyHazard_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_EnablingObjective_Procedure_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_Procedure_EnablingObjective_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Employee_TaskAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TimesheetAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Task_QuestionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Task_PositionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ProviderLevelAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ProviderAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ILA_TopicAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, DeliveryMethodAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TrainingTopicAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, NercStandardAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TraineeEvaluationTypeAuthorizationHandller>();
            services.AddSingleton<IAuthorizationHandler, MetaILAAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SegmentAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, AssessmentToolAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, RR_IssuingAuthorityAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, RegulatoryRequirementAuthorizationHandler>();
            //services.AddSingleton<IAuthorizationHandler, RR_SafetyHazard_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ILAAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TrainingTopicAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, StudentEvaluationFormAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, NercStandardMemberAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, CustomEnablingObjectiveAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TaxonomyLevelAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TestStatusAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TestAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TestTypeAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TestSettingAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TestItemTypeAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, CoversheetAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, CoverSheetTypeAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TestItemAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TestItemMatchAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TestItemMCQAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TestItemTrueFalseAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TestItemFillBlankAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ILATraineeEvaluationAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TestItemShortAnswerAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Proc_IssuingAuthority_HistoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Procedure_Task_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Procedure_StatusHistoryAuthorizationHandler>();
            //services.AddSingleton<IAuthorizationHandler, RR_Procedure_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, RR_StatusHistoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SafetyHazard_EO_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SafetyHazard_Task_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SafetyHazard_SetAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SafetyHazard_Set_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SafetyHazard_HistoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SafetyHazard_CategoryHistoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Task_ReferenceAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Task_Reference_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Task_ILA_LinkAuthorizationHandler>();
            //services.AddSingleton<IAuthorizationHandler, Task_RR_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Task_CollaboratorInvitationAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Task_Collaborator_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, DiscussionQuestionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, RatingScaleAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, StudentEvaluationQuestionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EnablingObjective_CategoryHistoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, RR_IssuingAuthority_StatusHistoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EnablingObjective_TopicHistoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EnablingObjective_SubCategoryHistoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EnablingObjectiveHistoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ILAHistoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ILA_StudentEvaluation_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ToolCategoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Tool_StatusHistoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, StudentEvaluationAudienceAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, StudentEvaluationAvailabilityAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SimulatorScenarioAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Task_HistoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Task_MetaTask_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Task_SuggestionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TrainingGroup_CategoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TrainingGroupAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Task_TrainingGroupAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_ILAAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_Task_ILA_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, PositionHistoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Position_TaskAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, PositonsSQAuthorizeHandler>();
            services.AddSingleton<IAuthorizationHandler, Position_EmployeeAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EnablingObjective_MetaEO_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TestItem_HistoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_EnablingObjective_TaskAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_EnablingObjective_ILALinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_EnablingObjective_RRLinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_EnablingObjective_MetaEOLinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EnablingObjective_StepAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EnablingObjective_Employee_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_PositionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_EnablingObjective_Position_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_EmployeeAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_EnablingObjective_Employee_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EnablingObjective_QuestionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_EnablingObjective_QuestionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_TestItemsAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_Task_SuggestionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_Task_Position_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_EnablingObjective_StepAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EnablingObjective_SuggestionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EnablingObjective_ToolAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_EnablingObjective_SuggestionsAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EmployeeCertificationHistoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, CertificationIssuingAuthorityAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ActivityNotificationAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EmployeeDocumentAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EmployeeHistoryAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler, ClientSettings_NotificationAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler, TrainingProgramTypeAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TrainingProgramILALinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TrainingProgram_HistoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_TrainingProgramAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_TrainingProgram_ILA_LinkAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler, RatingScaleNAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, StudentEvaluationAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, StudentEvaluationHistoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, QuestionBankAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, QuestionBankHistoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, StudentEvaluation_QuestionAutorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_TestStausAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_TestAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Test_HistoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, DutyArea_HistoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SubDutyArea_HistoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EvaluationMethodAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TaskQualificationAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TaskQualification_Evaluator_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TaskQualificationStatusAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TQEmpSettingAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler, IDPAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, IDP_ReviewStatusAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, IDP_ReviewAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler, ClientUserSettings_Dashboard_SettingAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Dashboard_SettingAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ClientSettings_GeneralSettingAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ClientSettings_LabelReplacementsAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ClientSettings_FeatureAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ClientSettings_LicenseAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler, ReportSkeletonAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ReportSkeletonCategoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ReportAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ClassScheduleAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler, ClassScheduleHistoryAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, CBTAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ScormUploadAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler, EvaluationReleaseEMPSettingsAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TestReleaseEMPSettingsAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TestReleaseEMPSetting_Retake_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SelfRegistrationOptionsAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ClassScheduleEmployeeAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ClassSchedule_RosterAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TQILAEmpSettingAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ILA_Evaluator_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ClassSchedule_Roster_StatusesAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ClassSchedule_RecurrenceAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ClassSchedule_StudentEvaluations_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, StudentEvaluationWithoutEmpAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, IDPAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, IDP_ReviewStatusAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, IDP_ReviewAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ClientUserSettings_Dashboard_SettingAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Dashboard_SettingAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ClientSettings_GeneralSettingAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ClientSettings_LabelReplacementsAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler, ClientSettings_LicenseAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, EmpTestAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ClassSchedule_Evaluation_RosterAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ClassSchedule_SelfRegAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, RatingScaleExpandedAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_Task_MetaTask_LinkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, MetaILAConfigurationPublishOptionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, Version_MetaILAAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, MetaILA_StatusAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ILA_PerformTraineeEvalAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, CBT_ScormRegistrationAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, MetaILA_SummaryTestAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TrainingProgramReviewAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_DesignDefaultViewAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILA_DesignAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILA_DevelopAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILA_ImplementAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, CBT_ScormRegistrationAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILADesign_DelieveryMethodsAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILADesign_EnablingObjectivesAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILADesign_NERCAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILADesign_PrerequistieAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILADesign_ProceduresAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILADesign_ResourcesAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILADesign_SafetyHazardsAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILADesign_SegmentsAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILADesign_TargetAudienceAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILADesign_TasksAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILADesign_TrainingTopicsAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILADesignReviewersAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILADevelopReviewersAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILAEvaluationAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILAEvaluation_DefaultViewAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILAEvaluation_TestAnalysisAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILAEvaluation_TrainingIssuesAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILAImplement_ClassScheduleAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILAImplementReviewersAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ILAPhasesAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_PhasesAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ProspectiveILAAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ProspectiveILA_ArchivesAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_ProspectiveILA_SnapshotAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_Segments_LinkObjectivesAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_Segments_NercStandardsAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_TrainingTopicsAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, InstructorWorkbook_TrainingTopicsHeadingAuthorizationHandler>();


            services.AddSingleton<IAuthorizationHandler, DIFSurveyAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, DIFSurvey_DevStatusAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, DIFSurvey_EmployeeAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, DIFSurvey_Employee_StatusAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, DIFSurvey_Employee_ResponseAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, DIFSurvey_TaskAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, DIFSurvey_Task_StatusAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, DIFSurvey_Task_TrainingFrequencyHandler>();

            services.AddSingleton<IAuthorizationHandler, EmpSettingsReleaseTypeAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, QTDUserAuthorizationHandler>();


            services.AddSingleton<IAuthorizationHandler, QTDUserAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler, TaskListReviewHandler>();
            services.AddSingleton<IAuthorizationHandler, TaskListReview_GeneralReviewerHandler>();
            services.AddSingleton<IAuthorizationHandler, TaskListReview_TypeHandler>();
            services.AddSingleton<IAuthorizationHandler, TaskListReview_StatusHandler>();
            services.AddSingleton<IAuthorizationHandler, TaskReviewHandler>();
            services.AddSingleton<IAuthorizationHandler, TaskReview_ReviewerHandler>();
            services.AddSingleton<IAuthorizationHandler, TaskReview_FindingHandler>();
            services.AddSingleton<IAuthorizationHandler, TaskReview_StatusHandler>();
            services.AddSingleton<IAuthorizationHandler, ActionItemHandler>();
            services.AddSingleton<IAuthorizationHandler, ActionItem_PriorityHandler>();
            services.AddSingleton<IAuthorizationHandler, ActionItem_SubDuty_OperationHandler>();
            services.AddSingleton<IAuthorizationHandler, ActionItem_Step_Operationhandler>();
            services.AddSingleton<IAuthorizationHandler, ActionItem_Suggestion_OperationHandler>();
            services.AddSingleton<IAuthorizationHandler, ActionItem_QuestionAndAnswer_OperationHandler>();
            services.AddSingleton<IAuthorizationHandler, ActionItem_Tool_OperationHandler>();
            services.AddSingleton<IAuthorizationHandler, ActionItem_EnablingObjective_OperationHandler>();
            services.AddSingleton<IAuthorizationHandler, ActionItem_Procedure_OperationHandler>();
            services.AddSingleton<IAuthorizationHandler, ActionItem_RegulatoryRequirement_Operationhandler>();
            services.AddSingleton<IAuthorizationHandler, ActionItem_SafetyHazard_OperationHandler>();
            services.AddSingleton<IAuthorizationHandler, ActionItem_OperationType_LinksHandler>();
            services.AddSingleton<IAuthorizationHandler, ActionItem_OperationTypesHandler>();

            services.AddSingleton<IAuthorizationHandler, SimulatorScenarioAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SimulatorScenario_DifficultyAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SimulatorScenario_PositionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SimulatorScenario_TaskAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SimulatorScenario_EnablingObjectiveAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SimulatorScenario_ProcedureAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SimulatorScenario_Task_CriteriaAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SimulatorScenario_EventAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SimulatorScenario_Script_CriteriaAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SimulatorScenario_ILAAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SimulatorScenario_PrerequisiteAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SimulatorScenario_CollaboratorAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SimulatorScenario_CollaboratorPermissionAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler, TrainingIssueHandler>();
            services.AddSingleton<IAuthorizationHandler, TrainingIssue_ActionItemHandler>();
            services.AddSingleton<IAuthorizationHandler, TrainingIssue_ActionItemPriorityHandler>();
            services.AddSingleton<IAuthorizationHandler, TrainingIssue_ActionItemStatusHandler>();
            services.AddSingleton<IAuthorizationHandler, TrainingIssue_DriverSubTypeHandler>();
            services.AddSingleton<IAuthorizationHandler, TrainingIssue_DriverTypeHandler>();
            services.AddSingleton<IAuthorizationHandler, TrainingIssue_SeverityHandler>();
            services.AddSingleton<IAuthorizationHandler, TrainingIssue_StatusHandler>();
            services.AddSingleton<IAuthorizationHandler, ClassSchedule_Roster_TimeRecordHandler>();

            services.AddSingleton<IAuthorizationHandler, ClassSchedule_TestReleaseEMPSettingsAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ClassSchedule_TestReleaseEMPSetting_Retake_LinksAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler, ClassSchedule_TQEMPSettingsAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ClassSchedule_Evaluator_LinksAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler, PersonActivityNotificationHandler>();
           
            services.AddSingleton<IAuthorizationHandler, ClassScheduleEmployee_ILACertificationLink_PartialCreditHandler>();
            services.AddSingleton<IAuthorizationHandler, ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditHandler>();

            services.AddSingleton<IAuthorizationHandler, SkillQualificationHandler>();
            services.AddSingleton<IAuthorizationHandler, SkillQualificationStatusHandler>();
            services.AddSingleton<IAuthorizationHandler, SkillQualification_Evaluator_LinkHandler>();
            services.AddSingleton<IAuthorizationHandler, SkillQualificationEmpSettingHandler>();

            services.AddSingleton<IAuthorizationHandler, SimulatorScenario_ScriptAuthorizationHandler>();
        }

        public static void AddAuthenticationApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<Interfaces.Services.Authentication.IUserService, Services.Authentication.UserService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IInstanceService, InstanceService>();
            services.AddTransient<Application.Interfaces.Services.Authentication.INotificationService, Application.Services.Authentication.NotificationService>();
            services.AddTransient<Interfaces.Services.Shared.IUserService, Services.Shared.UserService>();
            //services.AddTransient<Interfaces.Services.Shared.IClientSettingsService, Services.Shared.ClientSettingsService>();
            //services.AddTransient<Domain.Interfaces.Service.Core.IClientSettings_NotificationService,Domain.Services.Core.ClientSettings_NotificationService>();
            //services.AddTransient<QTD2.Domain.Interfaces.Service.Core.IClientSettings_NotificationService, QTD2.Domain.Services.Core.ClientSettings_NotificationService>();
            //services.AddTransient<QTD2.Domain.Interfaces.Service.Core.IClientSettings_NotificationService, QTD2.Domain.Services.Core.ClientSettings_NotificationService>();
            //services.AddTransient<QTD2.Domain.Interfaces.Service.Core.IClientSettings_NotificationService, QTD2.Domain.Services.Core.ClientSettings_NotificationService>();
            services.AddTransient<IIdentityProviderService, IdentityProviderService>();
            services.AddTransient<IInstanceIdentityProviderLinkService, InstanceIdentityProviderLinkService>();
            services.AddTransient<IAuthenticationSettingService, AuthenticationSettingService>();
            services.AddTransient<IAdminMessageAuthService, AdminMessageAuthService>();
        }

        public static void AddQTDApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();

            services.AddTransient<QTD2.Application.Interfaces.Factories.ITreeItemViewModelFactory, QTD2.Application.Factories.TreeItemViewModelFactory>();

            services.AddTransient<Application.Interfaces.Services.QTD.INotificationService, Application.Services.QTD.NotificationService>();
            services.AddTransient<Application.Interfaces.Services.QTD.IJobNotificationService, Application.Services.QTD.JobNotificationService>();

            services.AddTransient<Interfaces.Services.Shared.IUserService, Services.Shared.UserService>();

            services.AddTransient<ICertificationService, CertificationService>();
            services.AddTransient<ICertificationHistoryService, CertificationHistoryService>();

            services.AddTransient<ICertifyingBodiesService, CertiyfingBodiesService>();
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IClientUserService, ClientUserServices>();
            services.AddTransient<IDocumentService, DocumentService>();
            services.AddTransient<IDocumentTypeService, DocumentTypeService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IOrganizationService, OrganizationService>();
            services.AddTransient<IPositionService, PositionService>();
            services.AddTransient<ITrainingProgramService, TrainingProgramService>();
            services.AddTransient<IEnablingObjectiveService, EnablingObjectiveService>();
            services.AddTransient<IProcedureService, ProcedureService>();
            services.AddTransient<IStudentEvaluationWithoutEmpService, StudentEvaluationWithoutEmpService>();
            services.AddTransient<ISaftyHazardService, SaftyHazardService>();
            services.AddTransient<ISaftyHazard_CategoryService, SaftyHazard_CategoryService>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<IToolService, ToolService>();
            services.AddTransient<IEmployeeTaskService, EmployeeTaskService>();
            services.AddTransient<IVersioningService, VersioningService>();
            services.AddTransient<IProviderLevelService, ProviderLevelService>();
            services.AddTransient<IProviderService, ProviderService>();
            services.AddTransient<IILA_TopicService, ILA_TopicService>();
            services.AddTransient<IDeliveryMethodService, DeliveryMethodService>();
            services.AddTransient<ITrainingTopicService, TrainingTopicService>();
            services.AddTransient<INercStandardService, NercStandardService>();
            services.AddTransient<ITraineeEvaluationTypeService, TraineeEvaluationTypeService>();
            services.AddTransient<IMetaILAService, MetaILAService>();
            services.AddTransient<ISegmentService, SegmentService>();
            services.AddTransient<IAssessmentToolService, AssessmentToolService>();
            services.AddTransient<IRR_IssuingAuthorityService, RR_IssuingAuthorityService>();
            services.AddTransient<IRegulatoryRequirementService, RegulatoryRequirementService>();
            services.AddTransient<IILAService, ILAService>();
            services.AddTransient<IILAResourceService, ILAResourceService>();
            services.AddTransient<IILAApplicationService, ILAApplicationService>();
            services.AddTransient<IScormHelpersService, ScormHelpersService>();
            services.AddTransient<ITrainingTopic_CategoryService, TrainingTopic_CategoryService>();
            services.AddTransient<INERCTargetAudienceService, NERCTargetAudienceService>();
            services.AddTransient<IRatingScaleService, RatingScaleService>();
            services.AddTransient<IStudentEvaluationFormService, StudentEvaluationFormService>();
            services.AddTransient<IStudentEvaluationAvailabilityService, StudentEvaluationAvailabilityService>();
            services.AddTransient<ICoverSheetTypeService, CoverSheetTypeService>();
            services.AddTransient<IStudentEvaluationQuestionService, StudentEvaluationQuestionService>();
            services.AddTransient<INercStandardMemberService, NercStandardMemberService>();
            services.AddTransient<ICollaboratorInvitationService, CollaboratorInvitationService>();
            services.AddTransient<ICoversheetService, CoversheetService>();
            services.AddTransient<ICustomEnablingObjectiveService, CustomEnablingObjectiveService>();
            services.AddTransient<IStudentEvaluationAudienceService, StudentEvaluationAudienceService>();
            services.AddTransient<ITaxonomyLevelService, TaxonomyLevelService>();
            services.AddTransient<ITestStatusService, TestStatusService>();
            services.AddTransient<ITestService, TestService>();
            services.AddTransient<ITestTypeService, TestTypeService>();
            services.AddTransient<ITestSettingService, TestSettingService>();
            services.AddTransient<ITestItemTypeService, TestItemTypeService>();
            services.AddTransient<ITestItemService, TestItemService>();
            services.AddTransient<ITestItemMatchService, TestItemMatchService>();
            services.AddTransient<ITestItemMCQService, TestItemMCQService>();
            services.AddTransient<ITestItemTrueFalseService, TestItemTrueFalseService>();
            services.AddTransient<ITestItemFillBlankService, TestItemFillBlankService>();
            services.AddTransient<IILATraineeEvaluationService, ILATraineeEvaluationService>();
            services.AddTransient<ITestItemShortAnswerService, TestItemShortAnswerService>();
            services.AddTransient<IProc_IssuingAuthority_HistoryService, Proc_IssuingAuthority_HistoryService>();
            services.AddTransient<IRR_StatusHistoryService, RR_StatusHistoryService>();
            services.AddTransient<ISafetyHazard_SetService, SafetyHazard_SetService>();
            services.AddTransient<ISafetyHazard_HistoryService, SafetyHazard_HistoryService>();
            services.AddTransient<ISafetyHazard_CategoryHistoryService, SafetyHazard_CategoryHistoryService>();
            services.AddTransient<ITask_ReferenceService, Task_ReferenceService>();
            services.AddTransient<ITask_CollaboratorInvitationService, Task_CollaboratorInvitationService>();
            services.AddTransient<IDiscussionQuestionService, DiscussionQuestionService>();
            services.AddTransient<IEnablingObjective_CategoryHistoryService, EnablingObjective_CategoryHistoryService>();
            services.AddTransient<IEnablingObjective_TopicHistoryService, EnablingObjective_TopicHistoryService>();
            services.AddTransient<IEnablingObjective_SubCategoryHistoryService, EnablingObjective_SubCategoryHistoryService>();
            services.AddTransient<IEnablingObjectiveHistoryService, EnablingObjectiveHistoryService>();
            services.AddTransient<IILAHistoryService, ILAHistoryService>();
            services.AddTransient<ITool_StatusHistoryService, Tool_StatusHistoryService>();
            services.AddTransient<IProcedureStatusHistoryService, ProcedureStatusHistoryService>();
            services.AddTransient<IInstructorService, InstructorService>();
            services.AddTransient<IInstructor_CategoryService, Instructor_CategoryService>();
            services.AddTransient<IInstructorHistoryService, InstructorHistoryService>();
            services.AddTransient<IInstructor_CategoryHistoryService, Instructor_CategoryHistoryService>();
            services.AddTransient<ITask_HistoryService, Task_HistoryService>();

            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<ILocation_CategoryService, Location_CategoryService>();
            services.AddTransient<ILocationHistoryService, LocationHistoryService>();
            services.AddTransient<ILocation_CategoryHistoryService, Location_CategoryHistoryService>();
            services.AddTransient<IVersion_Task_EnablingObjective_LinkService, Version_Task_EnablingObjective_LinkService>();
            services.AddTransient<IVersion_Task_Procedure_LinkService, Version_Task_Procedure_LinkService>();
            services.AddTransient<IVersion_Task_SaftyHazard_LinkService, Version_Task_SaftyHazard_LinkService>();
            services.AddTransient<ITrainingGroupService, TrainingGroupService>();
            services.AddTransient<IPositionHistoryService, PositionHistoryService>();
            services.AddTransient<ITestItem_HistoryService, TestItem_HistoryService>();
            services.AddTransient<IVersion_EnablingObjectiveService, Version_EnablingObjectiveService>();
            services.AddTransient<IVersion_PositionService, Version_PositionService>();
            services.AddTransient<IVersion_TaskService, Version_TaskService>();
            services.AddTransient<IEmployeeCertificationHistoryService, EmployeeCertificationHistoryService>();
            services.AddTransient<ICertificationIssuingAuthorityService, CertificationIssuingAuthorityService>();
            services.AddTransient<IActivityNotificationService, ActivityNotificationService>();
            services.AddTransient<IDashboardService, DashboardService>();
            services.AddTransient<IEmployeeHistoryService, EmployeeHistoryService>();

            services.AddTransient<IClientSettingsService, ClientSettingsService>();
            services.AddTransient<ITrainingProgramTypeService, TrainingProgramTypeService>();
            services.AddTransient<ITrainingProgram_HistoryService, TrainingProgram_HistoryService>();

            services.AddTransient<ITrainingProgramTypeService, TrainingProgramTypeService>();
            services.AddTransient<ITrainingProgram_HistoryService, TrainingProgram_HistoryService>();

            services.AddTransient<IStudentEvaluationService, StudentEvaluationService>();
            services.AddTransient<ITestSchedulerService, TestSchedulerService>();
            services.AddTransient<IRatingScaleNService, RatingScaleNService>();
            services.AddTransient<IStudentEvaluationQuestionService, StudentEvaluationQuestionService>();
            services.AddTransient<IStudentEvaluationHistoryService, StudentEvaluationHistoryService>();
            services.AddTransient<IQuestionBankService, QuestionBankService>();
            services.AddTransient<IQuestionBankHistoryService, QuestionBankHistoryService>();
            services.AddTransient<IVersion_TestService, Version_TestService>();
            services.AddTransient<ITest_HistoryService, Test_HistoryService>();
            services.AddTransient<IDutyArea_HistoryService, DutyArea_HistoryService>();
            services.AddTransient<ISubDutyArea_HistoryService, SubDutyArea_HistoryService>();
            services.AddTransient<ITaskRequalificationService, TaskRequalificationService>();
            services.AddTransient<IEvaluationMethodService, EvaluationMethodService>();
            services.AddTransient<IClassScheduleService, ClassScheduleService>();
            services.AddTransient<IPublicClassScheduleRequestService, PublicClassScheduleRequestService>();
            services.AddTransient<IClassScheduleHistoryService, ClassScheduleHistoryService>();
            services.AddTransient<ITestReleaseEmpSettingsService, TestReleaseEmpSettingsService>();
            services.AddTransient<IClassRosterService, ClassRosterService>();
            services.AddTransient<IIDPService, IDPService>();
            services.AddTransient<IStudentEvaluationWithoutEmpService, StudentEvaluationWithoutEmpService>();
            services.AddTransient<IClientUserSettingsService, ClientUserSettingsService>();
            //services.AddTransient<IEmpTestService, EmpTestService>();

            services.AddTransient<IReportsService, ReportService>();
            services.AddTransient<IReportGeneratorService, ReportGeneratorService>();
            services.AddTransient<IReportSkeletonService, ReportSkeletonService>();
            services.AddTransient<IReportSkeletonCategoriesService, ReportSkeletonCategoriesService>();
            services.AddTransient<IProcedureReviewService, ProcedureReviewService>();
            services.AddTransient<IProcedureReviewEmpService, ProcedureReviewEmpService>();
            services.AddTransient<IMetaILAConfigurationPublishOptionService, MetaILAConfigurationPublishOptionService>();
            services.AddTransient<IVersion_MetaILAService, Version_MetaILAService>();
            services.AddTransient<IMetaILA_StatusService, MetaILA_StatusService>();
            services.AddTransient<ICertificationSubRequirementService, CertificationSubRequirementService>();
            services.AddTransient<ITaskReQualificationEmp_SuggestionService, TaskReQualificationEmp_SuggestionService>();
            services.AddTransient<ITaskReQualificationEmp_StepService, TaskReQualificationEmp_StepService>();
            services.AddTransient<ITaskReQualificationEmp_QuestionAnswerService, TaskReQualificationEmp_QuestionAnswerService>();
            services.AddTransient<ITaskReQualificationEmp_SignOffService, TaskReQualificationEmp_SignOffService>();
            services.AddTransient<IEMPReleaseCheckService, EMPReleaseCheckService>();
            services.AddTransient<IOnlineCoursesService, OnlineCoursesService>();
            services.AddTransient<IPositionTaskService, PositionTaskService>();
            services.AddTransient<ITrainingProgramReviewService, TrainingProgramReviewService>();
            services.AddTransient<ITrainingProgramReviewService, TrainingProgramReviewService>();
            services.AddScoped<IEmployeeSummaryService, EmployeeSummaryService>();
            services.AddTransient<IMetaILA_SummaryTestService, MetaILA_SummaryTestService>();
            services.AddScoped<INercService, NercService>();
            services.AddTransient<IDIFSurveyService, DIFSurveyService>();
            services.AddTransient<IDIFSurveyTaskTrainingFrequencyService, DIFSurveyTaskTrainingFrequencyService>();
            services.AddTransient<IDIFSurveyTaskStatusService, DIFSurveyTaskStatusService>();
            services.AddTransient<IImportService, ImportService>();
            services.AddTransient<IQTDService, QTDService>();
            services.AddTransient<IEmpSettingsReleaseTypeService, EmpSettingsReleaseTypeService>();
            services.AddTransient<IQTDService, QTDService>();
            services.AddTransient<ITaskListReviewService, TaskListReviewService>();
            services.AddTransient<ITaskReviewActionItemService, TaskReviewActionItemService>();
            services.AddTransient<ITaskListReviewTypeService, TaskListReviewTypeService>();
            services.AddTransient<ITaskReviewFindingService, TaskReviewFindingService>();
            services.AddTransient<ITaskListReviewStatusService, TaskListReviewStatusService>();
            services.AddTransient<ITaskReview_ActionItemPriorityService, TaskReview_ActionItemPriorityService>();
            services.AddTransient<ITaskReviewService, TaskReviewService>();
            services.AddTransient<ISimulatorScenarioDifficultyService, SimulatorScenarioDifficultyService>();
            services.AddTransient<ISimulatorScenarioService, SimulatorScenarioService>();
            services.AddTransient<ISimulatorScenario_CollaboratorService, SimulatorScenario_CollaboratorService>();
            services.AddTransient<ISimulatorScenarioCollaboratorPermissionsService, SimulatorScenarioCollaboratorPermissonService>();
            services.AddTransient<ISimulatorScenarioStatusService, SimulatorScenarioStatusService>();
            services.AddTransient<ITrainingIssueService, TrainingIssueService>();
            services.AddTransient<ITrainingIssueStatusService, TrainingIssueStatusService>();
            services.AddTransient<ITrainingIssueSeverityService, TrainingIssueSeverityService>();
            services.AddTransient<ITrainingIssueActionItemStatusService, TrainingIssueActionItemStatusService>();
            services.AddTransient<ITrainingIssueDriverTypeService, TrainingIssueDriverTypeService>();
            services.AddTransient<ITrainingIssueActionItemPriorityService, TrainingIssueActionItemPriorityService>();
            services.AddTransient<IClassSchedule_Roster_TimeRecordService, ClassSchedule_Roster_TimeRecordService>();
            services.AddTransient<IClassSchedule_TestReleaseEMPSettingsService, ClassSchedule_TestReleaseEMPSettingsService>();
            services.AddTransient<IClassSchedule_TQEMPSettingsService, ClassSchedule_TQEMPSettingsService>();
            services.AddTransient<IClassSchedule_Evaluator_LinksService, ClassSchedule_Evaluator_LinkService>();
            services.AddTransient<IILASegmentLinkService, ILASegmentLinkService>();
            services.AddTransient<ICbtScormRegistrationService, CbtScormRegistrationService>();

            services.AddTransient<IGradeEvaluator, GradeEvaluator>();

            services.AddTransient<ITrainingProgramReview_TrainingIssue_LinkService, TrainingProgramReview_TrainingIssue_LinkService>();
            services.AddTransient<IAdminMessageService, AdminMessageService>();
            services.AddTransient<IAdminMessage_QTDUserService, AdminMessage_QTDUserService>();
            services.AddTransient<ICSE_ILACertLink_PartialCreditService, CSE_ILACertLink_PartialCreditService>();
        }
    }
}
