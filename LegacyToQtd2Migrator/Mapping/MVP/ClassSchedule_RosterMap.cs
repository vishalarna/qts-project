using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class ClassSchedule_RosterMap : Common.MigrationMap<RsTblClassStudent, ClassSchedule_Roster>
    {
        List<RsTblClassStudent> _tblClassStudent;

        public ClassSchedule_RosterMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<RsTblClassStudent> getSourceRecords()
        {
            _tblClassStudent = (_source as EMP_DemoContext).RsTblClassStudents.ToListAsync().Result;
            return _tblClassStudent;
        }

        protected override ClassSchedule_Roster mapRecord(RsTblClassStudent obj)
        {
            return new ClassSchedule_Roster()
            {
                //ClassScheduleId
                //TestId
                //TestTypeId
                //EmpId
                //Disclaimer
                //Grade
                //Interrupted
                //Restarted
                //CompletedDate
                //ReleaseDate
                //Score
                //EmployeeId
                //Deleted
                Active = true,
                //CreatedBy
                //CreatedDate
                //ModifiedBy
                //ModifiedDate
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _tblClassStudent.Count();
        }

        protected override void updateTarget(ClassSchedule_Roster record)
        {
           // (_target as QTD2.Data.QTDContext).ClassSchedule_Roster.Add(record);
        }
    }
}
