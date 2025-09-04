import { ImportData_VM } from "@models/ImportCSV/ImportData_VM";
import { ImportDatum_Class_VM } from "./ImportDatum_Class_VM";

export class ImportData_ClassResponse_VM extends ImportData_VM{
    data: ImportDatum_Class_VM[];
}