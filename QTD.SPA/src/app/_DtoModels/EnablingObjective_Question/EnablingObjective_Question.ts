import { EnablingObjective } from '../EnablingObjective/EnablingObjective';
import { Entity } from '../Entity';
import { Version_EnablingObjective_Question } from '../Version_EnablingObjective_Question/Version_EnablingObjective_Question';
import { Version_Task_Question } from '../Version_Task_Question/Version_Task_Question';

export class EnablingObjective_Question extends Entity {
  eoId!: any;
  question!: string;
  answer!: string;
  eo!: EnablingObjective;
  questionNumber !: number;
  version_EO_Questions!: Version_EnablingObjective_Question[];
}
