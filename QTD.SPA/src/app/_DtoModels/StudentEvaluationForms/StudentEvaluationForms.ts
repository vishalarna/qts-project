import { Entity } from "../Entity";


export class StudentEvaluationForms extends Entity {
  name!:string;
  ratingScaleId!:any;
  isShared!:boolean;
  isAvailableForAllILAs!:boolean;
  isNAOption!:boolean;
  includeComments!:boolean;
}
