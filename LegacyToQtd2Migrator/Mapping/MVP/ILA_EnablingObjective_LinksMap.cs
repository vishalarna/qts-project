using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class ILA_EnablingObjective_LinksMap : Common.MigrationMap<RsTblCoursesSkillsKnowledge, ILA_EnablingObjective_Link>
    {
        List<RsTblCoursesSkillsKnowledge> _courseskillsknowledge;
        public ILA_EnablingObjective_LinksMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<RsTblCoursesSkillsKnowledge> getSourceRecords()
        {
            _courseskillsknowledge = (_source as EMP_DemoContext).RsTblCoursesSkillsKnowledges.ToListAsync().Result;
            return _courseskillsknowledge;
        }

        protected override ILA_EnablingObjective_Link mapRecord(RsTblCoursesSkillsKnowledge obj)
        {
            return new ILA_EnablingObjective_Link()
            {
                //ILAId
                //EnablingObjectiveId
                //CreatedBy
                //CreatedDate
                //ModifiedBy
                //ModifiedDate
                Deleted = false,
                Active = true
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _courseskillsknowledge.Count();
        }

        protected override void updateTarget(ILA_EnablingObjective_Link record)
        {
            (_target as QTD2.Data.QTDContext).ILA_EnablingObjective_Links.Add(record);
        }
    }
}
