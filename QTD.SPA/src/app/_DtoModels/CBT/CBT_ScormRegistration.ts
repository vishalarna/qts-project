import { Entity } from "../Entity";
import { ClassSchedule_Employee } from "@models/SchedulesClassses/ClassSchedule_Employee";

export class CBT_ScormRegistration extends Entity{
  cbtScormUploadId!:any;
  classScheduleEmployeeId!:any;
  launchLink!:string;
  score!:number;
  grade!:string;
  registrationSuccess!:number;
  classScheduleEmployee!:ClassSchedule_Employee;
}
