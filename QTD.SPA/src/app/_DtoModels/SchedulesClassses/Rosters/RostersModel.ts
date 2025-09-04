export class RoastersModel {
  classRoasterId!: any;
  classScheduleId!: any;
  testId!: any;
  testTypeId!: any;
  empId!: any;
  disclaimer!: boolean;
  grade!: string;
  interrupted!: boolean;
  restarted!: boolean;
  completedDate!: any;
  releaseDate!: any;
  score: number | null;
  empIDs!: any[];
  empEmail!: string;
  employeeName!: string;
  image!: string;
  testItemType!: string;
  retakeOrder?:number;
}
