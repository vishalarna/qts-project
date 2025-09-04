export interface ProcedureLatestActivityVM {
  id: number;

  procedureTitle: string;

  activityDesc: string;

  createdBy: string;

  createdDate: Date | string | null;

  modifiedBy: string;

  modifiedDate: Date | string | null;
}
