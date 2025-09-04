import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { ErrorHandler, Injectable, NgZone } from '@angular/core';
import { map } from 'rxjs/operators';
import { ErrorLogging } from '../_Services/QTD/error-logging.service';
import { SweetAlertService } from '../_Shared/services/sweetalert.service';


@Injectable({
  providedIn:'root',
})
export class GlobalErrorHandler implements ErrorHandler {
  constructor(private log:ErrorLogging,
              private http:HttpClient,
              private alert:SweetAlertService) {}

  handleError(error: any) {
    // Check if it's an error from an HTTP response
    if (!(error instanceof HttpErrorResponse)) {
      this.alert.errorAlert(error.message)
      this.log.setError(error).then((res)=>{
        
      }).catch((res)=>{
        // Logic to handle error in case the promise fails
        var data:string[] =[]
        var pendingErrors = localStorage.getItem('error')
        data = pendingErrors?pendingErrors.split(','):[];
        data.push(error.stack? error.stack:"");
        localStorage.setItem('error',data.toString())
        
      })
      console.error(error.stack)
    }
    console.error(error)
  }
}
