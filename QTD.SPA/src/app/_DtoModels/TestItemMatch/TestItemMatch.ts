import { Entity } from "../Entity";

export class TestItemMatch extends Entity {
  testItemId!: any;
  choiceDescription!: string;
  matchDescription!: string;
  matchValue!: string;
  correctValue!: string;
  number!:number;
  originalCorrectValue!:string;
  originalMatchValue!:string;
}
