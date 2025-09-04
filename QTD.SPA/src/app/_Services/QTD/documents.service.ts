import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { firstValueFrom } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class DocumentsService {
  baseUrl = environment.QTD + 'documents';
  constructor(private http: HttpClient) {}

  get(name: string) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${name}`).pipe(
      map((res: any) => {
        
        return res[''] as any;
      })
    ));
  }
}
