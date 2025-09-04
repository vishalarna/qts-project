using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class EnablingObjective_QuestionsMap : Common.MigrationMap<TblTestItem, EnablingObjective_Question>
    {
        List<TblTestItem> _testitem;
        public EnablingObjective_QuestionsMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblTestItem> getSourceRecords()
        {
            _testitem = (_source as EMP_DemoContext).TblTestItems.ToListAsync().Result;
            return _testitem;
        }

        protected override EnablingObjective_Question mapRecord(TblTestItem obj)
        {
            return new EnablingObjective_Question()
            {
                //EnablingObjectiveId
                //Question
                //Answer
                //QuestionNumber,
                //CreatedDate,
                //CreatedBy
               //ModifiedBy
                //ModifiedDate
                Deleted = obj.Deleted ??false,
                Active = obj.Active?? true,
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _testitem.Count();
        }

        protected override void updateTarget(EnablingObjective_Question record)
        {
            (_target as QTD2.Data.QTDContext).EnablingObjective_Questions.Add(record);
        }
    }
}
