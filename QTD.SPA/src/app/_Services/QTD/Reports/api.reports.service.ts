import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { IReportsService } from './ireports-service';
import { ReportCreateOrUpdateOptions } from 'src/app/_DtoModels/Report/ReportCreateOrUpdateOptions';
import { ReportUpdateOptions } from 'src/app/_DtoModels/Report/ReportUpdateOptions';
import { ReportExportOptions, ReportExportType } from 'src/app/_DtoModels/Report/ReportExportOptions';
import { ReportSendOptions } from 'src/app/_DtoModels/Report/ReportSendOptions';
import { ReportFilterOption } from 'src/app/_DtoModels/Report/ReportFilterOption';
import { firstValueFrom, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})

export class ApiReportsService implements IReportsService {
  baseUrl = environment.QTD;
  constructor(private http: HttpClient) {
  }

  getReportSkeletonsAsync = () => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `reportSkeletons`)
      .pipe(
        map((res: any) => {
          return res.locList;
        })
      )
      );
  }

  createReportAsync = (options: ReportCreateOrUpdateOptions) => {
    return firstValueFrom(this.http.post<any>(this.baseUrl + `reports`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      ));
  }

  updateReportAsync = (options: ReportUpdateOptions, reportId: number) => {
    return firstValueFrom(this.http.put<any>(this.baseUrl + `reports/` + reportId, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      ));
  }

  getReportFilterOptionsAsync = (filterName: string) => {
    
    return firstValueFrom(this.http
      .get(this.baseUrl + `reports/options/` + filterName)
      .pipe(
        map((res: any) => {
          return res.locList;
        })
      )
      );
  }

  getReportSkeletonAsync = (reportSkeletonId: number) => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `reportSkeletons/` + reportSkeletonId)
      .pipe(
        map((res: any) => {
          return res.locList;
        })
      )
      );
  }

  getReportsAsync = () => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `reports`)
      .pipe(
        map((res: any) => {
          return res.locList;
        })
      )
      );
  }

  getReportsByIdAsync = (reportId: number) => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `reports/` + reportId)
      .pipe(
        map((res: any) => {
          return res.locList;
        })
      )
      );
  }

  deleteExistingReportAsync = (reportId: number) => {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `reports/` + reportId)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  generateReportAsync = (options: ReportCreateOrUpdateOptions) => {
    return firstValueFrom(this.http
      .post(this.baseUrl + 'reports/generate', options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  generateReportByIdAsync = (options: ReportUpdateOptions, reportId: number) => {
    return firstValueFrom(this.http
      .post(this.baseUrl + 'reports/generate/' + reportId, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  exportReportAsync = (options: ReportExportOptions) => {
    return firstValueFrom(this.http
      .post(this.baseUrl + 'reports/export', options, { observe: 'response', responseType: 'blob' })
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  exportReportByIdAsync = (options: ReportExportOptions, reportId: number) => {
    return firstValueFrom(this.http
      .post(this.baseUrl + 'reports/export/' + reportId, options, { observe: 'response', responseType: 'blob' })
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }
  
  sendReportAsync = (sendReportOption: ReportSendOptions) => {
    return firstValueFrom(this.http
      .post(this.baseUrl + 'reports/send', sendReportOption)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  sendReportByIdAsync = (options: ReportSendOptions, reportId: number) => {
    return firstValueFrom(this.http
      .post(this.baseUrl + 'reports/send/' + reportId, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  getReportSkeletonByNameAsync = (name:string) => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `reportSkeletons/name/${name}`)
      .pipe(
        map((res: any) => {
          return res.locList;
        })
      )
      );
  }
}