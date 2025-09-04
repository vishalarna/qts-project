import { moduleMetadata } from '@storybook/angular';
import { CommonModule } from '@angular/common';
// also exported from '@storybook/angular' if you can deal with breaking changes in 6.1
import { Story, Meta } from '@storybook/angular/types-6-0';

import { SpinnerComponent } from './spinner.component';

export default {
  title: 'Base Components/Spinner',
  component: SpinnerComponent
} as Meta;

const Template: Story<SpinnerComponent> = (args: SpinnerComponent) => ({
  props: args,
});

export const Border = Template.bind({});
Border.args = {
  color: 'orange'
};


export const Grow = Template.bind({});
Grow.args = {
    variant: 'grow',
    color: 'blue'
};

