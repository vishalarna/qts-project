import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { throwError } from 'rxjs/internal/observable/throwError';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ErrorLogging{
  constructor(private http:HttpClient) { }
  baseUrl = environment.QTD + 'Error';
  setError(error:any){
    return firstValueFrom(this.http.post(this.baseUrl,{"errorInfo":error.stack}).pipe(map((data)=>{
      return data
    })));
  }

}
