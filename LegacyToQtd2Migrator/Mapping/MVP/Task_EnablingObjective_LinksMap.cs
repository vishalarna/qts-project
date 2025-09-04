using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class Task_EnablingObjective_LinksMap : Common.MigrationMap<RsTblTasksSkillsKnowledge,Task_EnablingObjective_Link>
    {
        List<RsTblTasksSkillsKnowledge> _taskSkillKnowledge;

        public Task_EnablingObjective_LinksMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<RsTblTasksSkillsKnowledge> getSourceRecords()
        {
            _taskSkillKnowledge = (_source as EMP_DemoContext).RsTblTasksSkillsKnowledges.ToListAsync().Result;
            return _taskSkillKnowledge;
        }

        protected override Task_EnablingObjective_Link mapRecord(RsTblTasksSkillsKnowledge obj)
        {
            return new Task_EnablingObjective_Link()
            {
                Active = true,
                TaskId=obj.Tid,
                //EnablingObjectiveId       
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _taskSkillKnowledge.Count();
        }

        protected override void updateTarget(Task_EnablingObjective_Link record)
        {
            (_target as QTD2.Data.QTDContext).Task_EnablingObjective_Links.Add(record);
        }
    }
}
