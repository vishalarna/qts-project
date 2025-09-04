using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LegacyToQtd2Migrator.Helpers;

namespace LegacyToQtd2Migrator.Mapping.Vision
{
    public class ClientSettingsLicenseMap : Common.MigrationMap<SysTblSetting, ClientSettings_License>
    {
        List<SysTblSetting> _systemSettings;

        public ClientSettingsLicenseMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<SysTblSetting> getSourceRecords()
        {
            _systemSettings = (_source as EMP_DemoContext).SysTblSettings.Where(r => r.ItemName=="Option Code").ToList();
            return _systemSettings;
        }

        protected override ClientSettings_License mapRecord(SysTblSetting obj)
        {
            LicenseConverter converter = new LicenseConverter();
            var clientId = (_source as EMP_DemoContext).SysTblSettings.Where(r => r.ItemName == "ClientID").First();

            return converter.ConvertLegacyLicense(obj.ItemValue, clientId.ItemValue);
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _systemSettings.Count();
        }

        protected override void updateTarget(ClientSettings_License record)
        {
            (_target as QTD2.Data.QTDContext).ClientSettings_Licenses.Add(record);
        }
    }
}
