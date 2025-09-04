import { CBT_ScormRegistration } from "../CBT/CBT_ScormRegistration";
import { Employee } from "../Employee/Employee";
import { ClassSchedules } from "./ClassSchedules";

export class ClassSchedule_Employee{
  id:number;
  classScheduleId!:any;
  preTestStatusId!:any;
  testStatusId!:any;
  retakeStatusId!:any;
  cbtStatusId!:any;
  finalScore!:number;
  finalGrade!:string;
  gradeNotes!:string;
  employeeId!:any;
  isEnrolled!:boolean;
  isWaitlisted!:boolean;
  employee!:Employee;
  //special fields needed for TODO panels in class schedule overview;
  testId?:any;
  testTitle?:string;
  classSchedule!:ClassSchedules;
  scormRegistrations!:CBT_ScormRegistration[];
  completionDate?:any;
  plannedDate?:any;
}
