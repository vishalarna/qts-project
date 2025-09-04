import { moduleMetadata } from '@storybook/angular';
import { CommonModule } from '@angular/common';
// also exported from '@storybook/angular' if you can deal with breaking changes in 6.1
import { Story, Meta } from '@storybook/angular/types-6-0';

import { LinkComponent } from './link.component';

export default {
  title: 'Base Components/Link',
  component: LinkComponent,
  argTypes: { clicked: { action: 'clicked' } },
} as Meta;

const Template: Story<LinkComponent> = (args: LinkComponent) => ({
  props: args,
});

export const WithUrl = Template.bind({});
WithUrl.args = {
  href: 'https://www.google.com',
  target: '_blank',
  text: 'Google',
};

export const CaptureEvents = Template.bind({});
CaptureEvents.args = {
  text: 'OnClick',
};
