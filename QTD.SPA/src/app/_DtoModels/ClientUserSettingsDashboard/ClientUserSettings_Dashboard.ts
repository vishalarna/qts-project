export class ClientUserSettings_Dashboard {
    clientUserId : number;
    dashboardSettingId : number;
    enabled: boolean;

        enable()
        {
            this.enabled = true;
        }

        disable()
        {
            this.enabled = false;
        }
  
}