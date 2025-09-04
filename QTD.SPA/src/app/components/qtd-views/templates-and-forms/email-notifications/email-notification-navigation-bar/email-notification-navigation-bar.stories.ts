import {moduleMetadata} from '@storybook/angular';
import {CommonModule} from '@angular/common';
// also exported from '@storybook/angular' if you can deal with breaking changes in 6.1
import {Story, Meta} from '@storybook/angular/types-6-0';
import {EmailNotificationNavigationBarComponent} from './email-notification-navigation-bar.component'

import {declarations, imports, storybookProviders} from '../../../../../app.module.meta'
import * as data from '../../../../../../assets/qtd-docs/clientSettings_notifications.json';
import { pascalToCamel } from 'src/app/_Shared/Utils/PascalToCamel';

export default {
  title: 'QTD Components/EmailNotifications/NavigationBar',
  component: EmailNotificationNavigationBarComponent,
  decorators: [
    moduleMetadata({
      declarations: declarations,
      imports: imports,
      providers: storybookProviders(),
    }),
  ],
  argTypes: {changed: {action: 'changed'}},
} as Meta;

const Template: Story<EmailNotificationNavigationBarComponent> = (args: EmailNotificationNavigationBarComponent) => ({
  props: args,
});

export const Default = Template.bind({});
Default.args = {
  clientNotifications: pascalToCamel(data),
  selectedNotification: undefined
};

export const EMPLoginSelected = Template.bind({});
EMPLoginSelected.args = {
  clientNotifications: pascalToCamel(data),
  selectedNotification: pascalToCamel(data).filter(r => r.name == 'EMP Login')[0].id
};

