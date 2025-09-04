import { PublicClassScheduleIlaVM } from "./PublicClassScheduleIlaVM";
import { PublicClassScheduleVM } from "./PublicClassScheduleVM";

export class PublicClassScheduleRequestVM {
  id:                     string;
  classId:                string;
  classScheduleEmployeeId:string;
  firstName:              string;
  lastName:               string;
  emailAddress:           string;
  company:                string;
  nercCertNumber?:         string;
  nercCertType:           string;
  expirationDate?:        Date | string;
  requestedAction:        RequestedAction;    
  publicClassScheduleIla: PublicClassScheduleIlaVM;
  publicClassSchedule:    PublicClassScheduleVM;
}

export enum RequestedAction{
    NoAction,
    Accept, 
    Deny
}