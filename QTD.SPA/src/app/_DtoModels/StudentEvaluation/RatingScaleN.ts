import { Entity } from "../Entity";

export class RatingScaleN extends Entity{
  ratingScaleDescription!:string;
  ratingScaleExpanded:RatingScaleExpanded[]= [];
}

export class RatingScaleExpanded extends Entity{
  description!:string;
  ratings!:number;
}
