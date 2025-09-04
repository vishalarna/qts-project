using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyToQtd2Migrator.Releases
{
    public interface IRelease
    {
        List<Mapping.Common.IMigrationMap> MigrationMaps { get; set; }
        void RunRelease();

        void LoadMigrationMaps();
    }
}
