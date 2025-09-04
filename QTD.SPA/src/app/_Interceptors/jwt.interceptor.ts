import {Injectable} from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {jwtAuthHelper} from '../_Shared/Utils/jwtauth.helper';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor() {
  }

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {

    if (req.url === './assets/i18n/en.json') return next.handle(req);

    const jwtToken = jwtAuthHelper.ValidAuthToken;

    if(req.body instanceof FormData)
    {
      var request = req.clone({
        headers: new HttpHeaders({
          Authorization: `Bearer ${jwtToken}`,
          'Access-Control-Allow-Origin': '*',
        }),
      });
      return next.handle(request);
    }
    else
    {
      var request = req.clone({
        headers: new HttpHeaders({
          Authorization: `Bearer ${jwtToken}`,
          'Content-Type': 'application/json',
          'Access-Control-Allow-Origin': '*',
        }),
      });
      return next.handle(request);
    }

  }
}
