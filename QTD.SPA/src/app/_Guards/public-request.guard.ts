import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { FeatureService } from '../_Shared/services/featureService.service';

@Injectable({
  providedIn: 'root'
})
export class PublicRequestGuard implements CanActivate {

  constructor(private featureService: FeatureService,
              private router: Router  )  { }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      const instance = route.paramMap.get('instanceName');

      this.checkPublicClassEnabled(instance)
    return true;
  }

  async checkPublicClassEnabled(instance: any){
    const publicClass = await this.featureService.getFeaturesData(instance);
    if(!publicClass?.enabled){
      this.router.navigate(['/error/404']);
    }   
  }
  
}
