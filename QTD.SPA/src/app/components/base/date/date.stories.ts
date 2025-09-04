import { DateComponent } from './date.component';
import { moduleMetadata } from '@storybook/angular';
import { CommonModule } from '@angular/common';
// also exported from '@storybook/angular' if you can deal with breaking changes in 6.1
import { Story, Meta } from '@storybook/angular/types-6-0';
import { MatIconModule } from '@angular/material/icon';
import { IconComponent } from '../icon/icon.component';

export default {
  title: 'Base Components/Date',
  component: DateComponent,
  decorators: [
    moduleMetadata({
      declarations: [IconComponent],
      imports: [CommonModule, MatIconModule],
    }),
  ],
  argTypes: { changed: { action: 'changed' } },
} as Meta;

const Template: Story<DateComponent> = (args: DateComponent) => ({
  props: args,
});

export const DefaultDate= Template.bind({});
DefaultDate.args = {};

