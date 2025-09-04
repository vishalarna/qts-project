import { TestItemVM } from "@models/TestItem/TestItemVM";
import { FillintheBlank } from "@models/TestItemFillBlank/FillintheBlank";
import { MatchColumns } from "@models/TestItemMatch/MatchColumns";
import { ShortAnswers } from "@models/TestItemShortAnswer/ShortAnswers";

export class ReviewTestModel {
  testItem: TestItemVM;
  status: string;
  userAnswer: string;
  multipleCorrectAnswer: string[];
  blankIndexWithAnswer: FillintheBlank[];
  matchValueWithCorrectValue: MatchColumns[];
  shortAnswerWithCorrects: ShortAnswers[];
}
