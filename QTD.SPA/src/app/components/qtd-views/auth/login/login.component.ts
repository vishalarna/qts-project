import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  UntypedFormGroup,
  UntypedFormBuilder,
  UntypedFormControl,
  Validators,
} from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { LoginModel } from 'src/app/_DtoModels/Auth/login.dto';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { DateFormatPipe } from 'src/app/_Pipes/date-format.pipe';
import { AuthService } from 'src/app/_Services/Auth/auth.service';
import { ApiClientSettingsService } from 'src/app/_Services/QTD/ClientSettings/api.clientsettings.service';
import { CustomClaimTypes } from 'src/app/_Shared/Utils/CustomClaims';
import { jwtAuthHelper } from 'src/app/_Shared/Utils/jwtauth.helper';
import { LicenseHelperService } from 'src/app/_Shared/services/licenseHelper.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  loginForm!: UntypedFormGroup;

  /**
   * This will add waiting sign of loading
   */
  @Input() processing = false;

  /**
   * The Username of the user logging in
   */
  @Input() Username: string;

  /**
   * The Password of the user logging in
   */
  @Input() Password: string;

  /**
   * Zafeer - I suspect this pattern is incorrect
   * Consider using this form when a session times out
   * and we haven't redirected the user away.  we may want to display
   * a popup with a login screen so the user can return to working as they were
   * for this reason I'd propose the LoginSuccess I've added below
   */
  @Input() ReturnUrl: string;

  /**
   * What does this actually do when selected?
   */
  @Input() RememberMe: boolean = false;

  /**
   * Emitted on successful login
   */
  @Output() LoginSuccess = new EventEmitter<Event>();

  /**
   * Emitted on unsuccessful login
   */
  @Output() LoginFailed = new EventEmitter<Event>();

  /**
   * Emitted on errored login
   */
  @Output() LoginError = new EventEmitter<Event>();

  dtoLogin: LoginModel = new LoginModel();
  returnUrl!: string;
  dateFormatPipe=new DateFormatPipe(this.clientSettingsService);
  //Injecting required services
  constructor(
    private translate: TranslateService,
    private fb: UntypedFormBuilder,
    private authService: AuthService,
    private alertService: SweetAlertService,
    private _router: Router,
    private _activatedRoute: ActivatedRoute,
    private licenseHelperService:LicenseHelperService,
    public clientSettingsService: ApiClientSettingsService,
    private routeGuard:RouteGuard
  ) {

    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
  }

  ngOnInit(): void {
    this.Username = this._activatedRoute.snapshot.queryParamMap.get('username') || '';  
    this.readyLoginForm();
    this.returnUrl =
      this._activatedRoute.snapshot.queryParams['returnUrl'] || '/home/index';
  }
  readyLoginForm() {
    //that's how reactive forms are used, angular uses normal forms as well. But reactive forms are more responsive and have more control
    this.loginForm = this.fb.group({
      username: new UntypedFormControl(this.Username, [
        Validators.required,
        Validators.email,
      ]),
      password: new UntypedFormControl(this.Password, [Validators.required]),
      rememberMe: new UntypedFormControl(this.RememberMe),
    });
  }
  async signInUser(ref?: any) {

    if (!this.loginForm.valid) {
      this.alertService.errorAlert(
        'Error',
        this.translate.instant('L.validErr')
      );

      return;
    }

    this.processing = true;
    Object.assign(this.dtoLogin, this.loginForm.value);
    sessionStorage.removeItem('isAdminSliderBar');
    sessionStorage.removeItem('isEmpSideBar');
    localStorage.removeItem("userImg");
    // to commnunicate with out local API
    await this.authService
      .loginJwt(this.dtoLogin)
      .then(async (res) => {
        if (jwtAuthHelper.HasTfaRequired == "true") {
          this._router.navigate(['/auth/2fa']);
        } else {
          this.alertService.successToast(this.translate.instant('L.' + res));
          var isAdmin = jwtAuthHelper.IsAdminUser;
          var hasMultipleInstances = jwtAuthHelper.HasMultipleInstances;
          var hasZeroInstances = jwtAuthHelper.HasZeroInstances;
          if(isAdmin=="True" || hasMultipleInstances=="True" || hasZeroInstances=="True"){
            this._router.navigate([this.returnUrl]);
          }
          let client = jwtAuthHelper.unPackJWTToken[CustomClaimTypes.InstanceName];
          if(client){
            await this.licenseHelperService.setLicenseData();
            localStorage.removeItem('dateFormat');
            this.dateFormatPipe.transform(new Date());
          }
          this._router.navigate([this.returnUrl]);
        }
      })
      .catch((error) => {
        const appException = error.headers.get('Application-Error');
        this.alertService.errorAlert(appException);
      })
      .finally(() =>

      (this.processing = false));


  }

}
