import { ReportSkeleton } from "../ReportSkeleton/ReportSkeleton";
import { ReportSkeleton_Subcategories } from "./ReportSkeleton_Subcategories";

export class ReportSkeleton_Subcategory_Reports {
    id: string;
    reportSkeleton_SubcategoryId: string;
    reportSkeletonId: string;
    reportSkeleton: ReportSkeleton;
    reportSkeleton_Subcategories: ReportSkeleton_Subcategories[];
    order:number;
}