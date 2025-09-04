import { Entity } from '../Entity';
import { InstanceSetting } from './InstanceSetting';

export class Instance extends Entity {
  clientId!: any;
  name!: string;
  isInBeta!: boolean;
  instanceSetting: InstanceSetting;
}
