export class ToolCreateOptions {
  description?: string;
  active?: boolean;

  toolCategoryId: number;

  number: string;

  name: string;

  notes: string;

  hyperlink: string;

  effectiveDate: Date | string | null;

  upload: any;
}