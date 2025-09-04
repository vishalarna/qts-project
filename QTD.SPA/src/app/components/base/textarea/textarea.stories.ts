import { moduleMetadata } from '@storybook/angular';
import { CommonModule } from '@angular/common';
// also exported from '@storybook/angular' if you can deal with breaking changes in 6.1
import { Story, Meta } from '@storybook/angular/types-6-0';

import { MatIconModule } from '@angular/material/icon';
import { TextareaComponent } from './textarea.component';

export default {
  title: 'Base Components/Textarea',
  component: TextareaComponent,
  decorators: [
    moduleMetadata({
      imports: [CommonModule, MatIconModule],
    }),
  ],
} as Meta;

const Template: Story<TextareaComponent> = (args: TextareaComponent) => ({
  props: args,
});

export const Default = Template.bind({});
Default.args = {
  placeholder: 'Enter Text ',
};