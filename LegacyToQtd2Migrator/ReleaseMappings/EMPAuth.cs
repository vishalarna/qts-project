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
  public  class EMPAuth : IRelease
  {
        public List<IMigrationMap> MigrationMaps { get; set; }

        DbContext _source;
        DbContext _target;

        public EMPAuth(DbContext source, DbContext target)
        {
            _source = source;
            _target = target;
        }

        public void LoadMigrationMaps()
        {
            MigrationMaps = new List<IMigrationMap>();

            //Combine
            MigrationMaps.Add(new Mapping.EMPAuth.ClientsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.EMPAuth.InstancesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.EMPAuth.InstanceSettingsMap(_source, _target));

            //Combine
            MigrationMaps.Add(new Mapping.EMPAuth.AspNetUsersMap(_source, _target));
            MigrationMaps.Add(new Mapping.EMPAuth.AspNetUserClaimsMap(_source, _target));
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
