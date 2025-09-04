import { EnablingObjective } from '../EnablingObjective/EnablingObjective';
import { EnablingObjective_SubCategory } from '../EnablingObjective_SubCategory/EnablingObjective_SubCategory';
import { Entity } from '../Entity';

export class EnablingObjective_Topic extends Entity{
  description!: string;
  subCategoryId!: any;
  number!: number;
  title!: string;
  effectiveDate!:any;
  enablingObjectives_SubCategory!: EnablingObjective_SubCategory;
  enablingObjectives!: EnablingObjective[];
}
