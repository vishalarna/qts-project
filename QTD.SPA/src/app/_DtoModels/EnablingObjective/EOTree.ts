import { EO_LinkPositions } from "./EO_LinkPositions";

export class EOTree {
    id: any;
    description: string;
    children!: EOTree[];
    checkbox?: boolean;
    selected?: boolean;
    indeterminate?: boolean;
    parent?: EOTree;
    active?: boolean;
    isLink?: boolean;
    IsEO?: boolean = false;
    level?:string;
    isMeta?: boolean = false;
    isSkillQualification?: boolean = false;
    position_SQs?: EO_LinkPositions[] = [];
  }