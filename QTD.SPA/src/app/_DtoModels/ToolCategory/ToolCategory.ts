import { Entity } from '../Entity';
import { Tool } from '../Tool/Tool';

export class ToolCategory extends Entity {
  title: string;

  description: string;

  website: string;

  effectiveDate: Date | string | null;

  notes: string;

  tools: Tool[];
}
