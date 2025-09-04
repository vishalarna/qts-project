import { ValidateCSV_Results_VM } from "@models/ImportCSV/ValidateCSV_Results_VM";
import { ImportDatum_Class_VM } from "./ImportDatum_Class_VM";

export class ValidateCSV_Class_Results_VM extends ValidateCSV_Results_VM{
    data: ImportDatum_Class_VM[];
  }