import { ReportFilterOption } from "./ReportFilterOption";

export class ReportUpdateOptions{
    reportSkeletonId:number;
    internalReportTitle:string;
    filters: Array<ReportFilterOption>;
    displayColumns:string[];

    getInternalReportTitle(title: string){
        this.internalReportTitle = title;
    }

    getFilters(filter: ReportFilterOption){
        if(!this.filters) this.filters = [];
        const selectFilter = this.filters.filter(dd => dd.name === filter.name);
        if(selectFilter.length){
            selectFilter[0].value = filter.value; 
        } else {
            this.filters.push(filter);
        }
    }


    getDisplayColumns(columnName: string){
        if(!this.displayColumns) this.displayColumns = [];
        const selectColumn = this.displayColumns.filter(dd => dd === columnName);
        if(!selectColumn.length){
            this.displayColumns.push(columnName);
        }
    }
}