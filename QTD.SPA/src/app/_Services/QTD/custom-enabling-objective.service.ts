import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CustomEnablingObjectiveOption } from '@models/CustomEnablingObjective/CustomEnablingObjectiveOptions';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { CustomEnablingObjective } from 'src/app/_DtoModels/CustomEnablingObjective/CustomEnablingObjective';
import { CustomEnablingObjectiveCreateOption } from 'src/app/_DtoModels/CustomEnablingObjective/CustomEnablingObjectiveOption';
import { CustomEnablingObjectiveUpdateOption } from 'src/app/_DtoModels/CustomEnablingObjective/CustomEnablingObjectiveUpdateOption';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CustomEnablingObjectiveService {
  baseUrl = environment.QTD + 'customeo';

  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {
          return res['result'] as CustomEnablingObjective[];
        })
      )
      );
  }

  get(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as CustomEnablingObjective;
        })
      )
      );
  }

  create(options: CustomEnablingObjectiveCreateOption) {
    return firstValueFrom(this.http
      .post(this.baseUrl, options)
      .pipe(
        map((res: any) => {
          return res['result'] as CustomEnablingObjective;
        })
      )
      );
  }

  update(id: any, options: CustomEnablingObjectiveUpdateOption) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as CustomEnablingObjective;
        })
      )
      );
  }

  delete(id: any,options: CustomEnablingObjectiveOption) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}`, { body: options })
      .pipe(
        map((res: any) => {
          return res.message;
        })
      )
      );
  }
}
