using System.Collections.Generic;

namespace QTD2.Domain.Entities.Core
{
    public class ClientUserSettings_DashboardSetting : Common.Entity
    {
        public int ClientUserId { get; set; }
        public int DashboardSettingId { get; set; }
        public bool Enabled { get; set; }

        public virtual DashboardSetting DashboardSetting { get; set; }
        public virtual ClientUser ClientUser { get; set; }

        public ClientUserSettings_DashboardSetting() { }

        public ClientUserSettings_DashboardSetting(int clientUserId, int dashboardSettingId, bool enabled)
        {
            ClientUserId = clientUserId;
            DashboardSettingId = dashboardSettingId;
            Enabled = enabled;
        }

        public void Enable(string modifiedBy)
        {
            Enabled = true;
            ModifiedBy = modifiedBy;
            ModifiedDate = System.DateTime.Now;
        }

        public void Disable(string modifiedBy)
        {
            Enabled = false;
            ModifiedBy = modifiedBy;
            ModifiedDate = System.DateTime.Now;
        }
    }
}
