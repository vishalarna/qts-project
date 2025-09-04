import { Entity } from "../Entity";

export class TestItem_History extends Entity{
  changeNotes !: string;
  effectiveDate !: Date;
  newStatus !: boolean;
  oldStatus !: boolean;
  testItemId !: any;
}
