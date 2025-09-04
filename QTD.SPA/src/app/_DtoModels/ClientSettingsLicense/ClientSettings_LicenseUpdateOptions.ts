export class ClientSettings_LicenseUpdateOptions {
  clientAccountNumber: string;
  activationCode: string;

  constructor(activationCode: string) {
    this.activationCode = activationCode;
  }

  UpdateValue = (prop: string, value: string) => {
    this[prop] = value;
  }

}
