import { moduleMetadata } from '@storybook/angular';
import { CommonModule } from '@angular/common';
// also exported from '@storybook/angular' if you can deal with breaking changes in 6.1
import { Story, Meta } from '@storybook/angular/types-6-0';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { RouterTestingModule } from '@angular/router/testing';
import { TextboxComponent } from 'src/app/components/base/textbox/textbox.component';
import { LabelComponent } from 'src/app/components/base/label/label.component';
import { PasswordComponent } from 'src/app/components/base/password/password.component';
import { ButtonComponent } from 'src/app/components/base/button/button.component';
import { InputErrorComponent } from 'src/app/components/base/input-error/input-error.component';
import { TwoFactorAuthComponent } from './twofactorauth.component';
import { BaseModule } from 'src/app/components/base/base.module';

export default {
  title: 'QTD Components/Auth/TwoFactorAuth',
  component: TwoFactorAuthComponent,
  decorators: [
    moduleMetadata({
      declarations: [TwoFactorAuthComponent],
      imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        RouterTestingModule,
        LocalizeModule,
        BaseModule,
      ],
    }),
  ],
  argTypes: { VerifySuccess: { action: 'Token Received Successfully' },
              VerifyFailure : {action: 'Invalid Token'},
              ResendSuccess: { action: 'Token Resend Successfully' },
              ResendError : {action: 'Resend Token Failed'} },
} as Meta;

const Template: Story<TwoFactorAuthComponent> = (
  args: TwoFactorAuthComponent
) => ({
  props: args,
});

const resend2FaCode = (self: any) => {
  self.resending = true;
  return new Promise((_) => {
    setTimeout(() => {
      self.resending = false;
      self.alertService.successToast('Token Resend Request Successfull');
      self.ResendSuccess.emit();
    }, 3000);
  }).then(() => {});
};

const submit2FA = (self: any) => {
  self.processing = true;
  return new Promise((_) => {
    setTimeout(() => {
      self.processing = false;
      self.alertService.successToast('Token Response Recieved');
      self.VerifySuccess.emit();
    }, 3000);
  }).then(() => {});
};

export const Default = Template.bind({});
Default.args = {};

export const TwoFAWithNoEmail = Template.bind({});
TwoFAWithNoEmail.args = {
  Username: '',
  VerificationCode: '724BA',
  submit2FA: submit2FA,
  resend2FA: resend2FaCode,
};

export const TwoFAWithEmailAndNoCode = Template.bind({});
TwoFAWithEmailAndNoCode.args = {
  Username: 'qtd@qualitytrainingsystems.com',
  VerificationCode: '',
  submit2FA: submit2FA,
  resend2FA: resend2FaCode,
};

export const TwoFAFailedToken = Template.bind({});
TwoFAFailedToken.args = {
  Username: 'qtd@qualitytrainingsystems.com',
  VerificationCode: '111',
  resend2FA: resend2FaCode,

  submit2FA: (self: any) => {
    self.processing = true;
    return new Promise((_) => {
      setTimeout(() => {
        self.alertService.errorToast('Invalid Token for 2FA');
        self.processing = false;
        self.VerifyFailure.emit();
      }, 3000);
    });
  },
};

export const TwoFASuccessToken = Template.bind({});
TwoFASuccessToken.args = {
  Username: 'qtd@qualitytrainingsystems.com',
  VerificationCode: '111',

  resend2FA: resend2FaCode,

  submit2FA: (self: any) => {
    self.processing = true;
    return new Promise((_) => {
      setTimeout(() => {
        self.alertService.successToast('Successfully verified Token for 2FA');
        self.processing = false;
        self.VerifySuccess.emit();
      }, 3000);
    });
  },
};
