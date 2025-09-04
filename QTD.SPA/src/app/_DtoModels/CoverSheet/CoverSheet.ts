import { Entity } from "../Entity";

export class CoverSheet extends Entity {
  coversheetTitle!:string;
  coversheetTypeId!:any;
  coversheetInstructions!:string;
  coversheetFileUpload!:Uint8Array;
  coversheetImageUpload!:string;
}
