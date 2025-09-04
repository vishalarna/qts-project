import { moduleMetadata } from '@storybook/angular';
import { CommonModule } from '@angular/common';
// also exported from '@storybook/angular' if you can deal with breaking changes in 6.1
import { Story, Meta } from '@storybook/angular/types-6-0';

import { PasswordComponent } from './password.component';
import { MatIconModule } from '@angular/material/icon';
import { IconComponent } from '../icon/icon.component';

export default {
  title: 'Base Components/Password',
  component: PasswordComponent,
  decorators: [
    moduleMetadata({
      declarations: [IconComponent],
      imports: [CommonModule, MatIconModule],
    }),
  ],
  argTypes: { changed: { action: 'changed' } },
} as Meta;

const Template: Story<PasswordComponent> = (args: PasswordComponent) => ({
  props: args,
});

export const ShowAsPassword = Template.bind({});
ShowAsPassword.args = {};

export const ShowAsPlainText = Template.bind({});
ShowAsPlainText.args = {
  showPassword: true,
};

export const Disabled = Template.bind({});
Disabled.args = {
  disabled: true,
};
