import { HttpClient } from "@angular/common/http";
import { ApiReportsService } from "./api.reports.service";
import { StubReportsService } from "./stub.reports";
import { environment } from 'src/environments/environment';
import { ReportCreateOrUpdateOptions } from "src/app/_DtoModels/Report/ReportCreateOrUpdateOptions";
import { ReportUpdateOptions } from "src/app/_DtoModels/Report/ReportUpdateOptions";
import { ReportExportOptions, ReportExportType } from "src/app/_DtoModels/Report/ReportExportOptions";
import { ReportSendOptions } from "src/app/_DtoModels/Report/ReportSendOptions";

export interface IReportsService {
  getReportSkeletonsAsync: () => Promise<any>;
  createReportAsync: (options: ReportCreateOrUpdateOptions) => Promise<any>;
  updateReportAsync: (options: ReportUpdateOptions, reportId: number) => Promise<any>;
  getReportFilterOptionsAsync: (filterName: string) => Promise<any>;
  getReportSkeletonAsync: (reportSkeletonId: number) => Promise<any>;
  getReportsAsync: () => Promise<any>;
  getReportsByIdAsync: (reportId: number) => Promise<any>;
  deleteExistingReportAsync: (reportId: number) => Promise<any>;
  generateReportAsync: (options: ReportCreateOrUpdateOptions) => Promise<any>;
  generateReportByIdAsync: (options: ReportUpdateOptions, reportId: number) => Promise<any>;
  exportReportAsync: (options: ReportExportOptions) => Promise<any>;
  exportReportByIdAsync: (options: ReportExportOptions, reportId: number)=>void;
  sendReportAsync: (sendReportOption:ReportSendOptions) => Promise<any>;
  sendReportByIdAsync: (options: ReportSendOptions, reportId: number) => Promise<any>;
}

function reportsServiceFactory(http: HttpClient) {
  //here you can either inject params in to determine whic service to use OR detect an env var
  
  if (environment.Storybook_UseStub) {
    return new StubReportsService();
  }
  else {
    return new ApiReportsService(http);
  }
}

export const reportsServiceProvider = {
  provide: ApiReportsService,
  useFactory: reportsServiceFactory,
  deps: [HttpClient]
};
