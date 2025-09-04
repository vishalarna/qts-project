using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using System;
using LegacyToQtd2Migrator.Helpers;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class TrainingProgramsMap : Common.MigrationMap<TblPositionTrainingProgram, TrainingProgram>
    {
        List<TblPositionTrainingProgram> _positionTrainingPrograms;
        List<RsTblPositionTraining> _rsPositionTraining;
        List<TblPosition> _positions;

        public TrainingProgramsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblPositionTrainingProgram> getSourceRecords()
        {
            _positionTrainingPrograms = (_source as EMP_DemoContext).TblPositionTrainingPrograms.ToList();
            _rsPositionTraining = (_source as EMP_DemoContext).RsTblPositionTrainings.ToList();
            _positions = (_source as EMP_DemoContext).TblPositions.ToList();

            return _positionTrainingPrograms;
        }

        protected override TrainingProgram mapRecord(TblPositionTrainingProgram obj)
        {
            var trainingProgram = _positionTrainingPrograms.Where(r => r.Ptpid == obj.Ptpid).First();

            var sourcePosition = _positions.Where(r => r.Pid == obj.Pid).FirstOrDefault();
            if (sourcePosition == null) return null;

            var targetPosition = (_target as QTD2.Data.QTDContext).Positions.Where(r => r.PositionAbbreviation == sourcePosition.Pabbrev).First();

            return new TrainingProgram()
            {
                Active = true,
                EndDate = obj.TpendDate.HasValue ? obj.TpendDate.Value.ToQtsTime(false) : obj.TpendDate.ToQtsTime(false),
                StartDate = obj.Tpdate.GetValueOrDefault().ToQtsTime(false),
                TPVersionNo = obj.Revision.ToString(),
                ProgramTitle = obj.ProgramTitle,
                TrainingProgramTypeId = obj.ProgramType == null ? 1 : obj.ProgramType.ToUpper() == "I" ? 1 : obj.ProgramType.ToUpper() == "R" ? 2 : 3,
                PositionId = targetPosition.Id,
                TrainingProgram_ILA_Links = getIlaLinks(obj),
            };
        }

        private ICollection<TrainingPrograms_ILA_Link> getIlaLinks(TblPositionTrainingProgram obj)
        {
            List<TrainingPrograms_ILA_Link> links = new List<TrainingPrograms_ILA_Link>();

            List<RsTblPositionTraining> positions = new List<RsTblPositionTraining>();

            string programType = (obj.ProgramType ?? "I").ToUpper();

            if (programType.ToUpper() == "R")
            {
                positions = _rsPositionTraining
                                    .Where(r => r.Tyear == obj.Tpdate.GetValueOrDefault().Year.ToString())
                                    .Where(r => r.Pid == obj.Pid)
                                .ToList();
            }
            else
            {
                positions = _rsPositionTraining.Where(r => r.Ptpid == obj.Ptpid).ToList();
            }

            foreach (var position in positions)
            {
                var sourceIla = (_source as EMP_DemoContext).TblCourses.Where(r => r.Corid == position.Corid).First();
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
            if (record != null)
                (_target as QTD2.Data.QTDContext).TrainingPrograms.Add(record);
        }
    }
}
