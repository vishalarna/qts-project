export class EmployeePositionCreateOptions {
  positionId!: any;
  startDate!: Date | string;
  posQualificationDate: Date | string | null;
  endDate: Date | string | null;
  isTrainee!: boolean;
  isSignificant!:boolean;
  ManagerName: string;
  TrainingProgramVersion: string;
  isCertificationRequired?:boolean;
}
