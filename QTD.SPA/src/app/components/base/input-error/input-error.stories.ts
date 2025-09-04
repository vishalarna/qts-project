import { moduleMetadata } from '@storybook/angular';
import { CommonModule } from '@angular/common';
// also exported from '@storybook/angular' if you can deal with breaking changes in 6.1
import { Story, Meta } from '@storybook/angular/types-6-0';

import { InputErrorComponent } from './input-error.component';

export default {
  title: 'Base Components/InputError',
  component: InputErrorComponent,
} as Meta;

const Template: Story<InputErrorComponent> = (args: InputErrorComponent) => ({
  props: args,
});

export const Default = Template.bind({});
Default.args = {
  text: 'This is a default error message',
};