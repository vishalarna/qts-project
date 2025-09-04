export interface LocationLatestActivityVM{
    locId: number;

    locCategoryId: number;

    locNumber: string;

    locName: string;

    activityDesc: string;

    createdBy: string;
  
    createdDate: Date | string | null;
  
    modifiedBy: string;
  
    modifiedDate: Date | string | null;
}