import { moduleMetadata } from '@storybook/angular';
import { CommonModule } from '@angular/common';
// also exported from '@storybook/angular' if you can deal with breaking changes in 6.1
import { Story, Meta } from '@storybook/angular/types-6-0';

import { TextboxComponent } from './textbox.component';
import { FormsModule } from '@angular/forms';

export default {
  title: 'Base Components/Textbox',
  component: TextboxComponent,
  decorators: [
    moduleMetadata({
      declarations: [],
      imports: [CommonModule, FormsModule],
    }),
  ],
  argTypes: { changed: { action: 'changed' } },
} as Meta;

const Template: Story<TextboxComponent> = (args: TextboxComponent) => ({
  props: args,
});

export const Default = Template.bind({});
Default.args = {
  defaultValue: '',
  disabled: false,
  name: '',
  placeholder: '',
};

export const WithPlaceholder = Template.bind({});
WithPlaceholder.args = {
  defaultValue: '',
  disabled: false,
  name: '',
  placeholder: 'Please Enter some text',
};

export const Disabled = Template.bind({});
Disabled.args = {
  defaultValue: '',
  disabled: true,
  name: '',
  placeholder: '',
};
