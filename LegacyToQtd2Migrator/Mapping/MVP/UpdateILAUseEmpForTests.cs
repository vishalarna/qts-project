using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class UpdateILAUseEmpForTests : Common.MigrationMap<ILA, ILA>
    {
        List<ILA> _ilas;

        public UpdateILAUseEmpForTests(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<ILA> getSourceRecords()
        {
            _ilas = (_target as QTD2.Data.QTDContext).ILAs.Where(r => r.ILATraineeEvaluations.Any()).ToList();
            return _ilas;
        }

        protected override ILA mapRecord(ILA obj)
        {
            obj.UseForEMP = true;

            return obj;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _ilas.Count();
        }

        protected override void updateTarget(ILA record)
        {
            (_target as QTD2.Data.QTDContext).ILAs.Update(record);
        }
    }
}
