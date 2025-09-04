import { ImportData_VM } from "@models/ImportCSV/ImportData_VM";
import { ImportDatum_ILA_VM } from "./ImportDatum_ILA_VM";

export class ImportData_ILAResponse_VM extends ImportData_VM{
    data: ImportDatum_ILA_VM[];
}