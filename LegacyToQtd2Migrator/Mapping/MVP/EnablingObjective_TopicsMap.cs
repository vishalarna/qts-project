using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
 public  class EnablingObjective_TopicsMap : Common.MigrationMap<TblSkillsKnowledge, EnablingObjective_Topic>
    {
        List<TblSkillsKnowledge> _skillsknowledge;
        public EnablingObjective_TopicsMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblSkillsKnowledge> getSourceRecords()
        {
            _skillsknowledge = (_source as EMP_DemoContext).TblSkillsKnowledges.ToListAsync().Result;
            return _skillsknowledge;
        }

        protected override EnablingObjective_Topic mapRecord(TblSkillsKnowledge obj)
        {
            return new EnablingObjective_Topic()
            {
                //Title
                //SubCategoryId
                //CreatedBy,
                //CreatedDate
                //ModifiedBy
                //ModifiedDate
                Description = obj.Skdesc,
                Number=obj.Sknum,
                Deleted = false,
                Active = true,
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _skillsknowledge.Count();
        }

        protected override void updateTarget(EnablingObjective_Topic record)
        {
            (_target as QTD2.Data.QTDContext).EnablingObjective_Topics.Add(record);
        }
    }
}
