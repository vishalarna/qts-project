import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { AuthService } from 'src/app/_Services/Auth/auth.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss'],
})
export class ForgotPasswordComponent implements OnInit {
  resetPassForm!: UntypedFormGroup;

  /**
   * The Username requesting the password reset
   */
  @Input()
  Username: string;

  /**
   * This will add waiting sign of loading
   */
  @Input()
  processing = false;

  /**
   * Emitited when the forget password link generated
   */
  @Output()
  LinkSucess = new EventEmitter<Event>();

  /**
   * Emitted when there is no error in communication between
   * the server and the client but the server failed to send
   * the email of reset password.
   */
  @Output()
  LinkFailed = new EventEmitter<Event>();

  constructor(
    private translate: TranslateService,
    private fb: UntypedFormBuilder,
    private alertService: SweetAlertService,
    private authService: AuthService
  ) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
  }

  ngOnInit(): void {
    this.readyResetForm();
  }

  readyResetForm() {
    this.resetPassForm = this.fb.group({
      email: new UntypedFormControl('', [Validators.required, Validators.email]),
    });
  }

  getResentLink(ref?: any) {
    if (!this.resetPassForm.valid) {
      this.alertService.errorAlert(
        'Error',
        this.translate.instant('L.validErr')
      );

      return;
    }
  this.authService
  .sendResetLink(this.resetPassForm.get('email')?.value)
  .subscribe({
    next: (res) => {
      this.alertService.successToast(this.translate.instant('L.' + res));
    },
    error: (error) => { 
      const appException = error.headers.get('Application-Error');
      this.alertService.errorAlert(appException);
    }
  });
  }
}
