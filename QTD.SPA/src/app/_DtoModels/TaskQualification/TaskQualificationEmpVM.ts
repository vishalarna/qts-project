export class TaskQualificationEmpVM {
  empId!: any;
  taskId!:any;
  empName!: string;
  empImage!: string;
  empEmail!: string;
  taskNumber!:number;
  number!:string;
  taskDescription!:string;
  empReleaseDate?: Date;
  qualificationDate?: Date|string;
  evaluatorName!: string;
  dueDate?: Date;
  criteriaMet!: string;
  comments!: string;
  id?:any;
  status !: string;
  toolTip!:string;
  requiredRequals!:string;
  posIds:any[] = [];
  posNames!:string;
  isReliability:boolean;
  active:boolean;
  isRecalled:boolean;
}
