import { FillintheBlank } from "@models/TestItemFillBlank/FillintheBlank";
import { MatchColumns } from "@models/TestItemMatch/MatchColumns";

export class EmployeeAnswerModel {
  testId: string;
  questionId: string;
  testTypeId: string;
  employeeId: string;
  testItemTypeId: string;
  userAnswer: string;
  multipleCorrectAnswer: string[];
  blankIndexWithAnswer: FillintheBlank[];
  matchValueWithCorrectValue: MatchColumns[];
}
