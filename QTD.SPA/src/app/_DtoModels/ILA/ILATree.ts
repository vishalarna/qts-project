import { Entity } from "@models/Entity";

export class ILATree{
  id: any;
  description: string;
  children?: ILATree[];
  active?: boolean;
  checkbox?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: ILATree;
}
