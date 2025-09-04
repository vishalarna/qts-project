export class EmployeePositionUpdateOptions {
  employeePositionId?:any;
  employeeId!: any;
  positionId!: any;
  startDate!: Date | string;
  endDate!: Date | string | null;
  trainee!: boolean;
  qualificationDate!: Date | string | null ;
  managerName?:string;
  trainingProgramVersion?:string;
  isCertificationRequired?:boolean;
  isSignificant?:boolean;
}
