import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { TrainingProgramType } from 'src/app/_DtoModels/TrainingProgramType/TrainingProgramType';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TrainingProgramTypeService {

  baseUrl = environment.QTD + 'trainingPrograms';
  constructor(private http: HttpClient) {}
  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl+'/trainingProgramTypes')
      .pipe(
        map((res: any) => {
          return res['trainingProgramsTypes'] as TrainingProgramType[];
        })
      ));
  }
}
