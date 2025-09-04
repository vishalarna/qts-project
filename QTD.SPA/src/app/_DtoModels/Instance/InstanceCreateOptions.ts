export class InstanceCreateOptions {
  clientName!: string;
  scormTenant!: string;
  name!: string;
  createDatabase!: boolean;
  databaseName!: string;
  isInBeta!: boolean;
  clientAccountNumber!: number;
  identityProviderId:string;
  mfaEnabled:boolean;
}
