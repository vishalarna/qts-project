import { Entity } from "../Entity";
import { ILATraineeEvaluation } from "../ILATraineeEvaluation/ILATraineeEvaluation";

export class Test extends Entity {
  testStatusId!: any;
  testTitle!: string;
  ilaTraineeEvaluations!:ILATraineeEvaluation[];
  randomizeDistractors !: boolean;
  effectiveDate: string;
  isPublished: boolean;
  randomizeQuestionsSequence:boolean;
/*   testNotes!: string; */
}
