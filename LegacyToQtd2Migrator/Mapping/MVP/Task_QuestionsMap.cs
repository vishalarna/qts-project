using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class Task_QuestionsMap : Common.MigrationMap<TblTasksQuestion, Task_Question>
    {
        List<TblTasksQuestion> _tblTaskQuestion;

        public Task_QuestionsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTasksQuestion> getSourceRecords()
        {
            _tblTaskQuestion = (_source as EMP_DemoContext).TblTasksQuestions.ToListAsync().Result;
            return _tblTaskQuestion;
        }

        protected override Task_Question mapRecord(TblTasksQuestion obj)
        {
            return new Task_Question()
            {
                Active = true,
                TaskId = obj.Tid,
                QuestionNumber=obj.Tqnumber??=-1,
                Question=obj.Tqquestion,
                Answer=obj.Tqanswer
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _tblTaskQuestion.Count();
        }

        protected override void updateTarget(Task_Question record)
        {
            (_target as QTD2.Data.QTDContext).Task_Questions.Add(record);
        }
    }
}
