import { Entity } from '../Entity';
import { Procedure } from '../Procedure/Procedure';

export class Procedure_IssuingAuthority extends Entity {
  title!: string;
  website!: string;
  effectiveDate!: Date;
  notes!: string;
  isActive!: boolean;
  isDeleted!: boolean;
  description!: string;
  procedures !: Procedure[];
}
