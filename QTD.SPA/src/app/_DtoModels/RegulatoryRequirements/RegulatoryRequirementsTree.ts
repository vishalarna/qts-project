export interface RegulatoryRequirementsTree {
  id: any;
  description: string;
  children?: RegulatoryRequirementsTree[];
  active?: boolean;
  parent?: RegulatoryRequirementsTree;
}
