import { Entity } from '../Entity';
import { SaftyHazard } from '../SaftyHazard/SaftyHazard';

export class SaftyHazard_Category extends Entity {
  description!: string;
  title!:string;
  priority!: boolean;
  saftyHazards!: SaftyHazard[];
  effectiveDate?:Date;
  notes?:string;
  number?:any;
  hyperLinks:any;
  active:any;
}
