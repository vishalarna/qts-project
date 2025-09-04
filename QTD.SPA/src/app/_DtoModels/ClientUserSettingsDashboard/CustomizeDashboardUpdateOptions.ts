export class CustomizeDashboardUpdateOptions{

    updates: Array<CustomDashboardSettingOption>

    UpdateSettingOption = (setting: CustomDashboardSettingOption) => {
        if (!this.updates) this.updates = [];
        let updateSetting = this.updates.filter(r => r.settings == setting.settings)[0];
        if (updateSetting) {
            updateSetting.enable = setting.enable;
            updateSetting.disable = setting.disable;
          } else {
            this.updates.push(setting);
          }
    }
}

export class CustomDashboardSettingOption {
    settings:string;
    enable: boolean;
    disable: boolean

    constructor(setting: string, enable: boolean, disable: boolean){
        this.settings = setting;
        this.enable = enable;
        this.disable = disable;
    }
}
