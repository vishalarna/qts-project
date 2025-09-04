import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { TrainingProgram } from 'src/app/_DtoModels/TrainingProgram/TrainingProgram';
import { TrainingProgramCreateOptions } from 'src/app/_DtoModels/TrainingProgram/TrainingProgramCreateOptions';
import { TrainingProgramUpdateOptions } from 'src/app/_DtoModels/TrainingProgram/TrainingProgramUpdateOptions';
import { TrainingProgramFilterOptions } from 'src/app/_DtoModels/TrainingProgram/TrainingProgramFilterOptions';
import { ILAWithCountOptions } from 'src/app/_DtoModels/ILA/ILAWithCountOptions';
import { TrainingProgram_ILA_LinkCreateOptions } from 'src/app/_DtoModels/TrainingProgram/TrainingProgram_ILA_LinkCreateOptions';
import { TrainingProgram_HistoryCreateOptions } from 'src/app/_DtoModels/TrainingProgram/TrainingProgram_HistoryCreateOptions';
import { TrainingProgram_VersionTitleViewModel } from 'src/app/_DtoModels/TrainingProgram/TrainingProgram_VersionTitleViewModel';
import { firstValueFrom } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class TrainingProgramsService {
  baseUrl = environment.QTD + 'trainingPrograms';
  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {

          return res['trainingPrograms'] as TrainingProgram[];
        })
      )
      );
  }

  getVersionForPositionId(posId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/position/${posId}`).pipe(
      map((res:any)=>{
        return res['result'] as TrainingProgram[];
      })
    ));
  }

  getInitialVersionsForPositionIdAsync(posId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/position/${posId}/Initial Training Program`).pipe(
      map((res:any)=>{
        return res['result'] as TrainingProgram[];
      })
    ));
  }

  create(options: TrainingProgramCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  get(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {

          return res['trainingProgram'] as TrainingProgram;
        })
      )
      );
  }
  update(id: any, options: TrainingProgramCreateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}`, options)
      .pipe(
        map((res: any) => {

          return res.message;
        })
      )
      );
  }

  delete(id: any) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {

          return res.message;
        })
      )
      );
  }
  getStatsCount()
  {

    return firstValueFrom(this.http
      .get(this.baseUrl + '/stats')
      .pipe(
        map((res: any) => {
          return res['stats'] as any;
        })
      ));
  }
  getTrainingProgramByFilter(filter:string,options:TrainingProgramFilterOptions)
  {

    return firstValueFrom(this.http
      .post(this.baseUrl + `/${filter}`,options)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      ));
  }
  linkILA(id: any, options: TrainingProgram_ILA_LinkCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/ila`, options)
      .pipe(
        map((res: any) => {
          return res['result'];
        })
      )
      );
  }

  unlinkILA(id: any, options: TrainingProgram_ILA_LinkCreateOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/ila`, { body: options })
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  getLinkedILAWithCount(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/ila`)
      .pipe(
        map((res: any) => {
          return res['result'] as ILAWithCountOptions[];
        })
      )
      );
  }
  makeActiveInactiveOrDelete(id:any,options:TrainingProgram_HistoryCreateOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}`,{body:options}).pipe(
      map((res:any)=>{
        return res;
      })
    ));
  }
  publishTrainingProgram(id: any, options: TrainingProgram_HistoryCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/publish`,options)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  getNotLinkedWith(name : string,status:any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/notlinked/${name}/${status}`)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }

  GetActiveVersionTitleByPositionAndTrainingProgramTypeAsync(positionId : string,trainingProgramTypeId:string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/active/versionAndTitle/position/${positionId}/trainingProgramType/${trainingProgramTypeId}`)
      .pipe(
        map((res: any) => {
          return res['result'] as TrainingProgram_VersionTitleViewModel[];
        })
      )
      );
  }

  getTrainingProgramsWithNoReviews(){
    return firstValueFrom(this.http.get(this.baseUrl + '/active/versionAndTitle/noReview')
    .pipe(
      map((res:any) => {
        return res['result'] as TrainingProgram_VersionTitleViewModel[];
      })
    ));
  }

  getTrainingProgramIlaLinksAndReviewsAsync() {
    return firstValueFrom(this.http.get(this.baseUrl + `/links`).pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }
  
}
