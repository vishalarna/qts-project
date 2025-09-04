export class ClientSettings_Notification_CustomSetting{
    clientSettingsNotificationId: number;
    key: string;
    value: string;

    constructor (key, value, clientSettingsNotificationId)
    {
      this.key = key;
      this.value = value;
      this.clientSettingsNotificationId = clientSettingsNotificationId;
    }
}
