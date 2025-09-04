using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class StudentEvaluation_QuestionsMap : Common.MigrationMap<TblFormQuestion, StudentEvaluation_Question>
    {
        List<TblFormQuestion> _formQuestion;

        public StudentEvaluation_QuestionsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblFormQuestion> getSourceRecords()
        {
            _formQuestion = (_source as EMP_DemoContext).TblFormQuestions.ToListAsync().Result;
            return _formQuestion;
        }

        protected override StudentEvaluation_Question mapRecord(TblFormQuestion obj)
        {
            return new StudentEvaluation_Question()
            {
                Active = true,
                //StudentEvaluationId,
                //QuestionBankId
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _formQuestion.Count();
        }

        protected override void updateTarget(StudentEvaluation_Question record)
        {
            (_target as QTD2.Data.QTDContext).StudentEvaluation_Questions.Add(record);
        }
    }
}
