using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class Task_TrainingGroupsFromTblTrainingPhaseMap : Common.MigrationMap<TblTrainingPhase, Task_TrainingGroup>
    {
        List<TblTrainingPhase> _trainingPhases;
        public Task_TrainingGroupsFromTblTrainingPhaseMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTrainingPhase> getSourceRecords()
        {
            _trainingPhases = (_source as EMP_DemoContext).TblTrainingPhases.ToListAsync().Result;
            return _trainingPhases;
        }

        protected override Task_TrainingGroup mapRecord(TblTrainingPhase obj)
        {
            return new Task_TrainingGroup()
            {
                //TaskId
                //TrainingGroupId
                //CreatedBy
                //CreatedDate
                //ModifiedBy
                //ModifiedDate
                Deleted = false,
                Active = true,
            };
        }
        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _trainingPhases.Count();
        }
        protected override void updateTarget(Task_TrainingGroup record)
        {
            (_target as QTD2.Data.QTDContext).Task_TrainingGroups.Add(record);
        }
    }
}
