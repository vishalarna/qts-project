import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {
    UntypedFormGroup,
    UntypedFormBuilder,
    UntypedFormControl,
    Validators,
} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {TranslateService} from '@ngx-translate/core';
import {LoginModel} from 'src/app/_DtoModels/Auth/login.dto';
import {DateFormatPipe} from 'src/app/_Pipes/date-format.pipe';
import {AuthService} from 'src/app/_Services/Auth/auth.service';
import {ApiClientSettingsService} from 'src/app/_Services/QTD/ClientSettings/api.clientsettings.service';
import {SweetAlertService} from 'src/app/_Shared/services/sweetalert.service';

@Component({
    selector: 'app-login-sso',
    templateUrl: './login-sso.component.html',
    styleUrls: ['./login-sso.component.scss'],
})
export class LoginSsoComponent implements OnInit {
    loginForm!: UntypedFormGroup;
    @Input() processing = false;
    @Input() Username: string = "";
    @Input() ReturnUrl: string;
    dtoLogin: LoginModel = new LoginModel();
    returnUrl!: string;
    dateFormatPipe = new DateFormatPipe(this.clientSettingsService);
    authToken:string;
    refreshToken:string;
    
    constructor(
        private translate: TranslateService,
        private fb: UntypedFormBuilder,
        private authService: AuthService,
        private alertService: SweetAlertService,
        private _activatedRoute: ActivatedRoute,
        public clientSettingsService: ApiClientSettingsService,
        private _router:Router
    ) {

        const browserLang = localStorage.getItem('lang') ?? 'en';
        this.translate.use(browserLang);
    }

    ngOnInit(): void {
        this.readyLoginForm();
        this.returnUrl =
            this._activatedRoute.snapshot.queryParams['returnUrl'] || '/home/index';
            this._activatedRoute.fragment.subscribe(fragment => {
                if(fragment){
                    const params = new URLSearchParams(fragment);
                    this.authToken = params.get('authToken');
                    this.refreshToken = params.get('refreshToken');
                    if(this.authToken && this.refreshToken){
                        this.authService.setAuthToken(this.authToken,this.refreshToken);
                        this._router.navigate([this.returnUrl]);
                    }
                }
              });
    }

    readyLoginForm() {
        this.loginForm = this.fb.group({
            username: new UntypedFormControl(this.Username, [
                Validators.required,
                Validators.email,
            ])
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
        await this.authService.loginSSO(this.dtoLogin)
    }

}
