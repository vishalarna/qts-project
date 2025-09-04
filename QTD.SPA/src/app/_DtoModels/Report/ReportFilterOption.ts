import { ReportFilterOptionParent } from "./ReportFilterOptionParent";
import { ReportFilterTableColumns } from "./ReportFilterTableColumns";

export class ReportFilterOption{
    id:number;
    value:string;
    name:string;
    active? :boolean
    filterOptionParents: Array<ReportFilterOptionParent>;
    filterTableColumns: Array<ReportFilterTableColumns>;
    constructor(filterName: string, filterValue: string ){
        this.name = filterName;
        this.value = filterValue;
     }
}