import { Entity } from "@models/Entity";
import { DIFSurvey } from "./DIFSurvey";
import { Employee } from "@models/Employee/Employee";
import { DIFSurvey_Employee_Status } from "./DIFSurvey_Employee_Status";
import { DIFSurvey_Employee_Response } from "./DIFSurvey_Employee_Response";

export class DIFSurvey_Employee extends Entity   {
    difSurveyId!:string;
    employeeId!:string;
    started:boolean;
    complete:boolean;
    statusId!:string;   
    releaseDate: Date | null;
    completedDate: Date | null;
    comments:string;
    responses:DIFSurvey_Employee_Response[];
    status:DIFSurvey_Employee_Status;
    employee:Employee;
    difSurvey:DIFSurvey;
}