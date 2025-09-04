using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using LegacyToQtd2Migrator.Helpers;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class IDPsMap : Common.MigrationMap<TblIdp, IDP>
    {
        List<TblIdp> _idp;

        List<ClassSchedule_Employee> _classSchedules_employees;

        public IDPsMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblIdp> getSourceRecords()
        {
            _idp = (_source as EMP_DemoContext).TblIdps.ToListAsync().Result;

            _classSchedules_employees = (_target as QTD2.Data.QTDContext).ClassScheduleEmployees.ToList();

            return _idp;
        }

        protected override IDP mapRecord(TblIdp obj)
        {
            var sourceEmployee = obj.EidNavigation;
            var targetEmployee = (_target as QTD2.Data.QTDContext).Employees
                    .Where(r => r.Person.FirstName == sourceEmployee.EfirstName)
                    .Where(r => r.Person.LastName == sourceEmployee.ElastName).First();

            var sourceIla = obj.Cor;
            var targetIla = (_target as QTD2.Data.QTDContext).ILAs
                    .Where(r => r.Number == sourceIla.Cornum).FirstOrDefault();

            if (targetIla == null) return null;

            return new IDP()
            {
                EmployeeId = targetEmployee.Id,
                ILAId = targetIla.Id,
                IDPYear = new System.DateTime(string.IsNullOrEmpty(obj.Tyear) ? 1900 : System.Convert.ToInt32(obj.Tyear), 1, 1),
                //Score = obj.CompGrade,
                //GradeUpdateReason 
                //taskQualificationCompleted = obj.CompDate.HasValue,
                //Grade = obj.CompGrade,
                //completionDate = obj.CompDate.HasValue ? obj.CompDate.Value.ToQtsTime(false) : obj.CompDate.ToQtsTime(false), 
                Deleted = false,
                Active = true,
                IDPSchedules = getIDP_Schedules(obj, targetIla, targetEmployee)
            };
        }

        private ICollection<IDPSchedule> getIDP_Schedules(TblIdp obj, ILA targetIla, Employee targetEmployee)
        {
            var schedules = new List<IDPSchedule>();

            //find the class this person was enrolled in for the target ila

            int targetYear = string.IsNullOrEmpty(obj.Tyear) ? 1900 : Convert.ToInt32(obj.Tyear);

            var classScheduleEmployees =
                _classSchedules_employees
                 .Where(r => r.EmployeeId == targetEmployee.Id)
                 .Where(r => r.ClassSchedule.ILAID == targetIla.Id)
                 .Where(r => r.ClassSchedule.StartDateTime.Year == targetYear || r.ClassSchedule.EndDateTime.Year == targetYear);

            foreach (var classScheduleEmployee in classScheduleEmployees)
            {
                schedules.Add(new IDPSchedule()
                {
                    ClassSchedule = classScheduleEmployee.ClassSchedule,
                    Active = true,
                    endDate = classScheduleEmployee.ClassSchedule.EndDateTime,
                    plannedDate = obj.ReqCompDate,
                    startDate = classScheduleEmployee.ClassSchedule.StartDateTime,
                    Score = classScheduleEmployee.FinalScore.ToString(),
                    //GradeReason = GradeUpdateReason,
                    TaskQualificationCompleted = classScheduleEmployee.CompletionDate.HasValue,
                    Grade = classScheduleEmployee.FinalGrade,
                    CompletionDate = classScheduleEmployee.CompletionDate, // No TOQTS Time -> already in UTC
                });
            }

            return schedules;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _idp.Count();
        }

        protected override void updateTarget(IDP record)
        {
            if (record == null) return;

            (_target as QTD2.Data.QTDContext).IDPs.Add(record);
        }
    }
}
