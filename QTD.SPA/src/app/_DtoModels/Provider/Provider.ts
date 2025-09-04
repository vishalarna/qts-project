import { Entity } from '../Entity';
import { ILA } from '../ILA/ILA';

export class Provider extends Entity {
  name!: string;
  number!: number;
  providerLevelId!: any;
  contactName!: string;
  contactTitle!: string;
  contactPhone!: number;
  contactExt!: number;
  contactMobile!: number;
  contactEmail!: string;
  companyWebsite!: string;
  repName!: string;
  repTitle!: string;
  repPhone!: number;
  repEmail!: string;
  repSignature!: string;
  isPriority!: boolean;
  isNERC!: boolean;
  ilAs!: ILA[];
}
