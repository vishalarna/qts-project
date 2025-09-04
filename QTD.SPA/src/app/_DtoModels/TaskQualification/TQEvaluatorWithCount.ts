import { Position } from "../Position/Position";

export class TQEvaluatorWithCount{
  evaluatorId!:any;
  evaluatorFName!:string;
  evaluatorLName!:string;
  evaluatorName!:string;
  count!:number;
  positions:Position[] = [];
  positionTitle!:string;
}
