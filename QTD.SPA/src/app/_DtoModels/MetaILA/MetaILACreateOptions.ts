export class MetaILACreateOptions {
  name: string;
  description: string;
  metaILAStatusId: any;
  reason: string;
  effectiveDate: Date;
  isPriority!: boolean;
  metaILA_SummaryFinalTestId: string;
  metaILA_SummaryRetakeTestId: string;
  studentEvaluationId: string;
  providerId:string;
}
