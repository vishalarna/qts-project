import { ImportData_VM } from "@models/ImportCSV/ImportData_VM";
import { ImportDatum_DIFSurveyEmployeeResponse_VM } from "./ImportDatum_DIFSurveyEmployeeResponse_VM";

export class ImportData_DIFSurveyEmployeeResponse_VM extends ImportData_VM{
    difSurveyId:string;
    data: ImportDatum_DIFSurveyEmployeeResponse_VM[];
}