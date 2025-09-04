
export class ReportFilter {
    reportId: number;
    reportSkeletonId: number;
    propertyType: filterPropertyTypeEnum;
    valueType: filterValueTypeEnum;
    value: string;
    name:string;
}
enum filterValueTypeEnum {
    Single,
    Array,
    Range
}

enum filterPropertyTypeEnum {
    Date,
    Int,
    Boolean
}