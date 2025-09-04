using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using LegacyToQtd2Migrator.QTD2ScormContext;

namespace LegacyToQtd2Migrator.Mapping.Scorm
{
    public class ScormApiRegToLearnerMap : Common.MigrationMap<ScormApiRegToLearner, ScormApiRegToLearner>
    {
        List<ScormApiRegToLearner> _scormApiRegToLearner;

        public ScormApiRegToLearnerMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<ScormApiRegToLearner> getSourceRecords()
        {
            //breaking but owell, didn't plan for this
            _scormApiRegToLearner = (_target as MigrationTestContext).ScormApiRegToLearners.ToList();

            return _scormApiRegToLearner;
        }

        protected override ScormApiRegToLearner mapRecord(ScormApiRegToLearner obj)
        {
            var registrationIdParts = obj.ApiRegistrationId.Split('|');

            if (registrationIdParts.Length == 1) return null;

            var clid = Convert.ToInt32(registrationIdParts[0]);
            var eid = Convert.ToInt32(registrationIdParts[1]);
            
            var sourceEmployee = (_source as Legacy.Data.EMP_DemoContext).TblEmployees.Where(r => r.Eid == eid).FirstOrDefault();
            var sourceClass = (_source as Legacy.Data.EMP_DemoContext).TblClasses.Include("Cor").Where(r => r.Clid == clid).FirstOrDefault();

            if (sourceClass == null || sourceEmployee == null) return null;

            var targetEmployee = (_target as MigrationTestContext).Employees.Include("Person").Where(r => r.Person.FirstName == sourceEmployee.EfirstName).Where(r => r.Person.LastName == sourceEmployee.ElastName).Where(r => r.EmployeeNumber ==  sourceEmployee.Enum).First();
            var targetCourse = (_target as MigrationTestContext).Ilas.Include("ClassSchedules").Include("ClassSchedules.ClassScheduleEmployees").Where(r => r.NickName == sourceClass.Cor.Cornum).FirstOrDefault();

            if (targetCourse == null) return null; 

            var targetClass = targetCourse.ClassSchedules.Where(r => r.SpecialInstructions == sourceClass.Clid.ToString()).First();
            var targetStudent = targetClass.ClassScheduleEmployees.Where(r => r.EmployeeId == targetEmployee.Id).FirstOrDefault();

            if (targetStudent == null) return null;

            var targetCbtScormRegistration = (_target as MigrationTestContext).CbtScormRegistrations.Where(r => r.ClassScheduleEmployeeId == targetStudent.Id).FirstOrDefault();

            if (targetCbtScormRegistration == null) return null;

            string newFirstName = targetEmployee.Person.FirstName;
            string newLastName = targetEmployee.Person.LastName;
            string newApiLearnerId = targetEmployee.Id.ToString();
            string newRegistrationId = targetCbtScormRegistration.Id.ToString() + "." + targetStudent.Id.ToString();

            string oldApiLearnerId = obj.ApiLearnerId;
            string oldRegistrationId = obj.ApiRegistrationId;

            //the things we do for love
            using (var command = _target.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"
                                    ALTER TABLE [dbo].[ScormApiRegToLearner]
                                    NOCHECK CONSTRAINT  FK_ScormRegToLearn_learner

                                    ALTER TABLE [dbo].[ScormRegistration]
                                    NOCHECK CONSTRAINT  FK_ScormRegistration_reglearn

                                    BEGIN TRAN
                                    Update [dbo].[ScormApiLearner]
                                    set Api_learner_id = @newLearnerId,
                                        first_name = @newFirstName,
                                        last_name = @newLastName
                                        where api_learner_id = @oldLearnerId

                                    update [dbo].[ScormApiRegToLearner] set api_learner_id = @newLearnerId where api_learner_id = @oldLearnerId

                                    update [dbo].[ScormApiRegToLearner] set api_registration_id = @newRegistrationId where api_registration_id = @oldRegistrationId

                                    update [dbo].[ScormRegistration] set api_registration_id = @newRegistrationId where api_registration_id = @oldRegistrationId

                                    COMMIT TRAN
  
                                    ALTER TABLE [ScormApiRegToLearner] 
                                    CHECK CONSTRAINT FK_ScormRegToLearn_learner

                                    ALTER TABLE [dbo].[ScormRegistration]
                                    CHECK CONSTRAINT  FK_ScormRegistration_reglearn";

                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@newLearnerId", newApiLearnerId));
                command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@oldLearnerId", oldApiLearnerId));
                command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@newFirstName", newFirstName));
                command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@newLastName", newLastName));
                command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@newRegistrationId", newRegistrationId));
                command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@oldRegistrationId", oldRegistrationId));


                _target.Database.OpenConnection();

                var result = command.ExecuteNonQuery();
            }

            return obj;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _scormApiRegToLearner.Count();
        }

        protected override void updateTarget(ScormApiRegToLearner record)
        {

        }
    }
}
