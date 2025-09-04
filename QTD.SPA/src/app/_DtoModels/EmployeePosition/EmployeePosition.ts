import { Employee } from '../Employee/Employee';
import { Entity } from '../Entity';
import { Position } from '../Position/Position';
export class EmployeePosition extends Entity {
  employeeId!: any;
  positionId!: any;
  startDate!: Date;
  endDate!: Date | string | null;
  trainee!: boolean;
  qualificationDate!: Date | string | null;
  trainingProgramVersion:string;
  managerName:string
  employee!: Employee;
  position!: Position;
  isSignificant!:boolean;
  isCertificationNotRequired?:boolean;
}
