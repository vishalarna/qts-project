using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using System;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class ClassSchedulesMap : Common.MigrationMap<TblClass, ClassSchedule>
    {
        List<TblClass> _class;
        List<TblEmployee> _employees;
        List<RsTblClassStudent> _tblClassScheduleEmployees;

        public ClassSchedulesMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblClass> getSourceRecords()
        {
            _class = (_source as EMP_DemoContext).TblClasses.Include("In").ToListAsync().Result;
            _tblClassScheduleEmployees = (_source as EMP_DemoContext).RsTblClassStudents.ToListAsync().Result;
            return _class;
        }

        protected override ClassSchedule mapRecord(TblClass obj)
        {
            var instructor = (_target as QTD2.Data.QTDContext).Instructors.Where(r => r.InstructorName == (obj.In != null ? obj.In.Inname : "")).FirstOrDefault();
            var location = (_target as QTD2.Data.QTDContext).Locations.Where(r => r.LocName == (obj.Lc != null ? obj.Lc.Lcid.ToString() : "")).FirstOrDefault();

            return new ClassSchedule()
            {
                ILAID = 1,
                //RecurrenceId,
                //IsRecurring,
                //ProviderID 
                StartDateTime = obj.Cldate.HasValue ? obj.Cldate.Value : new System.DateTime(1900, 1, 1),
                //EndDateTime=,
                //SpecialInstructions,
                //WebinarLink
                Notes = "CLNote1: " + obj.Clnote1 + System.Environment.NewLine + "CLNote2: " + obj.Clnote2,
                InstructorId = instructor == null ? null : instructor.Id,
                LocationId = location == null ? null : location.Id,
                Deleted = false,
                Active = true,
                //ClassScheduleHistories= getClassScheduleHistories(obj),
                ClassSchedule_Employee = getClassSchedule_Employee(obj)
            };
        }

        private ICollection<ClassScheduleHistory> getClassScheduleHistories(TblClass obj)
        {
            List<ClassScheduleHistory> classScheduleHistories = new List<ClassScheduleHistory>();
            classScheduleHistories.Add(new ClassScheduleHistory()
            {
                Active = true,
                Deleted = false,
                //ChangeNotes
                //ChangeEffectiveDate
                //CreatedBy
                //Createddate
                //ClassScheduleID
            });

            return classScheduleHistories;
        }

        private ICollection<ClassSchedule_Employee> getClassSchedule_Employee(TblClass obj)
        {
            List<ClassSchedule_Employee> classSchedule_Employees = new List<ClassSchedule_Employee>();
            if (obj.RsTblClassStudents != null)
            {
                foreach (var student in obj.RsTblClassStudents)
                {
                    //var classSchedule = (_target as QTD2.Data.QTDContext).ClassSchedules.Where(r => r.Person.FirstName == student.EidNavigation.EfirstName && r.Person.LastName == student.EidNavigation.ElastName).First();
                    var employee = (_target as QTD2.Data.QTDContext).Employees.Where(r => r.Person.FirstName == student.EidNavigation.EfirstName && r.Person.LastName == student.EidNavigation.ElastName).First();

                    classSchedule_Employees.Add(new ClassSchedule_Employee()
                    {
                        Active = true,
                        Deleted = false,
                        //ClassScheduleId = classSchedule.Clid,
                        PreTestStatusId = 1,
                        TestStatusId = String.IsNullOrEmpty(student.CompGrade) ? 1 : 3,
                        RetakeStatusId = 1,
                        CBTStatusId = String.IsNullOrEmpty(student.CompGrade) ?  1 : 3,
                        FinalScore = Convert.ToInt32(student.Score),
                        FinalGrade = student.CompGrade,
                        //GradeNotes,
                        EmployeeId = employee.Id,
                        //IsEnrolled,
                        //IsWaitlisted,
                        //TestId
                    });
                }
            }

            return classSchedule_Employees;
        }
        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _class.Count();
        }

        protected override void updateTarget(ClassSchedule record)
        {
            (_target as QTD2.Data.QTDContext).ClassSchedules.Add(record);
        }

    }
}
