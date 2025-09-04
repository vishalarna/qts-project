import { ImportDatum_VM } from "@models/ImportCSV/ImportDatum_VM";
import { ValidationError_VM } from "@models/ImportCSV/ValidationError_VM";

export class ImportDatum_DIFSurveyEmployeeResponse_VM extends ImportDatum_VM{
    employeeNumber: string;
    taskNumber: string;
    difficulty: string;
    importance: string;
    frequency: string;
    na: string;
    comments: string;
    validationErrors: ValidationError_VM[];
}