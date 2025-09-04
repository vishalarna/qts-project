export class SafetyHazardTree{
  id: any;
  description: string;
  children?: SafetyHazardTree[];
  active?: boolean;
  parent?: SafetyHazardTree;
}
