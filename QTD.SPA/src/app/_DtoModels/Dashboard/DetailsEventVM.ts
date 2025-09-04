export class DetailsEventVM{
  firstName!:string;
  lastName!:string;
  middleName!:string;
  picture!:string;
  ilaList:ILAListVM[] = []
  procedureReviewList:ProcedureReviewList[] = []
  taskQualificationList:TaskQualReviewList[] = []
}

export class ILAListVM{
  endDateTime!:Date;
  ilaId!:any;
  ilaTitle!:string;
  isCollapsable!:boolean;
  location!:string;
  startDateTime!:Date;
  type!:string;
  parentId!:any;
  trainings:ILATrainingsVM[] = [];
}

export class ILATrainingsVM{
  dueDate!:any;
  id!:any;
  status!:string;
  testType!:string;
  title!:string;
  type!:string;
  parentId!:any;
}

export class ProcedureReviewList{
  dueDate!:any;
  endDateTime!:any;
  startDateTime!:any;
  status!:string;
  title!:string;
  location!:string;
  procedureId!:any;
  id!:any;
  type!:string;
  isCollapsable!:boolean;
}

export class TaskQualReviewList{
  id!:any;
  parentId!:any;
  type!:string;
  title!:string;
  status!:string;
  startDateTime!:any;
  endDateTime!:any;
}
