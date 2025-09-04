import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { AuthenticationViewModel } from 'src/app/_DtoModels/Auth/AuthenticateViewModel';
import { AuthService } from 'src/app/_Services/Auth/auth.service';
import { jwtAuthHelper } from 'src/app/_Shared/Utils/jwtauth.helper';
import { LicenseHelperService } from 'src/app/_Shared/services/licenseHelper.service';

@Component({
  selector: 'app-twofactorauth',
  templateUrl: './twofactorauth.component.html',
  styleUrls: ['./twofactorauth.component.scss'],
})
export class TwoFactorAuthComponent implements OnInit {
  authVM: AuthenticationViewModel = new AuthenticationViewModel();
  twoFAForm!: UntypedFormGroup;

  /**
   * Username for 2FA code is sent and to verify against
   */
  @Input()
  Username: string;
  /**
   * Controls the check box for do not ask on this device for 30 days
   */
  @Input()
  DoNotAsk: boolean;

  /**
   * This will add waiting sign of loading
   */
  @Input()
  processing = false;

  /**
   * Processing indicator for resend button
   */
  @Input()
  resending = false;

  /**
   * The verification code used to establish the 2FA
   */
  @Input()
  VerificationCode: string;

  /**
   * Emitted when the verification is successful
   */
  @Output()
  VerifySuccess = new EventEmitter<Event>();

  /**
   * Emitted when the communication with the server was successful
   * but the server rejeted the verification code.
   * i.e. the Verification code is incorrect or expired
   */
  @Output()
  VerifyFailure = new EventEmitter<Event>();

  /**
   * Emitted when the server returns an HTTPERROR when
   * attemptings to verify the token
   */
  @Output()
  VerifyError = new EventEmitter<Event>();

  /**
   * Emitted when the resend communication is successful
   */
  @Output()
  ResendSuccess = new EventEmitter<Event>();

  /**
   * Emitted when the resend communication failed
   */
  @Output()
  ResendError = new EventEmitter<Event>();

  returnUrl: any;

  constructor(
    private translate: TranslateService,
    private fb: UntypedFormBuilder,
    private alertService: SweetAlertService,
    private _router: Router,
    private authService: AuthService,
    private _activatedRoute: ActivatedRoute,
    private licenseHelper:LicenseHelperService
  ) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
  }

  ngOnInit(): void {
    
    this.ready2FAForm();
    if (!this.Username)
      this.twoFAForm.get('username')?.setValue(jwtAuthHelper.LoggedInUser);

    this.returnUrl =
      this._activatedRoute.snapshot.queryParams['returnUrl'] || '/home/index';
  }

  ready2FAForm() {
    //that's how reactive forms are used, angular uses normal forms as well. But reactive forms are more responsive and have more control
    this.twoFAForm = this.fb.group({
      username: new UntypedFormControl({value:this.Username,disabled:true}, [
        Validators.required,
        Validators.email,
      ]),
      verificationCode: new UntypedFormControl(this.VerificationCode, [
        Validators.required,Validators.pattern(/^\d+$/)
      ]),
      doNotAsk:new UntypedFormControl(this.DoNotAsk)
    });
  }

  async submit2FA(ref?: any) {
    
    if (!this.twoFAForm.valid) {
      this.alertService.errorAlert(
        'Error',
        this.translate.instant('L.validErr')
      );
      return;
    }
    this.authVM.userName = this.twoFAForm.get('username')?.value;
    this.authVM.verificationCode = this.twoFAForm.get('verificationCode')?.value;
    this.authVM.doNotAsk =this.twoFAForm.get('doNotAsk')?.value;

    try {
      const res = await this.authService.verifyTwoFA(this.authVM);
      this.alertService.successToast(this.translate.instant('L.' + res));
      
      if (this.returnUrl) {
        this._router.navigate([this.returnUrl]);
      } else {
        this._router.navigate(['/home']);
      }
    } catch (error) {
      this.alertService.errorAlert( 'Invalid code entered');
    }
  }

  async resend2FA(ref?: any) {
    if (
      this.twoFAForm.get('username')?.hasError('email') ||
      this.twoFAForm.get('username')?.hasError('required')
    ) {
      console.error('Invalid 2FA user');
      return;
    }
    this.authVM.userName = this.twoFAForm.get('username')?.value;
    this.authVM.doNotAsk =this.twoFAForm.get('doNotAsk')?.value;
    this.authVM.verificationCode = this.twoFAForm.get('verificationCode')?.value;
    await this.authService.sendTwoFA(this.authVM).then((res) => {
      this.alertService.successToast(this.translate.instant('L.' + res));
    });
  }

  
  signOut() {
    this.authService.logout();
  }
}
