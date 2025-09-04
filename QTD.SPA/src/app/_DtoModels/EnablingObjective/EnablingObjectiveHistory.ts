import { Entity } from "../Entity";

export class EnablingObjectiveHistory extends Entity{
  enablingObjectiveId!:string;
  oldStatus!:boolean;
  newStatus!:boolean;
  changeEffectiveDate!:Date;
  changeNotes!:string;
}
