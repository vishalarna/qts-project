using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using LegacyToQtd2Migrator.QTD2ScormContext;

namespace LegacyToQtd2Migrator.Mapping.Scorm
{
    public class CBTMap : Common.MigrationMap<ScormPackage, CbtScormUpload>
    {
        List<ScormPackage> _packages;
        List<Cbt> cbts = new List<Cbt>();

        public CBTMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<ScormPackage> getSourceRecords()
        {
            _packages = (_target as MigrationTestContext).ScormPackages.Include("ScormRegistrations").OrderBy(r => r.UpdateDt).ToList();
            return _packages;
        }

        protected override CbtScormUpload mapRecord(ScormPackage obj)
        {
            var apiCourseIdParts = obj.ApiCourseId.Split(' ');
            var corId = apiCourseIdParts[0];
            int corIdInt;

            bool validCorId = int.TryParse(corId, out corIdInt);

            if (validCorId)
            {
                //get ILA from CorId
                var sourceIla = (_source as Legacy.Data.EMP_DemoContext).TblCourses.Where(r => r.Corid == corIdInt).FirstOrDefault();

                if (sourceIla == null)
                    return null;

                var targetIla = (_target as MigrationTestContext).Ilas.Where(r => r.Number == sourceIla.Cornum).FirstOrDefault();

                if (targetIla == null)
                    return null;

                //get existing CBT or create
                var cbt = cbts.Where(r => r.Ilaid == targetIla.Id).FirstOrDefault();

                if (cbt != null && (cbt.Active != cbt.Active || apiCourseIdParts.Length == 1))
                    cbt.Active = cbt.Active || apiCourseIdParts.Length == 1;

                return new CbtScormUpload()
                {
                    Active = apiCourseIdParts.Length == 1,
                    Cbt = cbt == null ? createCbt(targetIla, obj) : cbt,
                    ConnectedDate = obj.UpdateDt,
                    DisconnectedDate = apiCourseIdParts.Length == 1 ? null : parseDate(apiCourseIdParts, obj),
                    ScormStatus = apiCourseIdParts.Length == 1 ? "Uploaded" : "Disconnected",
                    CbtScormRegistrations = getCbtScormRegistrations(obj),
                    Name = obj.DisplayTitle
                };
            }
            else
                return null;
        }

        private ICollection<CbtScormRegistration> getCbtScormRegistrations(ScormPackage obj)
        {
            List<CbtScormRegistration> registrations = new List<CbtScormRegistration>();

            foreach (var reg in obj.ScormRegistrations)
            {
                var registrationIdParts = reg.ApiRegistrationId.Split('|');

                if (registrationIdParts.Length == 1) return null;

                var clid = Convert.ToInt32(registrationIdParts[0]);
                var eid = Convert.ToInt32(registrationIdParts[1]);

                var sourceEmployee = (_source as Legacy.Data.EMP_DemoContext).TblEmployees.Where(r => r.Eid == eid).FirstOrDefault();
                var sourceClass = (_source as Legacy.Data.EMP_DemoContext).TblClasses.Include("Cor").Where(r => r.Clid == clid).FirstOrDefault();

                if (sourceClass == null || sourceEmployee == null) return null;

                var targetEmployee = (_target as MigrationTestContext).Employees.Include("Person").Where(r => r.Person.FirstName == sourceEmployee.EfirstName).Where(r => r.Person.LastName == sourceEmployee.ElastName).Where(r => r.EmployeeNumber == sourceEmployee.Enum).First();
                var targetCourse = (_target as MigrationTestContext).Ilas.Include("ClassSchedules").Include("ClassSchedules.ClassScheduleEmployees").Where(r => r.NickName == sourceClass.Cor.Cornum).First();
                var targetClass = targetCourse.ClassSchedules.Where(r => r.SpecialInstructions == sourceClass.Clid.ToString()).First();
                var targetStudent = targetClass.ClassScheduleEmployees.Where(r => r.EmployeeId == targetEmployee.Id).FirstOrDefault();

                if (targetStudent == null) return null;

                registrations.Add(new CbtScormRegistration()
                {
                    Active = true,
                    ClassScheduleEmployeeId = targetStudent.Id,
                    RegistrationCompletion = reg.CompletedDtUtc == null ? reg.FirstaccessDtUtc == null ? (int)QTD2.Domain.Entities.Core.CBT_ScormRegistrationCompletion.PENDING : (int)QTD2.Domain.Entities.Core.CBT_ScormRegistrationCompletion.INCOMPLETE : (int)QTD2.Domain.Entities.Core.CBT_ScormRegistrationCompletion.COMPLETED,
                    PassingScore = 0,
                    PassingScoreSource = 1
                });
            }

            return registrations;
        }

        private Cbt createCbt(Ila targetIla, ScormPackage obj)
        {
            var apiCourseIdParts = obj.ApiCourseId.Split(' ');

            var cbt = new Cbt()
            {
                Active = apiCourseIdParts.Length == 1,
                Ilaid = targetIla.Id,
                Availablity = 1,
                DueDateAmount = 10,
                EmpSettingsReleaseTypeId = 1,
                //DueDateInterval = 1,
                CbtlearningContractInstructions = ""
            };

            cbts.Add(cbt);
            return cbt;
        }

        private DateTime parseDate(string[] apiCourseIdParts, ScormPackage obj)
        {
            string date = String.Join(' ', apiCourseIdParts.Skip(1).Take(3));
            DateTime dateTime;

            bool isParsed = DateTime.TryParse(date, out dateTime);

            return isParsed ? dateTime : obj.UpdateDt;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _packages.Count();
        }

        protected override void updateTarget(CbtScormUpload record)
        {
            if (record == null) return;

            (_target as MigrationTestContext).CbtScormUploads.Update(record);
        }
    }
}
