import { ClientSettings_Notification_Step_AvailableCustomSetting } from "./ClientSettings_Notification_Step_AvailableCustomSetting";
import { ClientSettings_Notification_Step_CustomSetting } from "./ClientSettings_Notification_Step_CustomSetting";
import { ClientSettings_Notification_Step_ModelItem } from "./ClientSettings_Notification_Step_ModelItem";
import { ClientSettings_Notification_Step_Recipient } from "./ClientSettings_Notification_Step_Recipient";

export class ClientSettings_Notification_Step{
    clientSettingNotificationId: number;
    template: string;
    order: number;
    model: Array<ClientSettings_Notification_Step_ModelItem>;
    customSettings: Array<ClientSettings_Notification_Step_CustomSetting>;
    recipients: Array<ClientSettings_Notification_Step_Recipient>;
    availableCustomSettings: Array<ClientSettings_Notification_Step_AvailableCustomSetting>;
}