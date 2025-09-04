import { Entity } from "../Entity";

export class Procedure_StatusHistory extends Entity{
  procedureId!: any;
  oldStatus!: boolean;
  newStatus!: boolean;
  changedOn!: string;
  changedBy!: string;
  changeNotes!: string;
  changeEffectiveDate!: Date;
}
