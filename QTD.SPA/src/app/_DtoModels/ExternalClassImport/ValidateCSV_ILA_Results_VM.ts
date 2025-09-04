import { ValidateCSV_Results_VM } from "@models/ImportCSV/ValidateCSV_Results_VM";
import { ImportDatum_ILA_VM } from "./ImportDatum_ILA_VM";

export class ValidateCSV_ILA_Results_VM extends ValidateCSV_Results_VM{
    data: ImportDatum_ILA_VM[];
  }