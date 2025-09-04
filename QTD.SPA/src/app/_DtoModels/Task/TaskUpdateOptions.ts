export class TaskUpdateOptions {
  subdutyAreaId!: any;
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
  taskCriteriaUpload?: any;

  isMeta!: boolean;
  isReliability!: boolean;
  effectiveDate?: Date;
  changeNotes?: string;
}
