using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using System;

using Microsoft.EntityFrameworkCore;
using LegacyToQtd2Migrator.Helpers;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class StudentEvaluationsMap : Common.MigrationMap<TblForm, StudentEvaluation>
    {
        List<TblClass> _classes;
        List<TblCourse> _courses;
        List<TblEmployee> _employees;
        List<TblForm> _formEvaluation;
        List<TblStudentForm> _studentForms;
        List<TblStudentFormsAnswer> _studentFormAnswers;
        List<TblFormQuestion> _studentEvaluationQuestions;
        List<TblStudentEvaluationOverride> _studentEvaluationOverrides;

        List<StudentEvaluationQuestion> studentEvaluation_Questions;
        List<StudentEvaluation> _studentEvaluation;
        List<ILA> _ilas;
        List<Employee> _targetEmployees;
        List<QuestionBank> _targetQuestions;
        List<RatingScaleN> _ratingScales;

        public StudentEvaluationsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblForm> getSourceRecords()
        {
            _classes = (_source as EMP_DemoContext).TblClasses.ToList();
            _courses = (_source as EMP_DemoContext).TblCourses.ToList();
            _employees = (_source as EMP_DemoContext).TblEmployees.ToList();
            _formEvaluation = (_source as EMP_DemoContext).TblForms.ToListAsync().Result;
            _studentEvaluationQuestions = (_source as EMP_DemoContext).TblFormQuestions.ToList();
            _studentForms = (_source as EMP_DemoContext).TblStudentForms.ToList();
            _studentFormAnswers = (_source as EMP_DemoContext).TblStudentFormsAnswers.ToList();
            _studentEvaluationOverrides = (_source as EMP_DemoContext).TblStudentEvaluationOverrides.ToList();

            _ilas = (_target as QTD2.Data.QTDContext).ILAs.ToList();
            _targetEmployees = (_target as QTD2.Data.QTDContext).Employees.ToList();
            _targetQuestions = (_target as QTD2.Data.QTDContext).QuestionBanks.ToList();
            _ratingScales = (_target as QTD2.Data.QTDContext).RatingScaleNs.ToList();

            return _formEvaluation;
        }

        protected override StudentEvaluation mapRecord(TblForm obj)
        {
            var others = _studentEvaluationQuestions.Where(dd => dd.Fid == obj.Fid).ToList();

            if (obj.Rs == null) return null;

            var ratingScaleN = _ratingScales.Where(r => r.RatingScaleDescription == obj.Rs.Rsinstruction).First();

            var ratings = obj.Rs.Rsinstruction.Split(';');

            var rating1 = ratings.Length > 0 ? ratings[0].Split('=').Length > 1 ? ratings[0].Split('=')[1].Trim() : ratings[0] : null;
            var rating2 = ratings.Length > 1 ? ratings[1].Split('=').Length > 1 ? ratings[1].Split('=')[1].Trim() : ratings[1] : null;
            var rating3 = ratings.Length > 2 ? ratings[2].Split('=').Length > 1 ? ratings[2].Split('=')[1].Trim() : ratings[2] : null;
            var rating4 = ratings.Length > 3 ? ratings[3].Split('=').Length > 1 ? ratings[3].Split('=')[1].Trim() : ratings[3] : null;
            var rating5 = ratings.Length > 4 ? ratings[4].Split('=').Length > 1 ? ratings[4].Split('=')[1].Trim() : ratings[4] : null;

            var studentEval = new StudentEvaluation()
            {
                Active = true,
                RatingScaleId = ratingScaleN.Id,
                RatingScaleN = ratingScaleN,
                Instructions = obj.Finstructions,
                Title = obj.Fname,
                IsPublished = true,
                //IsAvailableForAllILAs,
                //IsAllowNAOption,
                //IsAvailableForSelectedILAs,
                IsIncludeCommentSections = true,
                StudentEvaluationQuestions = getStudentEvaluationQuestions(others),
                //StudentEvaluationHistories = getStudentEvaluationHistories(obj),
                StudentEvaluationWithoutEmps = getStudentEvaluationWithoutEmps(obj),
                ILA_StudentEvaluation_Links = getIlaStudentEvaluationLinks(obj),
                ClassSchedule_Evaluation_Rosters = getClassScheduleEvaluationRosters(obj),
                ClassSchedule_StudentEvaluations_Links = getClassScheduleStudentEvaluationLinks(obj)
            };

            return studentEval;
        }

        private ICollection<ClassSchedule_StudentEvaluations_Link> getClassScheduleStudentEvaluationLinks(TblForm obj)
        {
            var classSchedule_StudentEvaluations_Link = new List<ClassSchedule_StudentEvaluations_Link>();

            var studentForms = _studentForms.Where(r => r.Fid == obj.Fid);

            foreach (var studentForm in studentForms)
            {
                var sourceClass = _classes.Where(r => r.Clid == studentForm.Clid).FirstOrDefault();
                if (sourceClass == null) continue;

                var targetCourse = _ilas.Where(r => r.Number == sourceClass.Cor.Cornum).First();

                int endAMPM = sourceClass.EndAmPm.HasValue ? sourceClass.EndAmPm.Value : -1;
                string endTime = sourceClass.EndTimeStr;
                string[] endTimeParts = (endTime ?? "").Split(":");
                int endTimeHours = (endAMPM == -1 || endTimeParts.Length == 1) ? 0 : endAMPM == 0 ? endTimeParts[0] == "12" ? 0 : Convert.ToInt32(endTimeParts[0]) : endTimeParts[0] == "12" ? 12 : 12 + Convert.ToInt32(endTimeParts[0]);

                int endTimeMinutes = endTimeParts.Length == 2 ? Convert.ToInt32(endTimeParts[1]) : 0;

                DateTime endDate = sourceClass.Cldate.GetValueOrDefault();
                endDate = endDate.Hour == 0 ? endDate.AddHours(endTimeHours).AddMinutes(endTimeMinutes).ToQtsTime(false) : endDate.ToQtsTime(false);

                var targetClass = targetCourse.ClassSchedules.Where(r => r.EndDateTime == endDate).FirstOrDefault();

                if (targetClass == null) continue;

                classSchedule_StudentEvaluations_Link.Add(new ClassSchedule_StudentEvaluations_Link()
                {
                    Active = true,
                    ClassScheduleId = targetClass.Id,
                    Deleted = false
                });
            }

            return classSchedule_StudentEvaluations_Link;
        }

        private ICollection<ClassSchedule_Evaluation_Roster> getClassScheduleEvaluationRosters(TblForm obj)
        {
            var rosters = new List<ClassSchedule_Evaluation_Roster>();

            var studentForms = _studentForms.Where(r => r.Fid == obj.Fid);

            foreach (var studentForm in studentForms)
            {
                var sourceEmployee = _employees.Where(r => r.Eid == studentForm.Eid).FirstOrDefault();
                if (sourceEmployee == null) continue;

                var sourceClass = _classes.Where(r => r.Clid == studentForm.Clid).FirstOrDefault();
                if (sourceClass == null) continue;

                var targetEmployee = _targetEmployees.Where(r => r.EmployeeNumber == sourceEmployee.Enum && r.Person.FirstName == sourceEmployee.EfirstName && r.Person.LastName == sourceEmployee.ElastName).First();
                var targetCourse = _ilas.Where(r => r.Number == sourceClass.Cor.Cornum).First();

                int endAMPM = sourceClass.EndAmPm.HasValue ? sourceClass.EndAmPm.Value : -1;
                string endTime = sourceClass.EndTimeStr;
                string[] endTimeParts = (endTime ?? "").Split(":");
                int endTimeHours = (endAMPM == -1 || endTimeParts.Length == 1) ? 0 : endAMPM == 0 ? endTimeParts[0] == "12" ? 0 : Convert.ToInt32(endTimeParts[0]) : endTimeParts[0] == "12" ? 12 : 12 + Convert.ToInt32(endTimeParts[0]);

                int endTimeMinutes = endTimeParts.Length == 2 ? Convert.ToInt32(endTimeParts[1]) : 0;

                DateTime endDate = sourceClass.Cldate.GetValueOrDefault();
                endDate = endDate.Hour == 0 ? endDate.AddHours(endTimeHours).AddMinutes(endTimeMinutes).ToQtsTime(false) : endDate.ToQtsTime(false);

                var targetClass = targetCourse.ClassSchedules.Where(r => r.EndDateTime == endDate).FirstOrDefault();

                if (targetClass == null) continue;

                rosters.Add(new ClassSchedule_Evaluation_Roster()
                {
                    Active = true,
                    Deleted = false,
                    IsAllowed = true,
                    ClassScheduleId = targetClass.Id,
                    CompletedDate = studentForm.Sfcomplete ? studentForm.EvalDate : null,
                    EmployeeId = targetEmployee.Id,
                    IsCompleted = studentForm.Sfcomplete,
                    IsReleased = studentForm.DateCreated.HasValue,
                    ReleaseDate = studentForm.DateCreated,
                    IsStarted = true,
                });
            }

            return rosters;
        }

        private ICollection<ILA_StudentEvaluation_Link> getIlaStudentEvaluationLinks(TblForm obj)
        {
            List<ILA_StudentEvaluation_Link> links = new List<ILA_StudentEvaluation_Link>();

            foreach (var course in _courses.Where(r => r.Fid == obj.Fid))
            {
                var targetIla = _ilas.Where(r => r.Number == course.Cornum).First();

                links.Add(new ILA_StudentEvaluation_Link()
                {
                    ILAId = targetIla.Id,
                    Active = true,
                    isAllQuestionMandatory = false,
                    studentEvalAudienceID = null,
                    studentEvalAvailabilityID = null
                });
            }

            return links;
        }

        private List<StudentEvaluationWithoutEmp> getStudentEvaluationWithoutEmps(TblForm obj)
        {
            List<StudentEvaluationWithoutEmp> evals = new List<StudentEvaluationWithoutEmp>();
            var overrides = _studentEvaluationOverrides.Where(r => r.Fid == obj.Fid);

            foreach (var o in overrides)
            {
                var sourceQuestion = _studentEvaluationQuestions.Where(r => r.Fqid == o.Fqid).FirstOrDefault();

                if (sourceQuestion == null) continue;

                string sourceStem = (sourceQuestion.Fqdesc ?? "").Contains(@"{\rtf") ? RtfPipe.Rtf.ToHtml(sourceQuestion.Fqdesc ?? "") : sourceQuestion.Fqdesc;
                var targetQuestion = _targetQuestions.Where(r => r.Stem == sourceStem).First();

                var sourceClass = _classes.Where(r => r.Clid == o.Clid).FirstOrDefault();
                if (sourceClass == null) continue;

                var targetCourse = _ilas.Where(r => r.Number == sourceClass.Cor.Cornum).First();

                int endAMPM = sourceClass.EndAmPm.HasValue ? sourceClass.EndAmPm.Value : -1;
                string endTime = sourceClass.EndTimeStr;
                string[] endTimeParts = (endTime ?? "").Split(":");
                int endTimeHours = (endAMPM == -1 || endTimeParts.Length == 1) ? 0 : endAMPM == 0 ? endTimeParts[0] == "12" ? 0 : Convert.ToInt32(endTimeParts[0]) : endTimeParts[0] == "12" ? 12 : 12 + Convert.ToInt32(endTimeParts[0]);

                int endTimeMinutes = endTimeParts.Length == 2 ? Convert.ToInt32(endTimeParts[1]) : 0;

                DateTime endDate = sourceClass.Cldate.GetValueOrDefault();
                endDate = endDate.Hour == 0 ? endDate.AddHours(endTimeHours).AddMinutes(endTimeMinutes).ToQtsTime(false) : endDate.ToQtsTime(false);

                var targetClass = targetCourse.ClassSchedules.Where(r => r.EndDateTime == endDate).FirstOrDefault();

                if (targetClass == null) continue;

                evals.Add(new StudentEvaluationWithoutEmp()
                {
                    Average = (double)(o.RatingAverage.GetValueOrDefault()),
                    ClassScheduleId = targetClass.Id,
                    High = (double)(o.RatingHigh.GetValueOrDefault()),
                    Low = (double)(o.RatingLow.GetValueOrDefault()),
                    AdditionalComments = o.Comments,
                    Notes = o.Comments,
                    QuestionId = targetQuestion.Id,
                    DataMode = "Aggregate" //Individual                    
                });
            }

            var responses = _studentFormAnswers.Where(r => r.Sf != null && r.Sf.Fid == obj.Fid);

            foreach (var response in responses)
            {
                var sourceQuestion = _studentEvaluationQuestions.Where(r => r.Fqid == response.Fqid).First();
                string sourceStem = (sourceQuestion.Fqdesc ?? "").Contains(@"{\rtf") ? RtfPipe.Rtf.ToHtml(sourceQuestion.Fqdesc ?? "") : sourceQuestion.Fqdesc;
                var targetQuestion = _targetQuestions.Where(r => r.Stem == sourceStem).First();
                var sourceEmployee = _employees.Where(r => r.Eid == response.Sf.Eid).FirstOrDefault();

                if (sourceEmployee == null) continue;

                var sourceClass = _classes.Where(r => r.Clid == response.Sf.Clid).FirstOrDefault();

                if (sourceClass == null) continue;

                var targetEmployee = _targetEmployees.Where(r => r.EmployeeNumber == sourceEmployee.Enum && r.Person.FirstName == sourceEmployee.EfirstName && r.Person.LastName == sourceEmployee.ElastName).First();
                var targetCourse = _ilas.Where(r => r.Number == sourceClass.Cor.Cornum).First();

                int endAMPM = sourceClass.EndAmPm.HasValue ? sourceClass.EndAmPm.Value : -1;
                string endTime = sourceClass.EndTimeStr;
                string[] endTimeParts = (endTime ?? "").Split(":");
                int endTimeHours = (endAMPM == -1 || endTimeParts.Length == 1) ? 0 : endAMPM == 0 ? endTimeParts[0] == "12" ? 0 : Convert.ToInt32(endTimeParts[0]) : endTimeParts[0] == "12" ? 12 : 12 + Convert.ToInt32(endTimeParts[0]);

                int endTimeMinutes = endTimeParts.Length == 2 ? Convert.ToInt32(endTimeParts[1]) : 0;

                DateTime endDate = sourceClass.Cldate.GetValueOrDefault();
                endDate = endDate.Hour == 0 ? endDate.AddHours(endTimeHours).AddMinutes(endTimeMinutes).ToQtsTime(false) : endDate.ToQtsTime(false);

                var targetClass = targetCourse.ClassSchedules.Where(r => r.EndDateTime == endDate).FirstOrDefault();

                if (targetClass == null) continue;

                var sourceRatingScale = response.Fq.FidNavigation.Rs;

                var targetRatingScale = _ratingScales.Where(r => r.RatingScaleDescription == sourceRatingScale.Rsinstruction).First();

                evals.Add(new StudentEvaluationWithoutEmp()
                {
                    EmployeeId = targetEmployee.Id,
                    ClassScheduleId = targetClass.Id,
                    RatingScale = targetRatingScale.RatingScaleExpanded.Where(r => r.Ratings == response.Sfascore).FirstOrDefault() == null ? null : targetRatingScale.RatingScaleExpanded.Where(r => r.Ratings == response.Sfascore).First().Id,
                    Active = true,
                    Deleted = false,
                    AdditionalComments = response.Sfacomments,
                    Notes = response.Sfacomments,
                    QuestionId = targetQuestion.Id,
                    DataMode = "Individual"
                });
            }

            var evalsFromClass = evals.Where(r => r.ClassScheduleId == 12662);

            return evals;
        }

        private ICollection<StudentEvaluationHistory> getStudentEvaluationHistories(TblForm obj)
        {
            List<StudentEvaluationHistory> histories = new List<StudentEvaluationHistory>();

            return histories;
        }

        private ICollection<StudentEvaluation_Question> getStudentEvaluationQuestions(List<TblFormQuestion> forms)
        {
            List<StudentEvaluation_Question> studentEvaluation_Questions = new List<StudentEvaluation_Question>();

            foreach (var obj in forms)
            {
                var sourceQuestion = _studentEvaluationQuestions.Where(r => r.Fqid == obj.Fqid).First();
                string sourceStem = (sourceQuestion.Fqdesc ?? "").Contains(@"{\rtf") ? RtfPipe.Rtf.ToHtml(sourceQuestion.Fqdesc ?? "") : sourceQuestion.Fqdesc;
                var targetQuestion = _targetQuestions.Where(r => r.Stem == sourceStem).First();

                studentEvaluation_Questions.Add(new StudentEvaluation_Question()
                {
                    QuestionBankId = targetQuestion.Id,
                    Active = true,
                    Deleted = false
                });
            }

            return studentEvaluation_Questions;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _formEvaluation.Count();
        }

        protected override void updateTarget(StudentEvaluation record)
        {
            if (record == null) return;

            var existing = (_target as QTD2.Data.QTDContext).StudentEvaluations.Where(r => r.Title == record.Title).FirstOrDefault();
            if (existing == null)
                (_target as QTD2.Data.QTDContext).StudentEvaluations.Add(record);
        }
    }
}
