using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using System;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class StudentEvaluationQuestionsMap : Common.MigrationMap<TblFormQuestion, StudentEvaluationQuestion>
    {
        List<TblFormQuestion> _formQuestion;

        public StudentEvaluationQuestionsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblFormQuestion> getSourceRecords()
        {
            _formQuestion = (_source as EMP_DemoContext).TblFormQuestions.ToListAsync().Result;
            return _formQuestion;
        }

        protected override StudentEvaluationQuestion mapRecord(TblFormQuestion obj)
        {
            return new StudentEvaluationQuestion()
            {
                Active = true,
                EvalFormID=obj.Fid??=-1,
                //QuestionNumber=Convert.ToInt32(obj.Fqnum),
                //QuestionImage,
                //QuestionFiles
                //QuestionText
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _formQuestion.Count();
        }

        protected override void updateTarget(StudentEvaluationQuestion record)
        {
            (_target as QTD2.Data.QTDContext).StudentEvaluationQuestions.Add(record);
        }
    }
}
