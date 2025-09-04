import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import {BehaviorSubject, firstValueFrom, interval, Observable} from 'rxjs';
import {map, mergeMap, switchMap} from 'rxjs/operators';
import {RefreshTokenModel} from 'src/app/_DtoModels/Auth/RefreshTokenModel';
import {environment} from 'src/environments/environment';
import {jwtAuthHelper} from '../../_Shared/Utils/jwtauth.helper';
import {SweetAlertService} from '../../_Shared/services/sweetalert.service';
import {AuthenticationViewModel} from '../../_DtoModels/Auth/AuthenticateViewModel';
import {CreatePasswordViewModel} from '../../_DtoModels/Auth/CreatePasswordViewModel';
import {LoginModel} from '../../_DtoModels/Auth/login.dto';
import {ModifyTokenModel} from 'src/app/_DtoModels/Auth/ModifyTokenModel';
import {DataBroadcastService} from 'src/app/_Shared/services/DataBroadcast.service';
import { IdentityProviderVM } from '@models/IdentityProvider/IdentityProviderVM';
import { IdentityProviderUpdateModel } from '@models/IdentityProvider/IdentityProviderUpdateModel';

/*this keyword is used to mark the class is enabled for dependency
 injection and provided in property defines the scope of instance of the injected class.
 FYI https://stackoverflow.com/questions/45146960/what-is-the-scope-of-an-angular-module-import
 It'll clarify how components and service/providers maintains their scope in angular 2+
 */
@Injectable({providedIn: 'root'})

/* This is an abstract class with some helper functions to validate, decode, store, get and check the expiration of JWT.
Wondering why not jwtAuthHelper as a static class? Typescript does not have static keyword for classes. You can make them abstract or module.
FYI : https://stackoverflow.com/questions/13212521/typescript-static-classes
Not to be confused with Angular Modules and typescript module, see this https://www.javatpoint.com/typescript-module
*/
export class AuthService extends jwtAuthHelper {
    baseUrl = environment.APIAuth;

    constructor(
        private http: HttpClient,
        private route: Router,
        private dataservice: DataBroadcastService
    ) {
        super(dataservice);

    }

    loginJwt(dto: LoginModel) {
        return firstValueFrom(this.http
            .post(this.baseUrl + 'users/authenticate', dto, {withCredentials: true})
            .pipe(
                map((res: any) => {
                    if (res.authToken && res.refreshToken) {

                        this.setAuthToken(res.authToken, res.refreshToken);
                    }
                    return res.message;
                })
            ));
    }

    loginSSO(dto: LoginModel) {
        const ssoUrl = `${this.baseUrl}users/authenticate/initiate`;
        const params = new URLSearchParams();

        // Add DTO fields as query parameters
        Object.keys(dto).forEach(key => params.append(key, dto[key]));

        // Redirect the browser directly to the backend SAML initiation endpoint
        window.location.href = `${ssoUrl}?${params.toString()}`;
    }

    logout() {
        // this.http
        //   .delete(this.baseUrl + 'users/token', {
        //     withCredentials: true,
        //   })
        //   .subscribe();
        return new Promise((_) => {
            jwtAuthHelper.removeAuthToken();
            const ssoUrl = `${this.baseUrl}users/authenticate/logout`
            window.location.href = `${ssoUrl}`;
        });
    }

    verifyTwoFA(dto: AuthenticationViewModel) {
        return firstValueFrom(this.http
            .put(this.baseUrl + 'twoFactorAuthentication', dto, {
                withCredentials: true,
            })
            .pipe(
                map((res: any) => {
                    if (res.authToken && res.refreshToken) {
                        this.setAuthToken(res.authToken, res.refreshToken);
                    }
                    return res.message;
                })
            ));
    }

    sendTwoFA(dto: AuthenticationViewModel) {
        return firstValueFrom(this.http
            .post(this.baseUrl + 'twoFactorAuthentication', dto, {
                withCredentials: true,
            })
            .pipe(
                map((res: any) => {
                    return res.message;
                })
            )
        );
    }

    sendResetLink(username: string) {
        return this.http
            .put(
                this.baseUrl + `users/password/reset-token?email=${username}`,
                {},
                {withCredentials: true}
            )
            .pipe(
                map((res: any) => {
                    return res.message;
                })
            );
    }

    resetPassword(dto: CreatePasswordViewModel) {
        return firstValueFrom(this.http
            .post(this.baseUrl + 'users/password', dto, {withCredentials: true})
            .pipe(
                map((res: any) => {
                    return res.message;
                })
            ));
    }

    verifyResetTokenExpiration(dto: CreatePasswordViewModel){
        return firstValueFrom(this.http
            .post(this.baseUrl + 'users/password/expiration', dto, {withCredentials: true}));
    }
    
    refreshToken(oldJwtToken: string, refresJwtToken: string) {
        return this.http
            .post(
                this.baseUrl + 'users/token',
                {
                    authToken: oldJwtToken,
                    refreshToken: refresJwtToken,
                },
                {
                    withCredentials: true,
                }
            )
            .pipe(
                map((res: any) => {
                    if (res.authToken && res.refreshToken) {
                        this.setAuthToken(res.authToken, res.refreshToken);
                    }
                    return res.message.toString().startsWith('TokenRefreshed');
                })
            );
    }

    modifyToken(model: ModifyTokenModel) {
        return firstValueFrom(this.http
            .put(this.baseUrl + 'users/token', model, {
                withCredentials: true,
            })
            .pipe(
                map((res: any) => {
                    if (res.authToken && res.refreshToken) {
                        this.setAuthToken(res.authToken, res.refreshToken);
                    }
                    return res.message.toString().startsWith('TokenModified');
                })
            ));
    }

    getUserIdentityProviderByUsername(username:string){
        return firstValueFrom(this.http.get(this.baseUrl + `users/identityprovider/${username}`).pipe(
            map((res: any) => {
              return res['identityProvider'] as IdentityProviderVM;
            })));
    }

    updateUserIdentityProviderClaimByUsername(username: string, options: IdentityProviderUpdateModel) {
        return firstValueFrom(this.http.put(this.baseUrl + `users/${username}/identityprovider`, options).pipe(
          map((res: any) => {
            
            return res;
          })));
      }
}
