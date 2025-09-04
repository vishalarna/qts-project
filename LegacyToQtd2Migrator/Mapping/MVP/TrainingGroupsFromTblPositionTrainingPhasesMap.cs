using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class TrainingGroupsFromTblPositionTrainingPhasesMap : Common.MigrationMap<TblTrainingPhase, TrainingGroup>
    {
        List<TblTrainingPhase> _trainingPhases;
        public TrainingGroupsFromTblPositionTrainingPhasesMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTrainingPhase> getSourceRecords()
        {
            _trainingPhases = (_source as EMP_DemoContext).TblTrainingPhases.ToListAsync().Result;
            return _trainingPhases;
        }

        protected override TrainingGroup mapRecord(TblTrainingPhase obj)
        {
            return new TrainingGroup()
            {
                //CategoryId
                GroupNumber=obj.Tpnum??-1,
                //GroupName
                GroupDescription =obj.Tpdesc,
                //HyperLink
                //PDF
                Deleted = false,
                Active = true,
            };
        }
        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _trainingPhases.Count();
        }
        protected override void updateTarget(TrainingGroup record)
        {
            (_target as QTD2.Data.QTDContext).TrainingGroups.Add(record);
        }
    }
}
