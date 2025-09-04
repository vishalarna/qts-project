export class ProcedureTree {
    id: any;
    description: string;
    children?: ProcedureTree[];
    checkbox?: boolean;
    selected?: boolean;
    indeterminate?: boolean;
    parent?: ProcedureTree;
    active?: boolean;
    number?: any;
  }