import { Injectable } from '@angular/core';
import { IReportsService } from './ireports-service';
import * as reportSkeletonData from '../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/report_skeleton.json';
import * as filterPositions from '../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/positions.json';
import { ReportSkeleton } from 'src/app/_DtoModels/ReportSkeleton/ReportSkeleton';
import { pascalToCamel } from 'src/app/_Shared/Utils/PascalToCamel';
import * as reportSummaryData from '../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/report_summary.json';
import * as reportData from '../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/reports.json';
import { Report } from 'src/app/_DtoModels/Report/Report';
import { ReportCreateOrUpdateOptions } from 'src/app/_DtoModels/Report/ReportCreateOrUpdateOptions';
import { ReportUpdateOptions } from 'src/app/_DtoModels/Report/ReportUpdateOptions';
import { ReportExportOptions, ReportExportType } from 'src/app/_DtoModels/Report/ReportExportOptions';
import { ReportSendOptions } from 'src/app/_DtoModels/Report/ReportSendOptions';

@Injectable({
    providedIn: 'root',
})
export class StubReportsService implements IReportsService {
    constructor() {}

    //reports services

    createReportAsync = (options: ReportCreateOrUpdateOptions) => {
        return new Promise((resolve, reject) => {
            setTimeout(() => {
                Promise.resolve();
            }, 500);
        });
    }

    updateReportAsync = (options: ReportUpdateOptions,reportId:number) => {
        return new Promise((resolve, reject) => {
            setTimeout(() => {
                Promise.resolve();
            }, 500);
        });
    }

    getReportFilterOptionsAsync = ( filterName: string) => {
        return new Promise<any>((resolve, reject) => {
            setTimeout(() => {
                resolve(pascalToCamel(filterPositions));
            }, 500);
        });
    }

    // report skeleton service

    getReportSkeletonsAsync = () => {
        return new Promise<ReportSkeleton>((resolve, reject) => {
            setTimeout(() => {
                resolve(pascalToCamel(reportSkeletonData));
            }, 500);
        });
    }

    getReportSkeletonAsync = (reportSkeletonId: number) => {
        return new Promise<ReportSkeleton>((resolve, reject) => {
            setTimeout(() => {
                const reportsData = pascalToCamel(reportSkeletonData);
                const reportsById = reportsData.filter(dd => { return dd.id == reportSkeletonId; });
                resolve(reportsById[0]);
                Promise.resolve();
            }, 500);
        });
    }

    generateReportAsync = (options: ReportCreateOrUpdateOptions) => {
        return new Promise((resolve, reject) => {
            setTimeout(() => {
                Promise.resolve();
            }, 500);
        });
    }

    getReportsAsync = () => {
        return new Promise<Report>((resolve, reject) => {
            setTimeout(() => {
                resolve(pascalToCamel(reportData));
            }, 500);
        });
    }

    getReportsByIdAsync = (reportId:number) => {
        return new Promise<Report>((resolve, reject) => {
            setTimeout(() => {
                const reportsData = pascalToCamel(reportData);
                const reportsById = reportsData.filter(dd =>{ return dd.id == reportId; });
                
                resolve(reportsById[0]);
            }, 500);
        });
    }

    deleteExistingReportAsync = (reportId:number) => {
        return new Promise((resolve, reject) => {
            setTimeout(() => {
                Promise.resolve();
            }, 500);
        });
    }

    generateReportByIdAsync = (options: ReportUpdateOptions,reportId:number) => {
        return new Promise((resolve, reject) => {
            setTimeout(() => {
                Promise.resolve();
            }, 500);
        });
    }

    exportReportAsync = (options: ReportExportOptions) => {
        return new Promise((resolve, reject) => {
            setTimeout(() => {
                Promise.resolve();
            }, 500);
        });
    }

    exportReportByIdAsync = (options: ReportExportOptions,reportId:number) => {
        return new Promise((resolve, reject) => {
            setTimeout(() => {
                Promise.resolve();
            }, 500);
        });
    }

    sendReportAsync = (sendReportOption:ReportSendOptions) => {
        return new Promise((resolve, reject) => {
            setTimeout(() => {
                Promise.resolve();
            }, 500);
        });
    }

    sendReportByIdAsync =(options:ReportSendOptions,reportId:number)=>{
        return new Promise((resolve, reject) => {
            setTimeout(() => {
                Promise.resolve();
            }, 500);
        });
    }


}
