using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class AssessmentToolMap : Common.MigrationMap<TblIlaDetail, AssessmentTool>
    {
        List<TblIlaDetail> _iladetail;
        public AssessmentToolMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblIlaDetail> getSourceRecords()
        {
            _iladetail = (_source as EMP_DemoContext).TblIlaDetails.ToListAsync().Result;
            return _iladetail;
        }

        protected override AssessmentTool mapRecord(TblIlaDetail obj)
        {
            var sourceIla = (_source as EMP_DemoContext).TblCourses.Where(r => r.Corid == obj.Corid);

            return new AssessmentTool()
            {        
                //Name= 
                Deleted = false,
                Active = true
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _iladetail.Count();
        }

        protected override void updateTarget(AssessmentTool record)
        {
            (_target as QTD2.Data.QTDContext).AssessmentTools.Add(record);
        }
    }
}
