import { Entity } from "@models/Entity";
import { Position } from "@models/Position/Position";
import { DIFSurvey_DevStatus } from "./DIFSurvey_DevStatus";
import { DIFSurvey_Employee } from "./DIFSurvey_Employee";
import { DIFSurvey_Task } from "./DIFSurvey_Task";

export class DIFSurvey extends Entity   {
    title!: string;
    positionId!: string;
    startDate!: Date;
    dueDate!: Date;
    instructions: string;
    devStatusId: string;
    releasedToEMP: boolean; 
    historicalOnly: boolean;
    position:  Position;
    employees: DIFSurvey_Employee[];
    tasks: DIFSurvey_Task[];
    devStatus:DIFSurvey_DevStatus;
}