import { moduleMetadata } from '@storybook/angular';
import { CommonModule } from '@angular/common';
// also exported from '@storybook/angular' if you can deal with breaking changes in 6.1
import { Story, Meta } from '@storybook/angular/types-6-0';

import { SpinnerComponent } from '../spinner/spinner.component';
import { ButtonComponent } from './button.component';
import { MatButtonModule } from '@angular/material/button';
import { IconComponent } from '../icon/icon.component';
import { MatIconModule } from '@angular/material/icon';

export default {
  title: 'Base Components/Button',
  component: ButtonComponent,
  decorators: [
    moduleMetadata({
      declarations: [SpinnerComponent, IconComponent],
      imports: [CommonModule, MatButtonModule, MatIconModule],
    }),
  ],
  argTypes: { clicked: { action: 'clicked' } },
} as Meta;

const Template: Story<ButtonComponent> = (args: ButtonComponent) => ({
  props: args,
});

export const Primary = Template.bind({});
Primary.args = {
  disabled: false,
  text: 'Primary Button',
  variant: 'contained',
  icon: '',
  spinner: false,
  shape: 'rect',
};

export const PrimaryIconButton = Template.bind({});
PrimaryIconButton.args = {
  disabled: false,
  text: 'Primary Icon button',
  variant: 'contained',
  icon: 'home',
  ButtonType: 'icon',
};


export const PrimaryWithIcon = Template.bind({});
PrimaryWithIcon.args = {
  disabled: false,
  text: 'Primary Button with Icon',
  variant: 'contained',
  icon: 'home',
  iconPosition :'left',
  spinner: false,
  shape: 'rect',
};

export const PrimaryWithSpinner = Template.bind({});
PrimaryWithSpinner.args = {
  disabled: false,
  text: 'Primary Button with Spinner',
  variant: 'contained',
  icon: '',
  spinner: true,
  shape: 'rect',
};

export const PrimaryButtonDisabled = Template.bind({});
PrimaryButtonDisabled.args = { 
  color: 'secondary',
  disabled: true,
  text: 'Disabled Button',
  variant: 'contained',
  icon: '',
  spinner: false,
  shape: 'rect',
};

export const Secondary = Template.bind({});
Secondary.args = {
  color: 'secondary',
  disabled: false,
  text: 'Secondary Button',
  variant: 'contained',
  icon: '',
  spinner: false,
  shape: 'rect',
};

export const SecondaryWithIcon = Template.bind({});
SecondaryWithIcon.args = {
  color: 'secondary',
  disabled: false,
  text: 'Secondary Button with Icon',
  variant: 'contained',
  icon: 'home',
  spinner: false,
  shape: 'rect',
};

export const SecondaryWithSpinner = Template.bind({});
SecondaryWithSpinner.args = {
  color: 'secondary',
  disabled: false,
  text: 'Secondary Button with Spinner',
  variant: 'contained',
  icon: '',
  spinner: true,
  shape: 'rect',
};

export const SecondaryButtonDisabled = Template.bind({});
SecondaryButtonDisabled.args = {
  color: 'secondary',
  disabled: true,
  text: 'Secondary Button',
  variant: 'contained',
  icon: '',
  spinner: false,
  shape: 'rect',
};


export const Outlined = Template.bind({});
Outlined.args = {
  color: 'secondary',
  disabled: false,
  text: 'Outlined',
  variant: 'outlined',
  icon: '',
  spinner: false,
  shape: 'rect',
};

export const Text = Template.bind({});
Text.args = {
  color: 'primary',
  disabled: false,
  text: 'Outlined',
  variant: 'text',
  icon: '',
  spinner: false,
  shape: 'rect',
};


// a story where you click it in, timeout for 1 second, then show disabled/spinner
