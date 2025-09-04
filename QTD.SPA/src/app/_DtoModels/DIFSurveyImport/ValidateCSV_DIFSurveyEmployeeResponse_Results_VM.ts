import { ValidateCSV_Results_VM } from "@models/ImportCSV/ValidateCSV_Results_VM";
import { ImportDatum_DIFSurveyEmployeeResponse_VM } from "./ImportDatum_DIFSurveyEmployeeResponse_VM";

export class ValidateCSV_DIFSurveyEmployeeResponse_Results_VM extends ValidateCSV_Results_VM{
  data: ImportDatum_DIFSurveyEmployeeResponse_VM[];
}