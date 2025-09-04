export interface SHLatestActivityVM {
  id: number;

  title: string;

  activityDesc: string;

  createdBy: string;

  createdDate: Date | string | null;

  modifiedBy: string;

  modifiedDate: Date | string | null;
}
