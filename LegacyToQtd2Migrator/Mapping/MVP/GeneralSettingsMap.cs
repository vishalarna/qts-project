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
    public class GeneralSettingsMap : Common.MigrationMap<SysTblSetting, ClientSettings_GeneralSettings>
    {
        List<SysTblSetting> _settings;

        List<TblEmpsetting> _empSettings;

        public GeneralSettingsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<SysTblSetting> getSourceRecords()
        {
            _empSettings = (_source as EMP_DemoContext).TblEmpsettings.ToList();
            _settings = (_source as EMP_DemoContext).SysTblSettings.Where(r => r.ItemName == "Company Name").ToList();

            return _settings;
        }

        protected override ClientSettings_GeneralSettings mapRecord(SysTblSetting obj)
        {
            var generalSettings = (_target as QTD2.Data.QTDContext).ClientSettings_GeneralSettings.First();

            generalSettings.CompanyName = obj.ItemValue;
            generalSettings.CompanySpecificCoursePassingScore = Convert.ToDecimal(_empSettings.Where(r => r.EmpsettingDesc == "DefaultTestCutScore").First().EmpsettingValue) / 100.0M;

            return generalSettings;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _settings.Count();
        }

        protected override void updateTarget(ClientSettings_GeneralSettings record)
        {
            (_target as QTD2.Data.QTDContext).ClientSettings_GeneralSettings.Update(record);
        }
    }
}