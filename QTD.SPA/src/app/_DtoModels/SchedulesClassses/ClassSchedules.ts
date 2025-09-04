import { Entity } from "../Entity";
import { ILA } from "../ILA/ILA";
import { Instructor } from "../Instructors/Instructor";
import { Location } from "../Locations/Location";
import { Provider } from "../Provider/Provider";
import { ClassSchedule_Employee } from "./ClassSchedule_Employee";
import { ClassSchedule_Rosters } from "./Rosters/ClassSchedule_Rosters";

export class ClassSchedules extends Entity{
  providerId!:any;
  ilaid!:any;
  startDateTime!:Date;
  endDateTime!:Date;
  instructorId!:any;
  locationId!:any;
  specialInstructions!:any;
  webinarLink!:any;
  location!:Location;
  instructor!:Instructor;
  classSchedule_Employee!:ClassSchedule_Employee[];
  classSchedule_Rosters!:ClassSchedule_Rosters[];
  recurrenceId !:any;
  isRecurring:boolean;
  ila!:ILA;
  provider!:Provider;
  isStartAndEndTimeEmpty:boolean;
  classSize: number;
}
