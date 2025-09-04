//using QTD2.Domain.Entities.Core;
//using LegacyToQtd2Migrator.Legacy.Data;
//using System.Collections.Generic;
//using System.Linq;

//using Microsoft.EntityFrameworkCore;

//namespace LegacyToQtd2Migrator.Mapping.MVP
//{
//    public class StudentEvaluationsWithEmpAggregateMap : Common.MigrationMap<TblStudentEvaluationOverride, StudentEvaluationWithoutEmp>
//    {  
//        List<TblStudentEvaluationOverride> _studentEvaluationOverrides;

//        public StudentEvaluationsWithEmpAggregateMap(DbContext source, DbContext target) : base(source, target)
//        {

//        }

//        protected override List<TblStudentEvaluationOverride> getSourceRecords()
//        { 
//            _studentEvaluationOverrides = (_source as EMP_DemoContext).TblStudentEvaluationOverrides.ToList();

//            return _studentEvaluationOverrides;
//        }

//        protected override StudentEvaluation mapRecord(TblForm obj)
//        {
//            var others = _studentEvaluationQuestions.Where(dd => dd.Fid == obj.Fid).ToList();
//            var ratingScales = (_target as QTD2.Data.QTDContext).RatingScales;
//            var ratingScaleN = (_target as QTD2.Data.QTDContext).RatingScaleNs.Where(r => r.RatingScaleDescription == obj.Rs.Rsinstruction).First();

//            var ratings = obj.Rs.Rsinstruction.Split(';');

//            var rating1 = ratings.Length > 0 ? ratings[0].Split('=').Length > 1 ? ratings[0].Split('=')[1].Trim() : ratings[0] : null;
//            var rating2 = ratings.Length > 1 ? ratings[1].Split('=').Length > 1 ? ratings[1].Split('=')[1].Trim() : ratings[1] : null;
//            var rating3 = ratings.Length > 2 ? ratings[2].Split('=').Length > 1 ? ratings[2].Split('=')[1].Trim() : ratings[2] : null;
//            var rating4 = ratings.Length > 3 ? ratings[3].Split('=').Length > 1 ? ratings[3].Split('=')[1].Trim() : ratings[3] : null;
//            var rating5 = ratings.Length > 4 ? ratings[4].Split('=').Length > 1 ? ratings[4].Split('=')[1].Trim() : ratings[4] : null;

//            return new StudentEvaluation()
//            {
//                Active = true,
//                RatingScaleId = ratingScales
//                                    .Where(r => r.Position1Text == rating1)
//                                    .Where(r => string.IsNullOrEmpty(rating2) || r.Position2Text == rating2)
//                                    .Where(r => string.IsNullOrEmpty(rating3) || r.Position3Text == rating3)
//                                    .Where(r => string.IsNullOrEmpty(rating4) || r.Position4Text == rating4)
//                                    .Where(r => string.IsNullOrEmpty(rating5) || r.Position5Text == rating5)
//                                    .First().Id,
//                RatingScaleN = ratingScaleN,
//                Instructions = obj.Finstructions,
//                Title = obj.Fname,
//                IsPublished = true,
//                //IsAvailableForAllILAs,
//                //IsAllowNAOption,
//                //IsAvailableForSelectedILAs,
//                //IsIncludeCommentSections
//                StudentEvaluationQuestions = getStudentEvaluationQuestions(others),
//                StudentEvaluationHistories = getStudentEvaluationHistories(obj),
//                StudentEvaluationWithoutEmps = getStudentEvaluationWithoutEmps(obj)
//            };
//        }

//        private List<StudentEvaluationWithoutEmp> getStudentEvaluationWithoutEmps(TblForm obj)
//        {
//            List<StudentEvaluationWithoutEmp> evals = new List<StudentEvaluationWithoutEmp>();
//            var overrides = _studentEvaluationOverrides.Where(r => r.Fid == obj.Fid);

//            foreach (var o in overrides)
//            {
//                evals.Add(new StudentEvaluationWithoutEmp()
//                {
//                    Average = (double)o.RatingAverage,
//                    ClassScheduleId = 1,
//                    High = (double)o.RatingHigh,
//                    Low = (double)o.RatingLow,
//                    AdditionalComments = o.Comments,
//                    Notes = o.Comments,
//                    StudentEvaluationQuestion = new StudentEvaluationQuestion(),
//                    DataMode = "Aggregate" //Individual                    
//                });
//            }

//            return new List<StudentEvaluationWithoutEmp>();
//        }

//        private ICollection<StudentEvaluationHistory> getStudentEvaluationHistories(TblForm obj)
//        {
//            List<StudentEvaluationHistory> histories = new List<StudentEvaluationHistory>();

//            return histories;
//        }

//        private ICollection<StudentEvaluation_Question> getStudentEvaluationQuestions(List<TblFormQuestion> forms)
//        {
//            studentEvaluation_Questions = new List<StudentEvaluation_Question>();

//            foreach (var obj in forms)
//            {
//                var sourceQuestion = _studentEvaluationQuestions.Where(r => r.Fqid == obj.Fqid).First();
//                string sourceStem = (sourceQuestion.Fqdesc ?? "").Contains(@"{\rtf") ? RtfPipe.Rtf.ToHtml(sourceQuestion.Fqdesc ?? "") : sourceQuestion.Fqdesc;
//                var targetQuestion = (_target as QTD2.Data.QTDContext).QuestionBanks.Where(r => r.Stem == sourceStem).First();

//                studentEvaluation_Questions.Add(new StudentEvaluation_Question()
//                {
//                    QuestionBankId = targetQuestion.Id,
//                    Active = true,
//                    Deleted = false
//                });
//            }

//            return studentEvaluation_Questions;
//        }

//        protected override void setTotalRecordsToConvert()
//        {
//            TotalRecordsToConvert = _formEvaluation.Count();
//        }

//        protected override void updateTarget(StudentEvaluation record)
//        {
//            var existing = (_target as QTD2.Data.QTDContext).StudentEvaluations.Where(r => r.Title == record.Title).FirstOrDefault();
//            if (existing == null)
//                (_target as QTD2.Data.QTDContext).StudentEvaluations.Add(record);
//        }
//    }
//}
