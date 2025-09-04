import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { ReportSkeletonCategories } from 'src/app/_DtoModels/ReportSkeleton_Category/ReportSkeletonCategories';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ReportSkeletonCategoriesService {
  baseUrl = environment.QTD + 'reportSkeletonCategories';
  constructor(private http: HttpClient) {}

  getActiveReportSkeletonCategoriesAsync() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {
          return res['locList'] as ReportSkeletonCategories[];
        })
      )
      );
  }

  getReportSkeletonCategoryByIdAsync(reportSkeletonCategoryId:string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${reportSkeletonCategoryId}`)
      .pipe(
        map((res: any) => {
          return res['locList'] as ReportSkeletonCategories;
        })
      )
      );
  }
}
