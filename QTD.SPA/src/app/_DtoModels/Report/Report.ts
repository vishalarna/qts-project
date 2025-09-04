import { Entity } from "../Entity";
import { ReportDisplayColumn } from "./ReportDisplayColumn";
import { ReportFilter } from "./ReportFilter";

export class Report extends Entity {
    reportSkeletonId: number;
    clientUserId: number;
    name: string;
    title: string;
    filters:ReportFilter[];
    displayColumns:ReportDisplayColumn[];
}