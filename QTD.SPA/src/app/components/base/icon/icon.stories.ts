import { moduleMetadata } from '@storybook/angular';
import { CommonModule } from '@angular/common';
// also exported from '@storybook/angular' if you can deal with breaking changes in 6.1
import { Story, Meta } from '@storybook/angular/types-6-0';

import { IconComponent } from './icon.component';
import { MatIconModule } from '@angular/material/icon';

export default {
  title: 'Base Components/Icon',
  component: IconComponent,
  decorators: [
    moduleMetadata({
      imports: [CommonModule, MatIconModule],
    }),
  ],
} as Meta;

const Template: Story<IconComponent> = (args: IconComponent) => ({
  props: args,
});

export const Visibility = Template.bind({});
Visibility.args = {
  icon: 'visibility',
};

// export const FontAwesomeEyeOpen = Template.bind({});
// FontAwesomeEyeOpen.args = {
//   provider: 'fa',
//   icon: 'fa-eye',
// };

export const AddCircle = Template.bind({});
AddCircle.args = {
  icon: 'add_circle',
};

// export const FontAwesomeAddCircle = Template.bind({});
// FontAwesomeAddCircle.args = {
//   provider: 'fa',
//   icon: 'fa-circle-plus',
// };