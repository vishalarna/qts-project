import {moduleMetadata} from '@storybook/angular';
import {CommonModule} from '@angular/common';
// also exported from '@storybook/angular' if you can deal with breaking changes in 6.1
import {Story, Meta} from '@storybook/angular/types-6-0';

import {declarations, imports, storybookProviders} from '../../../../../app.module.meta'
import {NotificationsEnabledComponent} from './notifications-enabled.component'

import {action} from '@storybook/addon-actions'

export default {
  title: 'QTD Components/EmailNotifications/NotificationsEnabled',
  component: NotificationsEnabledComponent,
  decorators: [
    moduleMetadata({
      declarations: declarations,
      imports: imports,
      providers: storybookProviders(),
    }),
  ],
  argTypes: {onNotificationEnabledToggleChangeEvent: {action: 'onNotificationEnabledToggleChangeEvent'}},
} as Meta;

const Template: Story<NotificationsEnabledComponent> = (args: NotificationsEnabledComponent) => ({
  props: args,
});

export const WriteMode = Template.bind({});
WriteMode.args = {
  mode: 'write',
  enabled: false,
  timingText:'',
};

export const ReadMode = Template.bind({});
ReadMode.args = {
  mode: 'read',
  enabled: false,
  timingText:'',
};

export const WriteModeEnabled = Template.bind({});
WriteModeEnabled.args = {
  mode: 'write',
  enabled: true,
  timingText:'',
};

