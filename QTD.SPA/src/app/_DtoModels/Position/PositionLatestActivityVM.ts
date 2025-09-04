export interface PositionLatestActivityVM {
  id: number;

  positionName: string;

  positionNum: string;

  activityDesc: string;

  createdBy: string;

  createdDate: Date | string | null;

  modifiedBy: string;

  modifiedDate: Date | string | null;
}
