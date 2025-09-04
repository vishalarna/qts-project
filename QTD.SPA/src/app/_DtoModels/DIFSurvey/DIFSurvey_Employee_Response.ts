import { Entity } from "@models/Entity";
import { DIFSurvey_Employee } from "./DIFSurvey_Employee";
import { DIFSurvey_Task } from "./DIFSurvey_Task";

export class DIFSurvey_Employee_Response extends Entity   {
    difSurveyEmployeeId!:string;
    difSurveyTaskId!:string;
    difficulty:number;
    importance: number;
    frequency:number;
    na!:boolean;
    comments:string;
    difSurveyEmployee:DIFSurvey_Employee;
    difSurveyTask:DIFSurvey_Task;
}