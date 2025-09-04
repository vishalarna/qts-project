import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { AssesmentTool } from 'src/app/_DtoModels/NERCTargetAudience/AssesmentTool';
import { NERCTargetAudience } from 'src/app/_DtoModels/NERCTargetAudience/NERCTargetAudience';
import { TrainingTopics } from 'src/app/_DtoModels/NERCTargetAudience/TrainingTopics';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NercTargetAudienceService {

  constructor(
    private http: HttpClient
  ) { }

  baseUrl = environment.QTD;

  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl+'nercTargetAudience')
      .pipe(
        map((res: any) => {

          return res['result'] as NERCTargetAudience[];
        })
      )
      );
  }

  getAllTrainingTopics() {
    return firstValueFrom(this.http
      .get(this.baseUrl+'trainingTopicsCategories')
      .pipe(
        map((res: any) => {

          return res['result'] as TrainingTopics[];
        })
      )
      );
  }

  getAllAssesmentTools() {
    return firstValueFrom(this.http
      .get(this.baseUrl+'assessmentTool')
      .pipe(
        map((res: any) => {

          return res['result'] as AssesmentTool[];
        })
      )
      );
  }
}
