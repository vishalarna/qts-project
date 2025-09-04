import { ValidateCSV_Results_VM } from "@models/ImportCSV/ValidateCSV_Results_VM";
import { ImportDatum_Employee_VM } from "./ImportDatum_Employee_VM";

export class ValidateCSV_Employee_Results_VM extends ValidateCSV_Results_VM{
    data: ImportDatum_Employee_VM[];
  }