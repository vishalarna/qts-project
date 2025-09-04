export class TaskCreateOptions {
  subdutyAreaId!:number;
  description!: string;
  number!: number;
  conditions!: string;
  standards!: string;
  critical!: boolean;
  tools!: string;
  references!: string;
  requiredTime!: number;
  majorVersion!: number;
  minorVersion!: number;
  isSignificant!: boolean;
  isMeta!:boolean;
  isReliability!:boolean;
  taskCriteriaUpload?:string;
  effectiveDate?:Date;
  changeNotes?:string;
}
