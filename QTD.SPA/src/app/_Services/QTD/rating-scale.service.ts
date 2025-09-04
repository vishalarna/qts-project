import { RatingScaleCreateOptions } from './../../_DtoModels/RatingScale/RatingScaleCreateOptions';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { RatingScale } from 'src/app/_DtoModels/RatingScale/RatingScale';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RatingScaleService {
  baseUrl = environment.QTD + 'ratingscale';
  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res:any)=>{
        return res['ratingScales'] as RatingScale[];
      })
    ));
  }

  create(options: RatingScaleCreateOptions) {
    return firstValueFrom(this.http.post(this.baseUrl, options).pipe(
      map((res: any) => {

        return res
      })
    ));
  }
}
