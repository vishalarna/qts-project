using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.ClassScheduleEmployee.GradeEvaluation
{
    public class GradeEvaluator : IGradeEvaluator
    {

        IClassScheduleService _classScheduleService;
        IClassSchedule_RosterService _classScheduleRosterService;
        IILAService _ilaService;
        ICBT_ScormRegistrationService _cbtScormRegistrationService;
        IClassSchedule_TestReleaseEMPSettingsService _classScheduleTestReleaseSettingsService;

        ClassSchedule _classSchedule;
        List<ClassSchedule_Roster> _tests;
        CBT_ScormRegistration _cbt_scormRegistration;
        ILA _ila;
        ClassSchedule_TestReleaseEMPSetting _testSettings;


        bool _cbtRequiredForCourse;
        bool _finalTestRequired;
        int _retakesAllowed;

        public GradeEvaluator(
            IClassScheduleService classScheduleService,
            IClassSchedule_RosterService classScheduleRosterService,
            IILAService ilaService,
            ICBT_ScormRegistrationService cbtScormRegistrationService,
            IClassSchedule_TestReleaseEMPSettingsService classScheduleTestReleaseSettingsService
            )
        {
            _classScheduleService = classScheduleService;
            _classScheduleRosterService = classScheduleRosterService;
            _ilaService = ilaService;
            _cbtScormRegistrationService = cbtScormRegistrationService;
            _classScheduleTestReleaseSettingsService = classScheduleTestReleaseSettingsService;
        }

        public async Task<GradeEvaluationResult> EvaluateClassScheduleEmployeeAsync(ClassSchedule_Employee classSchedule_Employee)
        {
            _classSchedule = await _classScheduleService.GetAsync(classSchedule_Employee.ClassScheduleId);
            _ila = await _ilaService.GetAsync(_classSchedule.ILAID.Value);
            _tests = await _classScheduleRosterService.GetClassScheduleRostersByEmployeeIdAndClassScheduleId(classSchedule_Employee.EmployeeId, classSchedule_Employee.ClassScheduleId);
            _cbt_scormRegistration = await _cbtScormRegistrationService.GetByClassScheduleEmployeeId(classSchedule_Employee.Id);
            _testSettings = await _classScheduleTestReleaseSettingsService.GetTestSettingsByClassId(classSchedule_Employee.ClassScheduleId);

            return EvaluateClassScheduleEmployee(classSchedule_Employee, _classSchedule, _ila, _tests, _cbt_scormRegistration, _testSettings);
        }

        public GradeEvaluationResult EvaluateClassScheduleEmployee(ClassSchedule_Employee classScheduleEmployee, ClassSchedule classSchedule, ILA ila, List<ClassSchedule_Roster> tests, CBT_ScormRegistration cbtScormRegistration, ClassSchedule_TestReleaseEMPSetting testSettings)
        {
            _cbtRequiredForCourse = ila.CBTRequiredForCourse;
            _finalTestRequired = testSettings.FinalTestId.HasValue;
            _retakesAllowed = testSettings.RetakeEnabled ? testSettings.NumberOfRetakes.GetValueOrDefault() : 0;

            //cbt
            if (_cbtRequiredForCourse)
            {
                if (!_finalTestRequired)
                {
                    if (cbtScormRegistration.CompletedDate.HasValue)
                    {
                        return new GradeEvaluationResult(cbtScormRegistration.Score, cbtScormRegistration.Grade, cbtScormRegistration.CompletedDate.Value);
                    }
                }
            }

            //tests
            if (_finalTestRequired)
            {
                var finalTest = tests.Where(r => r.TestTypeId == 2).FirstOrDefault();
                var lastRetake = tests.Where(r => r.TestTypeId == 3).Where(r => r.CompletedDate.HasValue).OrderBy(r => r.CompletedDate.Value).LastOrDefault();
                var retakesTaken = tests.Where(r => r.TestTypeId == 3).Where(r => r.CompletedDate.HasValue).Count();

                if (finalTest == null)
                    return new GradeEvaluationResult();

                if (finalTest.IsGradePassing || _retakesAllowed == 0) //i'm curious if this line will break because at some point we have a grade and not a completed date.  it shouldnt
                    return new GradeEvaluationResult(finalTest.Score, finalTest.Grade, finalTest.CompletedDate);

                if ((lastRetake != null && lastRetake.IsGradePassing) && _retakesAllowed > 0)
                    return new GradeEvaluationResult(finalTest.Score, finalTest.Grade, finalTest.CompletedDate);

                if (!(lastRetake != null && lastRetake.IsGradePassing) && _retakesAllowed <= retakesTaken)
                    return new GradeEvaluationResult(finalTest.Score, finalTest.Grade, finalTest.CompletedDate);
            }

            return new GradeEvaluationResult();
        }
    }
}
