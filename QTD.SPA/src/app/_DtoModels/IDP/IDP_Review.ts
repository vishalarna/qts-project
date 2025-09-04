import { IDP_ReviewStatus } from "./IDP_ReviewStatus";

export class IDP_Review{
  title!:string;
  statusId!:any;
  instructions!:string;
  comments!:string;
  releaseDate?: any;
  completedDate?:any;
  endDate?:any;
  idP_ReviewStatus!:IDP_ReviewStatus;
}
