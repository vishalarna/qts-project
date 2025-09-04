export class ReportSkeletonFilter {
    reportSkeletonId: number;
    display: string;
    propertyType: filterPropertyTypeEnum;
    valueType: filterValueTypeEnum;
    value: string;
    defaultValue: string;
    minOption: Date;
    maxOption: Date;
    filterOption: string;
    name: string;
    maxAllowedSelections?:number;
}

export enum filterValueTypeEnum {
    Single,
    Array,
    Range
}
export enum filterPropertyTypeEnum {
    Date,
    Int,
    Boolean,
    String
}