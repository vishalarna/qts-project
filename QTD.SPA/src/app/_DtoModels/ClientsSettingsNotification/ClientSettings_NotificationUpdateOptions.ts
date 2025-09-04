export class ClientSettings_NotificationUpdateOptions {
  updateTemplates: Array<CustomSetting_Notification_TemplateEdit>;
  addRecipients: Array<CustomSetting_Notification_Recipients>;
  removeRecipients: Array<CustomSetting_Notification_Recipients>;
  notificationCustomSettings: Array<CustomSetting_Notification_Setting>;
  notificationStepCustomSettings: Array<StepCustomSetting>;
  disable: boolean;
  enable: boolean;

  ClearEnableDisable = () => {
    this.disable = false;
    this.enable = false;
  }

  Enable = () => {
    this.enable = true;
    this.disable = false;
  }

  Disable = () => {
    this.enable = false;
    this.disable = true;
  }

  updateTemplate = (templateDetails: CustomSetting_Notification_TemplateEdit) => {
    if (!this.updateTemplates) this.updateTemplates = [];
    let updateTemplate = this.updateTemplates.filter(r => r.order == templateDetails.order)[0];
    if (updateTemplate) {
      updateTemplate.template = templateDetails.template;
    } else {
      this.updateTemplates.push(templateDetails);
    }
  }

  UpdateStepCustomSetting = (setting: StepCustomSetting) => {
    if (!this.notificationStepCustomSettings) this.notificationStepCustomSettings = [];

    let customSetting = this.notificationStepCustomSettings.filter(dd => dd.stepOrder == setting.stepOrder && dd.key === setting.key)[0];
    if (customSetting) {
      customSetting.value = setting.value;
    } else {
      this.notificationStepCustomSettings.push(setting);
    }
  }

  UpdateCustomSetting = (setting: CustomSetting_Notification_Setting) => {
    if (!this.notificationCustomSettings) this.notificationCustomSettings = [];

    let customSetting = this.notificationCustomSettings.filter(dd => dd.key === setting.key)[0];
    if (customSetting) {
      customSetting.value = setting.value;
    } else {
      this.notificationCustomSettings.push(setting);
    }
  }

  RemoveStepCustomSetting = (setting: StepCustomSetting) => {
    if (!this.notificationStepCustomSettings) return;

    let customSetting = this.notificationStepCustomSettings.filter(dd => dd.stepOrder == setting.stepOrder && dd.key === setting.key)[0];

    if (customSetting) {
      this.notificationStepCustomSettings.splice(this.notificationStepCustomSettings.indexOf(customSetting), 1);
    }
  }

  AddToAddRecipient = (recipient: CustomSetting_Notification_Recipients) => {

    if (!this.addRecipients) this.addRecipients = [];

    if (!this.addRecipients.filter(dd => dd.employeeId == recipient.employeeId && dd.order == recipient.order)[0]) {
      this.addRecipients.push(recipient);
    }
  }

  RemoveFromAddRecipient = (recipient: CustomSetting_Notification_Recipients) => {

    if (!this.addRecipients) return;

    let recipientsvalue = this.addRecipients.filter(dd => dd.employeeId == recipient.employeeId && recipient.order == dd.order)[0];

    if (recipientsvalue) {
      this.addRecipients.splice(this.addRecipients.indexOf(recipientsvalue), 1);
    }
  }

  AddToRemoveRecipient = (recipient: CustomSetting_Notification_Recipients) => {
    if (!this.removeRecipients) this.removeRecipients = [];

    if (!this.removeRecipients.filter(dd => dd.employeeId == recipient.employeeId && dd.order == recipient.order)[0]) {
      this.removeRecipients.push(recipient);
    }
  }

  RemoveFromRemoveRecipient = (recipient: CustomSetting_Notification_Recipients) => {
    if (!this.removeRecipients) return;

    let recipientsvalue = this.removeRecipients.filter(dd => dd.employeeId == recipient.employeeId && recipient.order == dd.order)[0];

    if (recipientsvalue) {
      this.removeRecipients.splice(this.removeRecipients.indexOf(recipientsvalue), 1);
    }
  }
}

export class CustomSetting_Notification_Setting{
  key: string;
  value: string;

  constructor(key, value) {
    this.key = key;
    this.value = value;
  }
}

export class StepCustomSetting {
  stepOrder: number;
  key: string;
  value: string;

  constructor(order, key, value) {
    this.stepOrder = order;
    this.key = key;
    this.value = value;
  }
}

export class CustomSetting_Notification_TemplateEdit {
  order: number
  template: string
}

export class CustomSetting_Notification_Recipients {
  order: number
  employeeId: number

  constructor(employeeId: number, order: number) {
    this.order = order;
    this.employeeId = employeeId;
  }
}
