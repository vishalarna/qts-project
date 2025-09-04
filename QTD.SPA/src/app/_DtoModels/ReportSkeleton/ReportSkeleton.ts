import { Entity } from "../Entity";
import { ReportSkeletonColumn } from "./ReportSkeletonColumn";
import { ReportSkeletonFilter } from "./ReportSkeletonFilter";

export class ReportSkeleton extends Entity {
    defaultTitle: string;
    availableFilters: ReportSkeletonFilter[];
    displayColumns:ReportSkeletonColumn[];
}