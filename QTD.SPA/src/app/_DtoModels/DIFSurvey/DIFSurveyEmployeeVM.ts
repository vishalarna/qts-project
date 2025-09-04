import { Entity } from "@models/Entity";

export class DIFSurveyEmployeeVM extends Entity   {
  difSurveyId: string;
  title: string;
  completionDate: Date | null;
  dueDate: Date | null;
  statusId: number;
  status:number;
}