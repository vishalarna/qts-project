import { EmployeePosition } from '../EmployeePosition/EmployeePosition';
import { Entity } from '../Entity';
import { Task_Position } from '../Task_Position/Task_Position';
import { TrainingProgram } from '../TrainingProgram/TrainingProgram';

export class Position extends Entity {
  id!: any;
  
  name!: string;
  acronym!:string;

  positionNumber!: any;
  positionAbbreviation!: any;
  positionTitle!: any;
  positionDescription!: any;
  hyperLink!: any;
  positionsFileUpload!: string;
  fileName!:any;
  isPublished!: boolean;
  revisionNumber!: string;
  effectiveDate!: Date;
  employeePositions!: EmployeePosition[];
  trainingPrograms!: TrainingProgram[];
  task_Positions!: Task_Position[];
  position_Tasks!:any[];
  position_Employees!: EmployeePosition[];
  position_SQs!:any[];
}
