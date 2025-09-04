using Microsoft.AspNetCore.Http;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.Identity.Settings;
using QTD2.Test.Data.Infrastructure.Identity.Settings;
using QTD2.Test.Data.Infrastructure.Model.CertifyingBody;
using QTD2.Tests.IntegrationTests.Testing.Base;
using QTD2.Tests.IntegrationTests.Testing.Fixures;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace QTD2.Tests.IntegrationTests.Notifications
{
    [Collection("QTD Collection")]
    public class QTDNotificationTests : QTDControllerBase
    {
        protected Application.Interfaces.Services.QTD.IJobNotificationService _jobNotificationService { get { return (_fixture as QTDFixture).JobNotificationService; } }
        private readonly IDbContextBuilder _dbContextBuilder;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public QTDNotificationTests(QTDFixture qtdFixture, IDbContextBuilder dbContextBuilder, IHttpContextAccessor httpContextAccessor) : base(qtdFixture)
        {
            _dbContextBuilder = dbContextBuilder;
            _httpContextAccessor = httpContextAccessor;
        }


        [Theory]
        [InlineData(1, 1)]
        public async System.Threading.Tasks.Task ClassScheduleNotification(int employeeId, int order)
        {
            await _fixture.StartupAsync();
            var instanceName = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == Domain.CustomClaimTypes.InstanceName).Select(c => c.Value).SingleOrDefault();
            var qtdContext = _dbContextBuilder.BuildQtdContext(instanceName);
            await _jobNotificationService.SendClassScheduleNotification( employeeId, 1, qtdContext);
        }


        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 2, 1)]
        [InlineData(1, 3, 1)]
        public async System.Threading.Tasks.Task CertificationExpirationNotification(int employeeId, int order, int certificationId)
        {
            await _fixture.StartupAsync();
            // await _notificationService.SendCertificationExpirationNotification( order, employeeId, certificationId);
        }

        [Theory]
        [InlineData(1)]
        public async System.Threading.Tasks.Task EmpLoginNotification(int employeeId)
        {
            await _fixture.StartupAsync();
            var instanceName = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == Domain.CustomClaimTypes.InstanceName).Select(c => c.Value).SingleOrDefault();
            var qtdContext = _dbContextBuilder.BuildQtdContext(instanceName);
            await _jobNotificationService.SendEmpLoginNotification( employeeId, 1,"",qtdContext);
        }

        [Theory]
        [InlineData(1, 1)]
        public async System.Threading.Tasks.Task EmpTestNotification(int employeeId, int testId)
        {
            await _fixture.StartupAsync();
            // await _notificationService.SendEmpTestNotification( employeeId, testId, 1);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        public async System.Threading.Tasks.Task EmpPretestNotification(int employeeId, int courseId, int testId)
        {
            await _fixture.StartupAsync();
            // await _notificationService.SendEmpPretestNotification( employeeId, courseId, testId, 1);
        }

        [Theory]
        [InlineData(1, 1)]
        public async System.Threading.Tasks.Task EmpCourseNotification(int employeeId, int courseId)
        {
            await _fixture.StartupAsync();
            var instanceName = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == Domain.CustomClaimTypes.InstanceName).Select(c => c.Value).SingleOrDefault();
            var qtdContext = _dbContextBuilder.BuildQtdContext(instanceName);
            await _jobNotificationService.SendEmpCourseNotification( employeeId, courseId, 1, qtdContext);
        }

        [Theory]
        [InlineData(1, 1)]
        public async System.Threading.Tasks.Task EmpStudentEvaluationNotification(int employeeId, int ilaId)
        {
            await _fixture.StartupAsync();
            // await _notificationService.SendEmpStudentEvaluationNotification( employeeId, ilaId, 1);
        }

        [Theory]
        [InlineData(1, 1)]
        public async System.Threading.Tasks.Task EmpProcedureReviewNotification(int employeeId, int procedureId)
        {
            await _fixture.StartupAsync();
            // await _notificationService.SendEmpProcedureReviewNotification( employeeId, procedureId, 1);
        }

        [Theory]
        [InlineData(1, 1)]
        public async System.Threading.Tasks.Task EmpIDPReviewNotification(int employeeId, int idpId)
        {
            await _fixture.StartupAsync();
            var instanceName = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == Domain.CustomClaimTypes.InstanceName).Select(c => c.Value).SingleOrDefault();
            var qtdContext = _dbContextBuilder.BuildQtdContext(instanceName);
            await _jobNotificationService.SendEmpIDPReviewNotification( employeeId, idpId, 1, qtdContext);
        }

        [Theory]
        [InlineData(1, 1)]
        public async System.Threading.Tasks.Task EmpTaskQualitificationTraineeNotification(int employeeId, int taskId)
        {
            await _fixture.StartupAsync();
            // await _notificationService.SendEmpTaskQualitificationTraineeNotification( employeeId, taskId, 1);
        }

        [Theory]
        [InlineData(1, 1)]
        public async System.Threading.Tasks.Task EmpTaskQualitificationEvaluatorNotification(int employeeId, int taskId)
        {
            await _fixture.StartupAsync();
            // await _notificationService.SendEmpTaskQualitificationEvaluatorNotification( employeeId, taskId, 1);
        }

        [Theory]
        [InlineData(1, 1)]
        public async System.Threading.Tasks.Task EmpSelfRegistrationApprovalNotification(int employeeId, int courseId)
        {
            await _fixture.StartupAsync();
            // await _notificationService.SendEmpSelfRegistrationApprovalNotification( employeeId, courseId, 1);
        }

        [Theory]
        [InlineData(1, 1)]
        public async System.Threading.Tasks.Task EmpSelfRegistrationDenialNotification(int employeeId, int courseId)
        {
            await _fixture.StartupAsync();
            // await _notificationService.SendEmpSelfRegistrationDenialNotification( employeeId, courseId, 1);
        }

        [Theory]
        [InlineData(1, 1)]
        public async System.Threading.Tasks.Task EmpGapSurveyNotification(int employeeId, int surveyId)
        {
            await _fixture.StartupAsync();
            var instanceName = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == Domain.CustomClaimTypes.InstanceName).Select(c => c.Value).SingleOrDefault();
            var qtdContext = _dbContextBuilder.BuildQtdContext(instanceName);
            await _jobNotificationService.SendEmpGapSurveyNotification( employeeId, surveyId, 1, qtdContext);
        }

        [Theory]
        [InlineData(1, 1)]
        public async System.Threading.Tasks.Task EmpDifSurveyNotification(int employeeId, int surveyId)
        {
            await _fixture.StartupAsync();
            var instanceName = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == Domain.CustomClaimTypes.InstanceName).Select(c => c.Value).SingleOrDefault();
            var qtdContext = _dbContextBuilder.BuildQtdContext(instanceName);
            await _jobNotificationService.SendEmpDifSurveyNotification( employeeId, surveyId, 1, qtdContext);
        }
    }
}