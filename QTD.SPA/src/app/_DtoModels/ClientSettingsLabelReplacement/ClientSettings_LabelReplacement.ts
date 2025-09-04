import { Entity } from "../Entity";

export class ClientSettings_LabelReplacement extends Entity {
  defaultLabel!: string;
  labelReplacement!: string;

  constructor(defaultLabel: string, labelReplacement: string){
    super();
    this.defaultLabel = defaultLabel;
    this.labelReplacement = labelReplacement
  }
}
