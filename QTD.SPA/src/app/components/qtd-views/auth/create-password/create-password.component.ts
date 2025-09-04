import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  AbstractControl,
  AbstractControlOptions,
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { CreatePasswordViewModel } from 'src/app/_DtoModels/Auth/CreatePasswordViewModel';
import { AuthService } from 'src/app/_Services/Auth/auth.service';

@Component({
  selector: 'app-create-password',
  templateUrl: './create-password.component.html',
  styleUrls: ['./create-password.component.scss'],
})
export class CreatePasswordComponent implements OnInit {
  createPassForm!: UntypedFormGroup;
  passwordVM: CreatePasswordViewModel = new CreatePasswordViewModel();

  /**
   * The Username field.
   */
  @Input()
  Username: string;

  /**
   * The ConfirmPassword field.
   */
  @Input()
  ConfirmPassword: string;

  /**
   * * *Required when the mode=change*
   *
   * The users current password
   */
  @Input()
  CurrentPassword: string;

  /**
   * The mode of setting the password
   *
   * *change* is when a logged in user wishes to change his/her password
   *      requires the current password to change
   *
   * *reset* is for a user who forgot their password
   *      requires a token issued by the server, sent to an email address for validation
   *      this will likely just be a *hidden* value
   *
   * At this time there is no need for a *register* option because new users will be sent
   * through the *reset* workflow after their user is created
   */
  @Input()
  Mode: 'change' | 'reset';

  /**
   * The desired new password
   */
  @Input()
  Password: string;

  /**
   * *Required when the mode=reset*
   *
   * The reset token issued from the server
   * during the forgot password process
   * Should be sent to the server for validation before
   * changing the password
   */
  @Input()
  ResetPasswordToken: string;

  /**
   * Make sure you document these
   */
  @Input()
  processing = false;

  /**
   * Emitited when the password is successfully changed
   */
  @Output()
  ChangeSuccessful = new EventEmitter<Event>();

  /**
   * Emitted when there is no error in communication between
   * the server and the client but the server failed the operation
   * ex.  When the server determines the password isn't strong enough
   */
  @Output()
  ChangeFailed = new EventEmitter<Event>();

  /**
   * Emitted when the server responds with and error
   */
  @Output()
  ChangeError = new EventEmitter<Event>();

  constructor(
    private translate: TranslateService,
    private fb: UntypedFormBuilder,
    private alertService: SweetAlertService,
    private _router: Router,
    private _activatedRoute: ActivatedRoute,
    private authService: AuthService
  ) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
  }

  ngOnInit(): void {
    this.passwordVM.token = this._activatedRoute.snapshot.queryParams.token;
    this.readyCreatePassForm();
    this.verifyResetTokenExpiration();
  }

  readyCreatePassForm() {
    this.createPassForm = this.fb.group({
      email: new UntypedFormControl(this.Username, [
        Validators.required,
        Validators.email,
      ]),
      password: new UntypedFormControl(this.Password, [Validators.required,Validators.minLength(14)]),
      confirmPassword: new UntypedFormControl(this.ConfirmPassword, [
        Validators.required,
      ]),
    });
  }

  async resetPassword(ref?: any) {
    if (!this.createPassForm.valid) {
      this.alertService.errorAlert(
        'Error',
        this.translate.instant('L.validErr')
      );

      return;
    }
    this.processing = true;
    Object.assign(this.passwordVM, this.createPassForm.value);

    await this.authService
      .resetPassword(this.passwordVM)
      .then(
        (res) => {
          this.translate.instant('L.' + this.alertService.successToast(res));

          this._router.navigate(['/auth/login-password']);
        },
        (rej) => {
          this.translate.instant('L.' + this.alertService.errorAlert(rej));
        }
      )
      .finally(() => (this.processing = false));
  }

  async verifyResetTokenExpiration(){
    await this.authService
    .verifyResetTokenExpiration(this.passwordVM)
    .then(
      (res) => {}
      ,(rej) => {
        const appException = rej.headers.get('Application-Error').replace(/"/g,'');
        this.translate.instant('L.' + this.alertService.errorAlert(appException));
      }
    )
  }
}
