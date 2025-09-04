import { EnablingObjective_Question } from '../EnablingObjective_Question/EnablingObjective_Question';
import { Entity } from '../Entity';
import { Task_Question } from '../Task_Question/Task_Question';
import { Version_EnablingObjective } from '../Version_EnablingObjective/Version_EnablingObjective';
import { Version_Task } from '../Version_Task/Version_Task';

export class Version_EnablingObjective_Question extends Entity {
  eoQuestionId!: any;
  versionEOId!: any;
  question!: string;
  answer!: string;
  Version_EnablingObjective!: Version_EnablingObjective;
  enablingObjective_Question!: EnablingObjective_Question;
}
