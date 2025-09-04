import { Employee } from "../../Employee/Employee";
import { Entity } from "../../Entity";
import { Test } from "../../Test/Test";
import { TestType } from "../../TestType/TestType";

export class ClassSchedule_Rosters extends Entity{
  classScheduleId!:any;
  testId!:any;
  testTypeId!:any;
  empId!:any;
  disclaimer!:string;
  grade!:string;
  interrupted!:boolean;
  restarted!:boolean;
  completedDate!:any;
  releaseDate!:any;
  score!:number;
  test!:Test;
  testType!:TestType;
  employee!:Employee;
}
