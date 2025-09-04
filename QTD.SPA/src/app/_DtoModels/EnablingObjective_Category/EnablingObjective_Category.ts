import { EnablingObjective_SubCategory } from '../EnablingObjective_SubCategory/EnablingObjective_SubCategory';
import { Entity } from '../Entity';

export class EnablingObjective_Category extends Entity {
  description!: string;
  number!: number;
  title!: string;
  active !: boolean;
  effectiveDate!:any;
  reason!:string;
  enablingObjective_SubCategories!: EnablingObjective_SubCategory[];
}
