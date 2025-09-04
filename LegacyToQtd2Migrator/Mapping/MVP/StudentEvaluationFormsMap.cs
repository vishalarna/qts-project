using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class StudentEvaluationFormsMap : Common.MigrationMap<TblForm, StudentEvaluationForm>
    {
        List<TblForm> _studentForms;

        public StudentEvaluationFormsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblForm> getSourceRecords()
        {
            _studentForms = (_source as EMP_DemoContext).TblForms.ToListAsync().Result;
            return _studentForms;
        }

        protected override StudentEvaluationForm mapRecord(TblForm obj)
        {
            return new StudentEvaluationForm()
            {
                Active = true,
                Name = obj.Fname,
                RatingScaleId = obj.Rsid ?? -1,
                Deleted = false,
                IncludeComments = true
                //IsShared,
                //IsNAOption,
                //IncludeComments
                //IsAvailableForAllILAs  
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _studentForms.Count();
        }

        protected override void updateTarget(StudentEvaluationForm record)
        {
            (_target as QTD2.Data.QTDContext).StudentEvaluationForms.Add(record);
        }
    }
}
