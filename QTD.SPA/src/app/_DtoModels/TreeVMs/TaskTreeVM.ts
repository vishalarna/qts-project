export class DutyAreaTreeVM{
  id!:any;
  title!:string;
  letter!:string;
  number!:any;
  active!:boolean;
  subdutyAreas:SubDutyAreaTreeVM[] = [];
}

export class SubDutyAreaTreeVM{
  id!:any;
  title!:string;
  active!:boolean;
  subNumber!:any;
  tasks:TaskTreeDataVM[] = []
}

export class TaskTreeDataVM{
  id!:any;
  description!:string;
  number!:any;
  active!:boolean;
  isMeta!:boolean;
  isReliability!:boolean;
  position_Tasks?:any[];
}
