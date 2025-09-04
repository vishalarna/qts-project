import { Entity } from "../Entity";

export class TestItemMcq extends Entity {
  testItemId!: any;
  choiceDescription!: string;
  isCorrect!: boolean;
  number !: number;
}
