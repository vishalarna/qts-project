import { Entity } from '../Entity';
import { Instructor } from '../Instructors/Instructor';

export class Instructor_Category extends Entity 
{
  ICategoryDescription!: string;
  iCategoryTitle!:string;
  description!: string;
  title!:string;
  //instructors!: Instructor[];
  effectiveDate?:Date;
  categoryNotes?:string;
  website:any;
}
