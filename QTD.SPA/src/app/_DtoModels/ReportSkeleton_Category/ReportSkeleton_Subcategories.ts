import { ReportSkeletonCategories } from "./ReportSkeletonCategories";
import { ReportSkeleton_Subcategory_Reports } from "./ReportSkeleton_Subcategory_Reports";

export class ReportSkeleton_Subcategories {
    id: string;
    name: string;
    reportSkeleton_CategoryId: string;
    reportSkeleton_Category: ReportSkeletonCategories;
    reportSkeleton_Subcategory_Reports: ReportSkeleton_Subcategory_Reports[];
    order:number;
}