import { Entity } from "../Entity";

export class EnablingObjective_SubCategoryHistory extends Entity{
  enablingObjectiveSubCategoryId!:string;
  oldStatus!:boolean;
  newStatus!:boolean;
  changeEffectiveDate!:Date;
  changeNotes!:string;
}
