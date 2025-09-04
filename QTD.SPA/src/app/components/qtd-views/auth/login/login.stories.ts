import { moduleMetadata } from '@storybook/angular';
import { CommonModule } from '@angular/common';
// also exported from '@storybook/angular' if you can deal with breaking changes in 6.1
import { Story, Meta } from '@storybook/angular/types-6-0';

import { LoginComponent } from './login.component';
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
import { MatButtonModule } from '@angular/material/button';
import { BaseModule } from 'src/app/components/base/base.module';

export default {
  title: 'QTD Components/Auth/Login',
  component: LoginComponent,
  decorators: [
    moduleMetadata({
      declarations: [LoginComponent],
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
  argTypes: { LoginSuccess: { action: 'Successfully Login' },
              LoginFailed : {action: 'Login Failed'} },
} as Meta;

const Template: Story<LoginComponent> = (args: LoginComponent) => ({
  props: args,
});

const delay = (t: number) => new Promise((resolve) => setTimeout(resolve, t));

const login = (self: any) => {
  
  
  self.processing = true;
  return delay(3000).then(() => {
    self.processing = false;
    self.alertService.successToast('Login Response Recieved');
    self.LoginSuccess.emit();
  });
};

export const Default = Template.bind({});
Default.args = {
  processing: false,
  signInUser: login,
};

export const LoginWithWrongEmail = Template.bind({});
LoginWithWrongEmail.args = {
  Username: 'zafeer',
  Password: 'zafeer',
  signInUser: login
};

export const LoginWithEmailAndNoPassword = Template.bind({});
LoginWithEmailAndNoPassword.args = {
  Username: 'zafeer@qualitytrainingsystems.com',
  Password: '',
  signInUser:login
};

export const LoginWithRememberMe = Template.bind({});
LoginWithRememberMe.args = {
  Username: 'zafeer@qualitytrainingsystems.com',
  Password: 'Password1',
  RememberMe: true,
  signInUser: login,
};

export const LoginWithWrongCredentials = Template.bind({});
LoginWithWrongCredentials.args = {
  Username: 'zafeer@qualitytrainingsystems.com',
  Password: 'Password1',
  signInUser: (self: any) => {
    self.processing = true;
    return delay(3000).then(() => {
      self.processing = false;
      self.alertService.errorToast('Invalid Credentials');
      self.LoginFailed.emit();
    });
  },
};
