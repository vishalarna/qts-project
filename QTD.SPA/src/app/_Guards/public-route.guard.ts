import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { InstanceService } from '../_Services/Auth/instance.service';

@Injectable({
  providedIn: 'root'
})
export class PublicRouteGuard implements CanActivate {
  
  constructor(private instanceService: InstanceService,
              private router: Router,
  ) { }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree{
    const instance = route.paramMap.get('instanceName');
    const publicUrl = route.paramMap.get('publicUrl').toString();
  if (!instance || !publicUrl) {    
    this.router.navigate(['/error/404']);
    return false;
  }  
    return this.isPublicUrlAvailable(instance, publicUrl)    
  }
  
  async isPublicUrlAvailable(instance: string, incomingUrl: string): Promise<boolean> {   
  try {
    const response = await this.instanceService.getPublicURLInstanceSettingsAsync(instance);
    if (response == incomingUrl) {
      return true;
    } else {
      this.router.navigate(['/error/404']);
      return false;
    }
  } catch (error) {
    this.router.navigate(['/error/404']);
    return false;
  }
}
}
