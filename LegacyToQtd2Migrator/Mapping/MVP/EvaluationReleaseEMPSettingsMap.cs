using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
  public  class EvaluationReleaseEMPSettingsMap : Common.MigrationMap<TblEmpsetting, EvaluationReleaseEMPSettings>
    {
        List<TblEmpsetting> _empsettings;
        public EvaluationReleaseEMPSettingsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblEmpsetting> getSourceRecords()
        {
            _empsettings = (_source as EMP_DemoContext).TblEmpsettings.ToListAsync().Result;
            return _empsettings;
        }

        protected override EvaluationReleaseEMPSettings mapRecord(TblEmpsetting obj)
        {
            return new EvaluationReleaseEMPSettings()
            {
                //ILAId
                //EvaluationRequired
                //EvaluationUsedToDeployStudentEvaluation
                //EvaluationAvailableOnStartDate
                //EvaluationAvailableOnEndDate
                //FinalGradeRequired
                //ReleaseOnSpecificTimeAfterClassEndDate
                //ReleaseAfterEndTime
                //ReleasePrior
                //ReleaseAfterGradeAssigned
                //EvaluationDueDate
                //CreatedBy
                //CreatedDate
                //ModifiedBy
                //ModifiedDate
                //Deleted=false,
                //Active=true
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _empsettings.Count();
        }

        protected override void updateTarget(EvaluationReleaseEMPSettings record)
        {
            (_target as QTD2.Data.QTDContext).EvaluationReleaseEMPSettings.Add(record);
        }

    }
}
