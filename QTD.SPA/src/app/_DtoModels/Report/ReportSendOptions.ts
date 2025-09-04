import { ReportCreateOrUpdateOptions} from "./ReportCreateOrUpdateOptions";
import { ReportExportType } from "./ReportExportOptions";

export class ReportSendOptions {
    createOrUpdateOptions: ReportCreateOrUpdateOptions;
    exportType: ReportExportType;
    tos: string[];

    getExportType(dataexport: ReportExportType) {
            this.exportType = dataexport;
    }

    getCreateOrUpdateOption(option: ReportCreateOrUpdateOptions){
        this.createOrUpdateOptions = option;
    }

    getTos(name: string) {
        if (!this.tos) this.tos = [];
        const selectName = this.tos.filter(dd => dd === name);
        if (!selectName.length) {
            this.tos.push(name);
        }
    }

}

