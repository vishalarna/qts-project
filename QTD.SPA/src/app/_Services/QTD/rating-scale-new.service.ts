import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RatingScaleNewService {
  baseUrl = environment.QTD + 'ratingscalen';
  constructor(private http: HttpClient) {}
  getAll() {
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res: any) => {

        return res['ratingScales'] as any[];
      })
    ));
  }
}
