import { moduleMetadata } from '@storybook/angular';
import { CommonModule } from '@angular/common';
// also exported from '@storybook/angular' if you can deal with breaking changes in 6.1
import { Story, Meta } from '@storybook/angular/types-6-0';

import { AddNewMenuItemComponent } from './add-new-menu-item.component';
import { TextboxComponent } from '../textbox/textbox.component';
import { ButtonComponent } from '../button/button.component';
import { IconComponent } from '../icon/icon.component';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';

export default {
  title: 'Base Components/AddNewMenuItem',
  component: AddNewMenuItemComponent,
  decorators: [
    moduleMetadata({
      declarations: [TextboxComponent, ButtonComponent, IconComponent],
      imports: [CommonModule, FormsModule, MatIconModule],
    }),
  ],
  argTypes: {
    Saved: { action: 'Saved Clicked' },
    Cancelled: { action: 'Cancel Clicked' },
  },
} as Meta;

const Template: Story<AddNewMenuItemComponent> = (
  args: AddNewMenuItemComponent
) => ({
  props: args,
});

export const Default = Template.bind({});
Default.args = {};
