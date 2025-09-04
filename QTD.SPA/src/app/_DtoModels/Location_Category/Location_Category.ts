import { Entity } from '../Entity';
import { Location } from '../Locations/Location';

export class Location_Category extends Entity 
{
  
  locCategoryDesc!: string;
  locCategoryTitle!:string;
  website:any;
  locCategoryWebsite:any;
  categoryNotes:any;
  description!: string;
  title!:string;
  EffectiveDate: Date | string;
  locations!:Location[];
}
