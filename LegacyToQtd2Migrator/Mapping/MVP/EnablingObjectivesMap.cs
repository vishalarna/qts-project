using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class EnablingObjectivesMap : Common.MigrationMap<TblSkillsKnowledge, EnablingObjective>
    {
        List<TblSkillsKnowledge> _skillsknowledge;
        public EnablingObjectivesMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblSkillsKnowledge> getSourceRecords()
        {
            _skillsknowledge = (_source as EMP_DemoContext).TblSkillsKnowledges.ToListAsync().Result;
            return _skillsknowledge;
        }

        protected override EnablingObjective mapRecord(TblSkillsKnowledge obj)
        {
            return new EnablingObjective()
            {
                //SubCategoryId,
                //TopicId
                //isMetaEO,
                //IsSkillQualification
                //References,
               //Criteria
               //Conditions,
               //EffectiveDate,
               //CreatedBy
               //CreatedDate,
               //ModifiedBy,
               //ModifiedDate
               //EnablingObjective_CategoryId,
               //EnablingObjective_SubCategoryId
              CategoryId=obj.Cid??-1,
              Number=obj.Sknum.ToString(),
              Description=obj.Skdesc,
              Deleted = false,
              Active = obj.Inactive,
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _skillsknowledge.Count();
        }

        protected override void updateTarget(EnablingObjective record)
        {
            (_target as QTD2.Data.QTDContext).EnablingObjectives.Add(record);
        }
    }
}
