import { TestItemVM } from "@models/TestItem/TestItemVM";
import { FillintheBlank } from "@models/TestItemFillBlank/FillintheBlank";
import { MatchColumns } from "@models/TestItemMatch/MatchColumns";

export class SubmitTestModel {
  testItem: TestItemVM;
  correct: boolean;
  completionStatus: string;
  userAnswer: string;
  clearDescription: string;
  trueFalseAnswer: string;
  fillInTheBlankkAnswer: string[];
  mcqAnswer: string;
  shortAnswer: string[];
  multiCorrectAnswers: string[];
  matchValueAnswer: (string | null)[];
  multipleCorrectAnswer: string[];
  blankIndexWithAnswer: FillintheBlank[];
  matchValueWithCorrectValue: MatchColumns[];
  maximumScore: number;
  passingScore: number;
  totalScore: number;
  passFailStatus: string;
  providerName: string;
  ilaNumber: string;
  startDate: Date;
  endDate: Date;
  showSubmittedAnswers?: boolean;
  showCorrectIncorrectAnswers?: boolean;
  totalTestDuration : string;
}