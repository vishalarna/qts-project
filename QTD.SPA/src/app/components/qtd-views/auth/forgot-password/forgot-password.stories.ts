import { moduleMetadata } from '@storybook/angular';
import { CommonModule } from '@angular/common';
// also exported from '@storybook/angular' if you can deal with breaking changes in 6.1
import { Story, Meta } from '@storybook/angular/types-6-0';

import { TranslateModule, TranslateStore } from '@ngx-translate/core';
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
import { ForgotPasswordComponent } from './forgot-password.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatButtonModule } from '@angular/material/button';

export default {
  title: 'QTD Components/Auth/ForgotPassword',
  component: ForgotPasswordComponent,
  decorators: [
    moduleMetadata({
      declarations: [ForgotPasswordComponent],
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
  argTypes: { LinkSucess: { action: 'Reset Link Sent Successfully' },
              LinkFailed : {action: 'Reset Link Failed'} },
} as Meta;

const Template: Story<ForgotPasswordComponent> = (
  args: ForgotPasswordComponent
) => ({
  props: args,
});

const sendLink = (self: any) => {
  return new Promise((_) => {
    self.processing = true;
    setTimeout(() => {
      self.processing = false;
      self.alertService.successToast('Reset Link sent successfully');
      self.LinkSucess.emit();
    }, 3000);
  }).then(() => {});
};

export const Default = Template.bind({});
Default.args = {};

export const ForgotPasswordWithNoEmail = Template.bind({});
ForgotPasswordWithNoEmail.args = {
  Username: '',
  getResentLink: sendLink,
};

export const ForgotPasswordWithEmail = Template.bind({});
ForgotPasswordWithEmail.args = {
  Username: 'qtd@qualitytrainingsystems.com',
  getResentLink: sendLink,
};

export const ForgotPasswordRequestFailure = Template.bind({});
ForgotPasswordRequestFailure.args = {
  Username: 'qtd@qualitytrainingsystems.com',
  getResentLink: (self: any) => {
    return new Promise((_) => {
      self.processing = true;
      setTimeout(() => {
        self.processing = false;
        self.alertService.errorToast('Reset Link failed');
        self.LinkFailed.emit();
      }, 3000);
    }).then(() => {});
  },
};
