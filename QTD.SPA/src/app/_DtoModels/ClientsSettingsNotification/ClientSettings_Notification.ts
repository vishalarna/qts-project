import { ClientSettings_Notification_AvailableCustomSetting } from "./ClientSettings_Notification_AvailableCustomSetting";
import { ClientSettings_Notification_CustomSetting } from "./ClientSettings_Notification_CustomSetting";
import { ClientSettings_Notification_Step } from "./ClientSettings_Notification_Step";

export class ClientSettings_Notification{
    name: string;
    clientId: string;
    enabled: boolean;
    timingText: string;
    steps: Array<ClientSettings_Notification_Step>;
    availableCustomSettings: Array<ClientSettings_Notification_AvailableCustomSetting>;
    customSettings: Array<ClientSettings_Notification_CustomSetting>;
}
