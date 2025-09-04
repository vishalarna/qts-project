import { ReportFilterOption } from "./ReportFilterOption";

export class ReportCreateOrUpdateOptions{
    reportSkeletonId:number;
    internalReportTitle:string;
    filters:ReportFilterOption[];
    displayColumns:string[];
}