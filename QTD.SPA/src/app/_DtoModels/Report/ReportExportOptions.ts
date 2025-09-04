import { ReportCreateOrUpdateOptions } from "./ReportCreateOrUpdateOptions";

export class ReportExportOptions {
    exportType: ReportExportType;
    options: ReportCreateOrUpdateOptions;

    getExportType(dataexport: ReportExportType) {
        this.exportType = dataexport;
    }

    getCreateOrUpdateOption(option: ReportCreateOrUpdateOptions){
        this.options = option;
    }
}

export enum ReportExportType {
    Unk,
    Excel,
    Pdf
}