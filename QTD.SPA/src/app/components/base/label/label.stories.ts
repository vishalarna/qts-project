import { moduleMetadata } from '@storybook/angular';
import { CommonModule } from '@angular/common';
// also exported from '@storybook/angular' if you can deal with breaking changes in 6.1
import { Story, Meta } from '@storybook/angular/types-6-0';

import { LabelComponent } from './label.component';

export default {
  title: 'Base Components/Label',
  component: LabelComponent,
} as Meta;

const Template: Story<LabelComponent> = (args: LabelComponent) => ({
  props: args,
});

export const Default = Template.bind({});
Default.args = {
  for: 'Username',
  text: 'Username',
};
