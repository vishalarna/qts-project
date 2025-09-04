import { ImportDatum_VM } from "@models/ImportCSV/ImportDatum_VM";

export class ImportDatum_Employee_VM extends ImportDatum_VM {
    lastName: string;
    firstName: string;
    middle: string;
    empNum: string;
    email: string;
    phone: string;
    certName: string;
    certNum: string;
    issueDate: string;
    recertDate: string;
    certExpDate: string;
    positionNum: string;
    positionStartDate:string;
    posAbbrev: string;
    organizationName: string;
}