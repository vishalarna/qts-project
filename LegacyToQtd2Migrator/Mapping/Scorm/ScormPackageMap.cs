using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LegacyToQtd2Migrator.QTD2ScormContext;

namespace LegacyToQtd2Migrator.Mapping.Scorm
{
    public class ScormPackageMap : Common.MigrationMap<CbtScormUpload, ScormPackage>
    {
        List<CbtScormUpload> _scormUploads;

        public ScormPackageMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<CbtScormUpload> getSourceRecords()
        {
            _scormUploads = (_target as MigrationTestContext).CbtScormUploads.ToList();
            return _scormUploads;
        }

        protected override ScormPackage mapRecord(CbtScormUpload obj)
        {
            var scormPackage = (_target as MigrationTestContext).ScormPackages.Where(r => r.DisplayTitle == obj.Name).Where(r=> r.UpdateDt == obj.ConnectedDate).First();

            scormPackage.ApiCourseId = obj.Id.ToString();

            return scormPackage;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _scormUploads.Count();
        }

        protected override void updateTarget(ScormPackage record)
        {
            (_target as MigrationTestContext).ScormPackages.Update(record);
        }
    }
}
