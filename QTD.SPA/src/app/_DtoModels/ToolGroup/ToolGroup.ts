import { Entity } from '../Entity';
import { ToolGroup_Tool } from '../ToolGroup_Tool/ToolGroup_Tool';

export class ToolGroup extends Entity {
  description!: string;
  active!: boolean;
  toolGroup_Tools!: ToolGroup_Tool[];
}
