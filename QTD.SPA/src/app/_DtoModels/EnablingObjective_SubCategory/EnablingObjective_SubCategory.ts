import { EnablingObjective_Category } from "../EnablingObjective_Category/EnablingObjective_Category";
import { EnablingObjective_Topic } from "../EnablingObjective_Topic/EnablingObjective_Topic";
import { Entity } from "../Entity";

export class EnablingObjective_SubCategory extends Entity {
 title!: string;
 description!: string;
 categoryId!: any;
 number!: number;
 effectiveDate!:any;
 reason!:string;
 enablingObjectives_Category!: EnablingObjective_Category;
 enablingObjective_Topics!: EnablingObjective_Topic[];
}
