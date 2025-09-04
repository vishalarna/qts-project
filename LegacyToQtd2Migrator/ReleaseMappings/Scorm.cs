using LegacyToQtd2Migrator.Mapping.Common;
using LegacyToQtd2Migrator.Releases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyToQtd2Migrator.Releases
{
    public class Scorm : IRelease
    {
        public List<IMigrationMap> MigrationMaps { get; set; }

        DbContext _source;
        DbContext _target;

        public Scorm(DbContext source, DbContext target)
        {
            _source = source;
            _target = target;
        }

        public void LoadMigrationMaps()
        {
            MigrationMaps = new List<IMigrationMap>();

            MigrationMaps.Add(new Mapping.Scorm.CBTMap(_source, _target));
            MigrationMaps.Add(new Mapping.Scorm.ScormPackageMap(_source, _target));

            //combine
            MigrationMaps.Add(new Mapping.Scorm.ScormApiRegToLearnerMap(_source, _target));
            //MigrationMaps.Add(new Mapping.Scorm.ScormLearnerMap(_source, _target));
            //MigrationMaps.Add(new Mapping.Scorm.ScormRegistrationMap(_source, _target));

            MigrationMaps.Add(new Mapping.Scorm.ILAPopulateCbtMap(_source, _target));
            //corret class schedule map

        }

        public void RunRelease()
        {
            LoadMigrationMaps();

            foreach (var map in MigrationMaps)
            {
                map.ConvertRecords();
            }
        }
    }
}
