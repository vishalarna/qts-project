import { Injectable, Injector } from '@angular/core';
import {
  HttpInterceptor,
  HttpEvent,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse,
  HTTP_INTERCEPTORS,
  HttpEventType,
} from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { catchError, finalize, switchMap } from 'rxjs/operators';
import { jwtAuthHelper } from '../_Shared/Utils/jwtauth.helper';
import { AuthService } from '../_Services/Auth/auth.service';
import { Router } from '@angular/router';
import { SweetAlertService } from '../_Shared/services/sweetalert.service';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root',
})
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private authService: AuthService,
    private route: Router,
    private alert: SweetAlertService
  ) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error) => {
        if (error instanceof HttpErrorResponse) {
          const appException = error.headers.get('Application-Error');

          // if (appException) {
          //   console.error(appException);
          //   return throwError('ServerErr');
          // }
          if(error.status == 409){
            return throwError(error);
          }
          if(error.status == 404){
            return throwError(error);
          }
          if (error.status == 0) {
            this.alert.errorAlert('Server Not Responding');
            return throwError(error);
          }

          if (error.status === 401) {
            return this.handle401Error(req, next, error);
          }

          if (error.status === 400){
            this.alert.errorAlert(error.error?.message?.name ?? error.error);
            return throwError(error);
          }

          if (error.status === 500 && error.error === 'InvalidRefreshJWT') {
            jwtAuthHelper.removeAuthToken();
            this.alert
              .errorAlertRedirect(
                'Idle Timeout',
                'Credentials expired! Please log in again'
              )
              .then((_) => {
                setTimeout(() => {
                  this.route.navigate(['auth/login'], {
                    queryParams: { returnUrl: req.url },
                  });
                }, 3000);

                return throwError('InvalidRefreshJWT');
              });
          }

          this.alert.errorAlert(JSON.stringify(appException));
          return throwError(JSON.stringify(appException));
        } else {
          let errorMsg = `Client-Side Error: ${error.error.message}`;
          this.alert.errorAlert(errorMsg);
          return throwError(errorMsg);
        }
      })
    );
  }

  handle401Error(req: HttpRequest<any>, next: HttpHandler, error: HttpErrorResponse): Observable<HttpEvent<any>> {
    let jwtToken = jwtAuthHelper.ValidAuthToken;
    let refreshoken = jwtAuthHelper.ValidRefreshToken;

    if (jwtToken && refreshoken && refreshoken !== '' && jwtToken !== '')
      return this.authService.refreshToken(jwtToken, refreshoken).pipe(
        switchMap((_) => {
          return next.handle(req);
        })
      );
    else {
      jwtAuthHelper.removeAuthToken();
      return throwError(error);
    }
  }
}

export const ErrorInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptor,
  multi: true,
};
