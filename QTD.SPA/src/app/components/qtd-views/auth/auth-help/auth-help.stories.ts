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
import { AuthComponent } from '../auth.component';
import { AuthHelpComponent } from './auth-help.component';
import { BaseModule } from 'src/app/components/base/base.module';

export default {
  title: 'QTD Components/Auth/AuthHelp',
  component: AuthHelpComponent,
  decorators: [
    moduleMetadata({
      declarations: [AuthHelpComponent],
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
} as Meta;

const Template: Story<AuthHelpComponent> = (args: AuthHelpComponent) => ({
  props: args,
});

export const Default = Template.bind({});
Default.args = {};
