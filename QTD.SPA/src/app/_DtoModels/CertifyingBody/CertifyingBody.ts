import { Certification } from '../Certification/Certification';
import { Entity } from '../Entity';

export class CertifyingBody extends Entity {
  id:any;
  name!: string;
  desc!:string;
  website!:string;
  EffectiveDate: Date | string;
  notes!:string;
  certifications!: Certification[];
  isNERC?:boolean;
  enableCertifyingBodyLevelCEHEditing:boolean;
}
