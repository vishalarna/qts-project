import { Entity } from "@models/Entity";
import { Task } from "@models/Task/Task";
import { Employee } from "@models/Employee/Employee";
import { DIFSurvey_Employee_Response } from "./DIFSurvey_Employee_Response";
import { DIFSurvey } from "./DIFSurvey";
import { DIFSurvey_Task_Status } from "./DIFSurvey_Task_Status";
import { DIFSurvey_Task_TrainingFrequency } from "./DIFSurvey_Task_TrainingFrequency";

export class DIFSurvey_Task extends Entity   {
    difSurveyId!:string;
    taskId!:string;
    averageDifficulty!:number;
    averageImportance!:number;
    averageFrequency!:number;
    trainingStatusCalculatedId!:string;
    trainingStatusOverrideId:string;
    difSurvey_Task_TrainingFrequencyId:string;
    comments:string;
    commentingEmployeeId:any;
    employee:Employee;
    responses:DIFSurvey_Employee_Response[];
    difSurvey:DIFSurvey;
    task:Task;
    difSurveyTaskStatus:DIFSurvey_Task_Status;
    Task:Task;
    trainingStatus_Calculated:DIFSurvey_Task_Status;
    trainingStatusOverride:DIFSurvey_Task_Status;
    difSurvey_Task_TrainingFrequency:DIFSurvey_Task_TrainingFrequency;

}