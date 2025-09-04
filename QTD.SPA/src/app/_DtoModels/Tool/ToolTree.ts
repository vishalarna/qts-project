export interface ToolTree {
  id: any;
  description: string;
  children?: ToolTree[];
  active?: boolean;
  parent?: ToolTree;
}
