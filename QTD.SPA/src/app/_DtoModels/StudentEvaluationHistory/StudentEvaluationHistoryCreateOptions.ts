export class StudentEvaluationHistoryCreateOptions {

    studentEvaluationId: number;

    effectiveDate: Date | string;

    studentEvaluationNotes: string;

    actionType: string;

    studentEvaluationIds: number[];
}