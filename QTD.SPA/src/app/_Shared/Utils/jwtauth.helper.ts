import {JwtHelperService} from '@auth0/angular-jwt';
import {environment} from 'src/environments/environment';
import {DataBroadcastService} from '../services/DataBroadcast.service';
import {CustomClaimTypes} from './CustomClaims';

export abstract class jwtAuthHelper {
  private static readonly helper = new JwtHelperService();
  private static _databroadcastService: DataBroadcastService;

  /**
   *
   */
  constructor(private databroadcastService: DataBroadcastService) {
    jwtAuthHelper._databroadcastService = databroadcastService;
  }

  public async setAuthToken(token: string, refreshToken: string) {

    sessionStorage.removeItem('auth_token');
    sessionStorage.removeItem('refres_token');

    let doubleEncodedJWTToken = btoa(token);
    let doubleEncodedRefreshToken = btoa(refreshToken);

    sessionStorage.setItem('auth_token', doubleEncodedJWTToken);
    sessionStorage.setItem('refres_token', doubleEncodedRefreshToken);
    jwtAuthHelper._databroadcastService.isUserLoggedIn.next(true);
  }

  public static removeAuthToken() {
    sessionStorage.removeItem('auth_token');
    sessionStorage.removeItem('refres_token');
    jwtAuthHelper._databroadcastService.isUserLoggedIn.next(false);
  }

  public static get ValidAuthToken(): string {
    let encodedToken = atob(sessionStorage.getItem('auth_token') ?? '');
    return encodedToken;
  }

  public static get ValidRefreshToken(): string {
    let encodedToken = atob(sessionStorage.getItem('refres_token') ?? '');
    return encodedToken;
  }

  public static get AuthTokenExpirationDate() {
    return jwtAuthHelper.helper.getTokenExpirationDate(
      jwtAuthHelper.ValidAuthToken
    );
  }

  public static IsAuthTokenExpired(token: string) {
    return jwtAuthHelper.helper.isTokenExpired(token);
  }

  public static get isAuthTokenValid(): boolean {
    let authToken = jwtAuthHelper.ValidAuthToken;
    if (authToken && !jwtAuthHelper.IsAuthTokenExpired(authToken)) {
      return true;
    }

    return false;
  }

  public static get isRefreshTokenValid(): boolean {
    let refreshToken = jwtAuthHelper.ValidRefreshToken;
    if (
      refreshToken !== '' &&
      !jwtAuthHelper.IsAuthTokenExpired(refreshToken)
    ) {
      return true;
    }

    return false;
  }

  public static get unPackJWTToken() {
    return jwtAuthHelper.helper.decodeToken(jwtAuthHelper.ValidAuthToken);
  }

  static get LoggedInUser() {
    let token = jwtAuthHelper.unPackJWTToken;
    return token == undefined ? '' : token[CustomClaimTypes.UserName];
  }

  static get SelectedInstance() {
    let token = jwtAuthHelper.unPackJWTToken;
    return token == undefined ? '' : token[CustomClaimTypes.InstanceName];
  }

  static get IsBetaInstance() {
    let token = jwtAuthHelper.unPackJWTToken;
    let s = token == undefined ? '' : token[CustomClaimTypes.IsBetaInstance];
    if(!s) return false;
    return s.toLowerCase() === 'true';
  }

  static get IsEmployee() {
    let token = jwtAuthHelper.unPackJWTToken;
    return token == undefined ? '' : token[CustomClaimTypes.IsEmployee];
  }

  static get IsQtdUser() {
    let token = jwtAuthHelper.unPackJWTToken;
    return token == undefined ? '' : token[CustomClaimTypes.IsQtdUser];
  }

  static get IsAdminUser() {
    let token = jwtAuthHelper.unPackJWTToken;
    return token == undefined ? '' : token[CustomClaimTypes.IsAdmin];
  }

  static get HasMultipleInstances() {
    let token = jwtAuthHelper.unPackJWTToken;
    return token == undefined ? '' : token[CustomClaimTypes.HasMultipleInstances];
  }

  static get HasZeroInstances() {
    let token = jwtAuthHelper.unPackJWTToken;
    return token == undefined ? '' : token[CustomClaimTypes.HasZeroInstances];
  }

  static get HasTfaRequired() {
    let token = jwtAuthHelper.unPackJWTToken;
    return token == undefined ? '' : token[CustomClaimTypes.TfaRequired];
  }
}
