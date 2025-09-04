import { SafetyHazard_Set } from "../SaftyHazard_Set/SafetyHazard_Set";

export class SaftyHazardCompactOption{
  id!:any;
  saftyHazardCategoryId!:any;
  title!:any;
  active!:boolean;

  description!:string;
  revisionNumber!:string;
  effectiveDate!:Date;
  number!:string;
  hyperLink!:string;
  safetyHazardSetIds!:number[];
}
