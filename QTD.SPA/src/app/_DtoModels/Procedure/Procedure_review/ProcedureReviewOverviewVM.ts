export class ProcedureReviewOverviewVM{
    id:string;
    startDateTime : Date|string;
    endDateTime : Date|string;
    isStarted : boolean;
    isPublished : boolean;
    active : boolean;
    procedureNumber : string;
    procedureTitle : string;
    procedureReviewTitle : string;
    issuingAuthorityTitle : string;
    issuingAuthorityId : string;
}