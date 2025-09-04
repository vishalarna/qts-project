import { Entity } from '@models/Entity';
import { DIFSurveyResponseVm } from './DIFSurveyResponseVM';

export class DIFSurveyViewResponseVm extends Entity {
  title: string;
  instructions: string;
  completedDate!: Date | null;
  startDate:Date | null;
  dueDate:Date | null;
  difSurveyResponseVM:DIFSurveyResponseVm[];
  additionalComments:string | null;
}
