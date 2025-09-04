import { ImportDatum_VM } from "@models/ImportCSV/ImportDatum_VM";

export class ImportDatum_ILA_VM extends ImportDatum_VM{
    ilaName: string;
    ilaNum: string;
    ilaDesc: string;
    selfPaced: string;
    totalHours: string;
    nercName: string;
    effectiveDate: string;
    nercIsIncludeSimulation: string;
    nercEmergencyOperatingTraining: string;
    nercIsPartialCred: string;
    nercTotalCEH: string;
    nercStandards: string;
    nercSimulation: string;
    reg: string;
    reg2: string;
}