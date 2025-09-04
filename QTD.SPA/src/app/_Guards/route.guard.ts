import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanLoad,
  Route,
  Router,
  RouterStateSnapshot,
  UrlSegment,
  UrlTree,
} from '@angular/router';
import { Observable } from 'rxjs';
import { DataBroadcastService } from '../_Shared/services/DataBroadcast.service';
import { SweetAlertService } from '../_Shared/services/sweetalert.service';
import { CustomClaimTypes } from '../_Shared/Utils/CustomClaims';
import { jwtAuthHelper } from '../_Shared/Utils/jwtauth.helper';
import { LicenseHelperService } from '../_Shared/services/licenseHelper.service';
import { DatePipe } from '@angular/common';
import { FeatureService } from '../_Shared/services/featureService.service';
 
@Injectable({
  providedIn: 'root',
})
export class RouteGuard implements CanActivate, CanLoad {
  /**
   *
   */
  datePipe = new DatePipe('en-us');
 
  constructor(
    private alert: SweetAlertService,
    private route: Router,
    private databroadcastSrvc: DataBroadcastService,
    private licenseHelperService: LicenseHelperService,
    private featureService: FeatureService
  ) {}
 
  canLoad(route: Route, segments: UrlSegment[]): boolean | Promise<boolean> {
    let url = segments.toString().replace(',', '/');
    var isAdmin = jwtAuthHelper.IsAdminUser;
    var hasMultipleInstances = jwtAuthHelper.HasMultipleInstances;
    var isQtdUser = jwtAuthHelper.IsQtdUser;
    var isEmployee = jwtAuthHelper.IsEmployee;
    const tfaRequired = jwtAuthHelper.HasTfaRequired === 'true';
    if (url.startsWith('auth')) {
      return this.databroadcastSrvc.UserLoggedIn() && !tfaRequired
        ? this.route.navigate(['/home']).then((_) => {
            return false;
          })
        : true;
    }
    if((!isAdmin || isAdmin=="false")&& url.startsWith('admin')){
      return this.databroadcastSrvc.UserLoggedIn()
        ? this.route.navigate(['/home/index']).then((_) => {
            return false;
          })
        : true;
    }
    if(hasMultipleInstances=="False" && (url.startsWith('home/index') || url.startsWith('home/instance-selection'))){
      if(isQtdUser=="True"){
      return this.databroadcastSrvc.UserLoggedIn()
        ? this.route.navigate(['/home/dashboard']).then((_) => {
            return false;
          })
        : true;
      }else if(isEmployee=="True"){
        return this.databroadcastSrvc.UserLoggedIn()
        ? this.route.navigate(['/emp/dashboard']).then((_) => {
            return false;
          })
        : true;
      }
    }
    if (isEmployee =='True' && !this.employeeAllowableRoutes(url) && (!isAdmin || isAdmin=="False") && isQtdUser=="False") {
      this.alert.warningAlert('Unauthorized Access', 'You are not authorized to access this link');
      this.route.navigate(['/emp/dashboard']);
      return false;
    }
    return true;
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    let jwt = jwtAuthHelper.unPackJWTToken;
    if (!jwt) {
      this.alert
        .errorAlertRedirect(
          'Unauthorized',
          'Your session has expired. Please log in again.'
        )
        .then((_) => {
          jwtAuthHelper.removeAuthToken();
          this.route.navigate(['auth/login']);
        });
      return false;
    } else {
      this.featureService.getFeaturesData(jwtAuthHelper.SelectedInstance);
      let client = jwt[CustomClaimTypes.InstanceName];
      if (!client) {
        this.databroadcastSrvc.selectInstance.next(undefined);
 
        this.alert.warningAlert(
          'No Instance Selected !',
          'Please Select instace from list .'
        );
        this.route.navigate(['/home/instance-selection'], {
          queryParams: { returnUrl: state.url },
        });
        return false;
      } else {
        this.checkLicenseAndRoute(client).then((x) => {});
        return true;
      }
    }
  }
 
  async checkLicenseAndRoute(client) {
    var licenseSettingData = await this.licenseHelperService.setLicenseData();
    if (licenseSettingData != null) {
      var licenseActiveStatus = licenseSettingData.active;
      var licenseexpiration = this.datePipe.transform(
        licenseSettingData.expiration,
        'yyyy-MM-dd'
      );
      var todaysDate = this.datePipe.transform(Date.now(), 'yyyy-MM-dd');
      if (licenseActiveStatus === true && licenseexpiration > todaysDate) {
        this.databroadcastSrvc.selectInstance.next(client);
      } else {
        if (jwtAuthHelper.IsQtdUser == 'True') {
          this.route.navigate(['/data-exchange/database']);
        } else if (jwtAuthHelper.IsEmployee == 'True') {
          this.route.navigate(['/expired-license']);
        }
      }
    }
  }
 
  private employeeAllowableRoutes(url:string):boolean {
    return (url.startsWith('emp') || url.includes('implementation/test'));
  }
}
 
 