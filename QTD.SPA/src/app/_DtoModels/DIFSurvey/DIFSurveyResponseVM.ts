import { Entity } from "@models/Entity";


export class DIFSurveyResponseVm extends Entity {
    difSurveyTaskId: string;
    number:string;
    taskDescription:string;
    difficulty!: number;
    importance!: number;
    frequency!: number;
    na!: boolean;
    comments!: string;
  }