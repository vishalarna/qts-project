using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LegacyToQtd2Migrator.QTD2ScormContext;

namespace LegacyToQtd2Migrator.Mapping.Scorm
{
    public class ScormRegistrationMap : Common.MigrationMap<ScormRegistration, ScormRegistration>
    {
        List<ScormRegistration> _scormRegistrations;

        public ScormRegistrationMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<ScormRegistration> getSourceRecords()
        {
            _scormRegistrations = (_target as MigrationTestContext).ScormRegistrations.ToList();
            return _scormRegistrations;
        }

        protected override ScormRegistration mapRecord(ScormRegistration obj)
        {
            var registrationIdParts = obj.ApiRegistrationId.Split('|');

            var clid = Convert.ToInt32(registrationIdParts[0]);
            var eid = Convert.ToInt32(registrationIdParts[1]);

            var sourceEmployee = (_source as Legacy.Data.EMP_DemoContext).TblEmployees.Where(r => r.Eid == eid).First();
            var sourceClass = (_source as Legacy.Data.EMP_DemoContext).TblClasses.Where(r => r.Clid == clid).First();

            var targetEmployee = (_target as MigrationTestContext).Employees.Where(r => r.Person.FirstName == sourceEmployee.EfirstName).Where(r => r.Person.LastName == sourceEmployee.ElastName).First();
            var targetCourse = (_target as MigrationTestContext).Ilas.Where(r => r.NickName == sourceClass.Cor.Cordesc).First();
            var targetClass = targetCourse.ClassSchedules.Where(r=> r.StartDateTime == sourceClass.ClstartDate.GetValueOrDefault()).First();
            var targetStudent = targetClass.ClassScheduleEmployees.Where(r => r.EmployeeId == targetEmployee.Id).First();

            obj.ApiRegistrationId = targetStudent.Id.ToString();
            obj.ScormApiRegToLearner.ApiRegistrationId = targetStudent.Id.ToString();

            return obj;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _scormRegistrations.Count();
        }

        protected override void updateTarget(ScormRegistration record)
        {
            (_target as MigrationTestContext).ScormRegistrations.Update(record);
        }
    }
}
