import { CBT_ScormRegistration } from "../../CBT/CBT_ScormRegistration";

export class RosterOverviewVM {
  employeeName!:string;
  employeeEmail!:string;
  employeeImage!:any;
  pretestStatus!:string;
  cbtStatus!:string;
  testStatus!:string
  retakeCount!:number;
  score!:number;
  grade!:string;
  gradeNotes!:string;
  classEmployeeId!:any;
  isTestReleased!:boolean;
  isPreTestReleased!:boolean;
  isCBTReleased!:boolean;
  evaluationCompletedDate:Date|string;
  classScheduleEmployeeId:string;
  taskQualificationCompleted:boolean;
}
