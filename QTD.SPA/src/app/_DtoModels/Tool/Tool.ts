import { ToolCategory } from '@models/ToolCategory/ToolCategory';
import { Entity } from '../Entity';
import { Task_Tool } from '../Task_Tool/Task_Tool';
import { ToolGroup_Tool } from '../ToolGroup_Tool/ToolGroup_Tool';

export class Tool extends Entity {
  name!: string;
  number!: any;
  description!: string;
  active!: boolean;
  toolGroup_Tools!: ToolGroup_Tool[];
  task_Tools!: Task_Tool[];

  hyperlink: string;

  effectiveDate: Date | string | null;
  toolCategoryId: number;
  toolCategory?:ToolCategory;
}
