import { SafeHtml } from "@angular/platform-browser";
import { EnablingObjective } from "../EnablingObjective/EnablingObjective";
import { Entity } from "../Entity";
import { TaxonomyLevel } from "../TaxonomyLevel/TaxonomyLevel";
import { TestItemFillBlank } from "../TestItemFillBlank/TestItemFillBlank";
import { TestItemMatch } from "../TestItemMatch/TestItemMatch";
import { TestItemMcq } from "../TestItemMcq/TestItemMcq";
import { TestItemShortAnswer } from "../TestItemShortAnswer/TestItemShortAnswer";
import { TestItemTrueFalse } from "../TestItemTrueFalse/TestItemTrueFalse";
import { TestItemType } from "../TestItemType/TestItemType";

export class TestItem extends Entity {
  testItemTypeId!: any;
  taxonomyId!: any;
  isActive: boolean = true;
  description!: string;
  image!: string;
  eoId!:string;
  testItemType!:TestItemType;
  taxonomyLevel!:TaxonomyLevel;
  number !: string;
  testItemFillBlanks: TestItemFillBlank[] = [];
  testItemMCQs:TestItemMcq[] = [];
  testItemMatches : TestItemMatch[] = [];
  testItemShortAnswers:TestItemShortAnswer[] = [];
  testItemTrueFalses: TestItemTrueFalse[] = [];
  enablingObjective!:EnablingObjective;
  descriptionForFillBlanks!:SafeHtml;
}
