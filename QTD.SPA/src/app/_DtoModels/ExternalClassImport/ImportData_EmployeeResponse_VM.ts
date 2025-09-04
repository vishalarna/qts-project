import { ImportData_VM } from "@models/ImportCSV/ImportData_VM";
import { ImportDatum_Employee_VM } from "./ImportDatum_Employee_VM";

export class ImportData_EmployeeResponse_VM extends ImportData_VM{
    data: ImportDatum_Employee_VM[];
}