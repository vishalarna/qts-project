import { DIFSurveyTaskVM } from './DIFSurveyTaskVM'; // Import DIFSurveyTaskVM if it's defined in a separate file
import { DIFSurvey_EmployeeVM } from './DIFSurvey_EmployeeVM';

export class DIFSurveyVM {
  id: string;
  surveyTitle: string;
  positionId: string;
  startDate: Date;
  dueDate: Date;
  instructions: string;
  releasedToEMP: boolean;
  devStatusId: string;
  isActive: boolean;
  tasks: DIFSurveyTaskVM[];
  employees:DIFSurvey_EmployeeVM[];
  surveyStatus:string;
}
