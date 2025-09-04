export class TaskWithCountR5R6Options {
  description!: string;
  number!: any;
  id!: any;
  linkCount!: number;
  trainingGroupLinkCount!: number;
  active!: boolean;
  letter?:string;
  isUsedForTQ!:boolean;
  isRR?:boolean;
  positionTaskId!:number;
  isR5Impacted!:boolean;
  isR6Impacted!:boolean;
  r6ImpactedReason: string;
  r6ImpactedEffectiveDate: Date;
  r5ImpactedTasks:any[];
}
