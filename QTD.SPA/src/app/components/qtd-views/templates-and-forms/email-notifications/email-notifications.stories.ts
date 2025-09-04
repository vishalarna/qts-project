import { EmailNotificationsComponent } from './email-notifications.component';
import { moduleMetadata } from '@storybook/angular';
import { CommonModule } from '@angular/common';
// also exported from '@storybook/angular' if you can deal with breaking changes in 6.1
import { Story, Meta } from '@storybook/angular/types-6-0';

import {declarations, imports, storybookProviders } from '../../../../app.module.meta'

export default {
  title: 'QTD Components/EmailNotifications',
  component: EmailNotificationsComponent,
  decorators: [
    moduleMetadata({
      declarations: declarations,
      imports: imports,
      providers: storybookProviders(),
    }),
  ],
  argTypes: { changed: { action: 'changed' } },
} as Meta;

const Template: Story<EmailNotificationsComponent> = (args: EmailNotificationsComponent) => ({
  props: args,
});

export const Default = Template.bind({});
Default.args = {

};

