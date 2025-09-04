import { IdentityProviderVM } from '../IdentityProvider/IdentityProviderVM';
import { Entity } from '../Entity';
import { Instance } from './Instance';

export class InstanceSetting extends Entity {
  id: number;
  instanceId: number;
  databaseName: string;
  databaseVersion: string;
  instance: Instance;
  scormTenant: string;
  clientAccountNumber: string;
  defaultIdentityProvider:IdentityProviderVM
  defaultIdentityProviderId:string;
  mfaEnabled:boolean;
  publicUrl: string;
}
