import { SafeHtml } from "@angular/platform-browser";
import { TestItemFillBlank } from "../TestItemFillBlank/TestItemFillBlank";
import { TestItemMatch } from "../TestItemMatch/TestItemMatch";
import { TestItemMcq } from "../TestItemMcq/TestItemMcq";
import { TestItemShortAnswer } from "../TestItemShortAnswer/TestItemShortAnswer";
import { TestItemTrueFalse } from "../TestItemTrueFalse/TestItemTrueFalse";

export class TestItemVM  {
  id!: string;
  testItemTypeId!: string;
  description!: string;
  number!: string;
  testItemType!:string;
  testItemFillBlanks: TestItemFillBlank[] = [];
  testItemMCQs:TestItemMcq[] = [];
  testItemMatches : TestItemMatch[] = [];
  testItemShortAnswers:TestItemShortAnswer[] = [];
  testItemTrueFalses: TestItemTrueFalse[] = [];
  descriptionForFillBlanks!:SafeHtml;
}
