using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class TrainingGroupsFromRstblPositions_TasksMap: Common.MigrationMap<RstblPositionsTask, TrainingGroup>
    {
        List<RstblPositionsTask> _positionTasks;
        public TrainingGroupsFromRstblPositions_TasksMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<RstblPositionsTask> getSourceRecords()
        {
            _positionTasks = (_source as EMP_DemoContext).RstblPositionsTasks.ToListAsync().Result;
            return _positionTasks;
        }

        protected override TrainingGroup mapRecord(RstblPositionsTask obj)
        {
            return new TrainingGroup()
            {
                //CategoryId
                //GroupNumber,
                //GroupName
                //GroupDescription,
                //HyperLink,
                //PDF,
                Deleted = false,
                Active = true,
            };
        }
        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _positionTasks.Count();
        }
        protected override void updateTarget(TrainingGroup record)
        {
            (_target as QTD2.Data.QTDContext).TrainingGroups.Add(record);
        }
    }
}
