import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanActivateChild,
  CanLoad,
  Route,
  Router,
  RouterStateSnapshot,
  UrlSegment,
} from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AppComponent } from '../app.component';
import { jwtAuthHelper } from '../_Shared/Utils/jwtauth.helper';
import { SweetAlertService } from '../_Shared/services/sweetalert.service';
import { AuthService } from '../_Services/Auth/auth.service';
import { DataBroadcastService } from '../_Shared/services/DataBroadcast.service';
import { Store } from '@ngrx/store';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanLoad, CanActivate, CanActivateChild {
  isLoggedIn: Observable<boolean>;

  constructor(
    private _alertService: SweetAlertService,
    private _router: Router,
    private translate: TranslateService,
    private _databroadcastSrvc: DataBroadcastService,
    private authService: AuthService,
    private store: Store<{ data: boolean }>
  ) {
    this.isLoggedIn = this.store.select('data');
  }

  private async checkAccess(url: string): Promise<boolean> {
    
    const loggedIn = this._databroadcastSrvc.UserLoggedIn();
    const tfaRequired = jwtAuthHelper.HasTfaRequired === 'true';

    if (loggedIn && !tfaRequired) {
      return true;
    }
    if (loggedIn && tfaRequired) {
      if (url.toLowerCase().includes('2fa')) {
        return true;
      }
        await this._router.navigate(['/auth/2fa'], { queryParams: { returnUrl: url }});
      return false;
    }
    this._alertService.errorToast('Unauthorized');
    jwtAuthHelper.removeAuthToken();
    await this._router.navigate(['/auth/login'], { queryParams: { returnUrl: url } });
    
    return false;
  }

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
    return this.checkAccess(state.url);
  }

  async canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
    return this.checkAccess(state.url);
  }

  async canLoad(route: Route, segments: UrlSegment[]): Promise<boolean> {

    const url = segments.map(s => s.path).join('/');
    return this.checkAccess(url);
  }
}
