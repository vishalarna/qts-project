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
import { CreatePasswordComponent } from './create-password.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatButtonModule } from '@angular/material/button';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

export default {
  title: 'QTD Components/Auth/CreatePassword',
  component: CreatePasswordComponent,
  decorators: [
    moduleMetadata({
      declarations: [CreatePasswordComponent],
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
  argTypes: { ChangeSuccessful: { action: 'Password Changed Successfully' },
              ChangeFailed : {action: 'Password Change Failed'} },
} as Meta;

const Template: Story<CreatePasswordComponent> = (
  args: CreatePasswordComponent
) => ({
  props: args,
});

const delay = (t: number) => new Promise((resolve) => setTimeout(resolve, t));

const reset = (self: any) => {
  
  self.processing = true;
  
  return delay(3000).then(() => {
    self.processing = false;
    self.alertService.successToast('Reset Password Response Received');
    self.ChangeSuccessful.emit();
  });
};

const error = (self: any) => {
  
  self.processing = true;
  
  return delay(3000).then(() => {
    self.processing = false;
    self.alertService.errorToast('Reset Password Failed');
    self.ChangeFailed.emit();
  });
};

export const Default = Template.bind({});
Default.args = {
  processing: false,
  resetPassword: reset,
};

export const CreatePasswordWithWrongEmail = Template.bind({});
CreatePasswordWithWrongEmail.args = {
  Username: 'qts',
  Password: 'qts',
  ConfirmPassword: 'Password1',
  resetPassword: reset,
};

export const CreatePasswordWithPasswordOnly = Template.bind({});
CreatePasswordWithPasswordOnly.args = {
  Username: 'qts@qualitytrainingsystems.com',
  Password: 'qts',
  ConfirmPassword: '',
  resetPassword: reset,
};

export const CreatePasswordWithConfirmPasswordOnly = Template.bind({});
CreatePasswordWithConfirmPasswordOnly.args = {
  Username: 'qts@qualitytrainingsystems.com',
  Password: '',
  ConfirmPassword: 'qts',
  resetPassword: reset,
};

export const CreatePasswordWithUnmatchedPassword = Template.bind({});
CreatePasswordWithUnmatchedPassword.args = {
  Username: 'qts@qualitytrainingsystems.com',
  Password: 'qts1',
  ConfirmPassword: 'Password1',
  resetPassword: reset,
};

export const CreatePasswordError = Template.bind({});
CreatePasswordError.args = {
  Username: 'aqsa@qualitytrainingsystems.com',
  Password: 'qts1',
  ConfirmPassword: 'qts1',
  resetPassword: error,
};
