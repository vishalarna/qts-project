import { Entity } from "../Entity";
import { RatingScaleN } from "./RatingScaleN";

export class StudentEvaluation extends Entity{
  ratingScaleId!:any;
  title!:string;
  instructions!:string;
  isPublished!:boolean;
  isAvailableForAllILAs!:boolean;
  isAvailableForSelectedILAs!:boolean;
  isIncludeCommentSections!:boolean;
  isAllowNAOption!:boolean;
  ratingScaleN!:RatingScaleN;
}
