using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
  public class InstructorsMap : Common.MigrationMap<LktblInstructor, Instructor>
    {
        List<TblInstructorsAdministrator> _instructorAdmins;
        List<LktblInstructor> _instructors;

        public InstructorsMap(DbContext source, DbContext target) : base(source, target)
        {
        }

        protected override List<LktblInstructor> getSourceRecords()
        {
            _instructorAdmins = (_source as EMP_DemoContext).TblInstructorsAdministrators.Include("Instuctor").ToList();
            _instructors = (_source as EMP_DemoContext).LktblInstructors.ToList();

            return _instructors;
        }

        protected override Instructor mapRecord(LktblInstructor obj)
        {
            var adminRecord = _instructorAdmins.Where(r => r.InstuctorId == obj.Inid).FirstOrDefault();
            bool isAdmin = adminRecord != null && adminRecord.IsAdministrator.GetValueOrDefault();

            return new Instructor()
            {
                IsWorkBookAdmin = isAdmin,
                InstructorDescription = obj.InNote1,
                InstructorEmail = obj.Inemail,
                InstructorName = obj.Inname,
                InstructorNumber = obj.Inid.ToString(),
                ICategoryId = 1,
                Active = !obj.Inactive,
                Deleted = false
                //EffectiveDate
            };

            //missing ref to person table
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _instructorAdmins.Count();
        }

        protected override void updateTarget(Instructor record)
        {
            (_target as QTD2.Data.QTDContext).Instructors.Add(record);
        }


    }
}
