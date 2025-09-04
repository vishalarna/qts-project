using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using System;
using LegacyToQtd2Migrator.Helpers;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class TrainingProgramsFromRsTblPositionTraining : Common.MigrationMap<RsTblPositionTraining, TrainingProgram>
    {
        List<TblPositionTrainingProgram> _positionTrainingPrograms;
        List<RsTblPositionTraining> _rsPositionTraining;
        List<TblPosition> _positions;

        List<TrainingProgram> inserted = new List<TrainingProgram>();

        public TrainingProgramsFromRsTblPositionTraining(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<RsTblPositionTraining> getSourceRecords()
        {
            _positionTrainingPrograms = (_source as EMP_DemoContext).TblPositionTrainingPrograms.ToList();
            _rsPositionTraining = (_source as EMP_DemoContext).RsTblPositionTrainings.Where(r => r.Ttype.ToUpper() == "R").ToList();
            _positions = (_source as EMP_DemoContext).TblPositions.ToList();

            return _rsPositionTraining;
        }

        protected override TrainingProgram mapRecord(RsTblPositionTraining obj)
        {
            var trainingProgram = _positionTrainingPrograms.Where(r => r.Ptpid == obj.Ptpid).FirstOrDefault();

            var sourcePosition = _positions.Where(r => r.Pid == obj.Pid).FirstOrDefault();
            if (sourcePosition == null) return null;

            var targetPosition = (_target as QTD2.Data.QTDContext).Positions.Where(r => r.PositionAbbreviation == sourcePosition.Pabbrev).First();

            return new TrainingProgram()
            {
                Active = true,
                EndDate = new DateTime(Convert.ToInt32(obj.Tyear), 12, 31).ToQtsTime(false),
                StartDate = new DateTime(Convert.ToInt32(obj.Tyear), 1, 1).ToQtsTime(false),
                Year = new DateTime(Convert.ToInt32(obj.Tyear), 1, 1),
                TrainingProgramTypeId = 2,
                PositionId = targetPosition.Id,
                TrainingProgram_ILA_Links = getIlaLinks(obj)
            };
        }

        private ICollection<TrainingPrograms_ILA_Link> getIlaLinks(RsTblPositionTraining obj)
        {
            var sourceLinks = _rsPositionTraining
                                    .Where(r => r.Tyear == obj.Tyear)
                                    .Where(r => r.Pid == obj.Pid)
                                .ToList();
            List<TrainingPrograms_ILA_Link> links = new List<TrainingPrograms_ILA_Link>();

            foreach (var sourceLink in sourceLinks)
            {
                var sourceIla = (_source as EMP_DemoContext).TblCourses.Where(r => r.Corid == sourceLink.Corid).First();
                var targetIla = (_target as QTD2.Data.QTDContext).ILAs.Where(r => r.Number == sourceIla.Cornum).First();

                links.Add(new TrainingPrograms_ILA_Link()
                {
                    ILAId = targetIla.Id,
                    Active = true
                });
            }

            return links;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _positionTrainingPrograms.Count();
        }

        protected override void updateTarget(TrainingProgram record)
        {
            if (record == null) return;

            var existing = inserted
                .Where(r => r.PositionId == record.PositionId)
                .Where(r => r.StartDate == record.StartDate).FirstOrDefault();

            if (existing == null)
            {
                inserted.Add(record);
                (_target as QTD2.Data.QTDContext).TrainingPrograms.Add(record);
            }
        }
    }
}
