using MediatR;
using QTD2.Domain.Events.Core;
using QTD2.Domain.Interfaces.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnILATraineeEvaluation_UpdatedHandler : INotificationHandler<OnILATraineeEvaluation_Updated>
    {
        private readonly ITestReleaseEMPSettingsService _testReleaseEMPSettingsService;
        private readonly ITestReleaseEMPSetting_Retake_LinkService _testReleaseEMPSetting_Retake_LinkService;
        private readonly IClassSchedule_TestReleaseEMPSettingsService _classSchedule_TestReleaseEMPSettingsService;
        private readonly IClassSchedule_TestReleaseEMPSetting_Retake_LinksService _classSchedule_TestReleaseEMPSetting_Retake_LinksService;
        public OnILATraineeEvaluation_UpdatedHandler(
            ITestReleaseEMPSettingsService testReleaseEMPSettingsService, 
            ITestReleaseEMPSetting_Retake_LinkService testReleaseEMPSetting_Retake_LinkService,
            IClassSchedule_TestReleaseEMPSettingsService classSchedule_TestReleaseEMPSettingsService,
            IClassSchedule_TestReleaseEMPSetting_Retake_LinksService classSchedule_TestReleaseEMPSetting_Retake_LinksService) 
        {
            _testReleaseEMPSettingsService = testReleaseEMPSettingsService;
            _testReleaseEMPSetting_Retake_LinkService = testReleaseEMPSetting_Retake_LinkService;
            _classSchedule_TestReleaseEMPSettingsService = classSchedule_TestReleaseEMPSettingsService;
            _classSchedule_TestReleaseEMPSetting_Retake_LinksService = classSchedule_TestReleaseEMPSetting_Retake_LinksService;
        }

        public async System.Threading.Tasks.Task Handle(OnILATraineeEvaluation_Updated notification, CancellationToken cancellationToken)
        {
            var testReleaseEmpSettings = await _testReleaseEMPSettingsService.GetTestReleaseEmpSettingByTestIdAsync(notification.ILATraineeEvaluation.TestId);
            var testReleaseEmpSettingRetakeLinks = await _testReleaseEMPSetting_Retake_LinkService.GetTestReleaseEmpSettingRetakeLinksByTestIdAsync(notification.ILATraineeEvaluation.TestId);
            var classTestReleaseEmpSettings = await _classSchedule_TestReleaseEMPSettingsService.GetClassScheduleTestReleaseSettingsByTestIdAsync(notification.ILATraineeEvaluation.TestId);
            var classTestReleaseEmpSettingRetakeLinks = await _classSchedule_TestReleaseEMPSetting_Retake_LinksService.GetClassScheduleTestReleaseEmpSettingRetakeLinksByTestIdAsync(notification.ILATraineeEvaluation.TestId);

            foreach(var testSetting in testReleaseEmpSettings)
            {
                if(testSetting.ILAId != notification.ILATraineeEvaluation.ILAId)
                {
                    if (testSetting.FinalTestId == notification.ILATraineeEvaluation.TestId) testSetting.FinalTestId = null;
                    if (testSetting.PreTestId == notification.ILATraineeEvaluation.TestId) testSetting.PreTestId = null;
                    await _testReleaseEMPSettingsService.UpdateAsync(testSetting);
                }
            }

            foreach(var testRetakeLink in testReleaseEmpSettingRetakeLinks)
            {
                if(testRetakeLink.TestReleaseEMPSettings.ILAId != notification.ILATraineeEvaluation.ILAId)
                {
                    await _testReleaseEMPSetting_Retake_LinkService.DeleteAsync(testRetakeLink);
                }
            }

            foreach(var classTestSetting in classTestReleaseEmpSettings)
            {
                if(classTestSetting.ClassSchedule.ILAID != notification.ILATraineeEvaluation.ILAId)
                {
                    if (classTestSetting.FinalTestId == notification.ILATraineeEvaluation.TestId) classTestSetting.FinalTestId = null;
                    if (classTestSetting.PreTestId == notification.ILATraineeEvaluation.TestId) classTestSetting.PreTestId = null;
                    await _classSchedule_TestReleaseEMPSettingsService.UpdateAsync(classTestSetting);
                }
            }

            foreach(var classTestRetake in classTestReleaseEmpSettingRetakeLinks)
            {
                if(classTestRetake.ClassSchedule_TestReleaseEMPSetting.ClassSchedule.ILAID != notification.ILATraineeEvaluation.ILAId)
                {
                    await _classSchedule_TestReleaseEMPSetting_Retake_LinksService.DeleteAsync(classTestRetake);
                }
            }
        }
    }
}
