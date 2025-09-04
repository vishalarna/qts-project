export class StudentEvaluationCreateOptions {

  ratingScaleId: number;

  title: string;

  instructions?: string;

  isAvailableForAllILAs?: boolean | null;

  isAvailableForSelectedILAs?: boolean | null;

  isIncludeCommentSections?: boolean | null;

  isAllowNAOption?: boolean | null;

  mode?: string | null

  effectiveDate?: Date | string;

  studentEvaluationNotes?: string;

  stdEvalId?: number

  anotherMode?: any

  notes?:string;
}
