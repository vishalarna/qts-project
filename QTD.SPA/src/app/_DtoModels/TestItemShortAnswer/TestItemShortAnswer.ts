import { Entity } from "../Entity";

export class TestItemShortAnswer extends Entity {
  testItemId!: any;
  responses!: string;
  isCaseSensitive!: boolean;
  acceptableResponses!: any;
  number !: number;
}
