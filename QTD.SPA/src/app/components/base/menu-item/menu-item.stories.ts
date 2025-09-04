import { moduleMetadata } from '@storybook/angular';
import { CommonModule } from '@angular/common';
// also exported from '@storybook/angular' if you can deal with breaking changes in 6.1
import { Story, Meta } from '@storybook/angular/types-6-0';

import { MenuItemComponent } from './menu-item.component';
import { LinkComponent } from '../link/link.component';
import { IconComponent } from '../icon/icon.component';
import { MatIconModule } from '@angular/material/icon';
import { RouterTestingModule } from '@angular/router/testing';
import { LabelComponent } from '../label/label.component';
import { AddNewMenuItemComponent } from '../add-new-menu-item/add-new-menu-item.component';
import { ButtonComponent } from '../button/button.component';
import { TextboxComponent } from '../textbox/textbox.component';
import { FormsModule } from '@angular/forms';

export default {
  title: 'Base Components/MenuItem',
  component: MenuItemComponent,
  decorators: [
    moduleMetadata({
      declarations: [
        LinkComponent,
        LabelComponent,
        IconComponent,
        ButtonComponent,
        TextboxComponent,
        AddNewMenuItemComponent,
      ],
      imports: [CommonModule,FormsModule, MatIconModule, RouterTestingModule],
    }),
  ],
  argTypes: { ItemSaved: { action: 'Item Saved' } },
} as Meta;

const Template: Story<MenuItemComponent> = (args: MenuItemComponent) => ({
  props: args,
});

export const MenuWithNoChildren = Template.bind({});
MenuWithNoChildren.args = {
  Title: 'Dashboard',
  IconName: 'speed',
  RoutePath: '',
  HasChildren: false,
};

export const MenuWithChildren = Template.bind({});
MenuWithChildren.args = {
  Title: 'Analysis',
  IconName: 'rocket_launch',
  RoutePath: '',
  HasChildren: true,
  Children: [
    {
      Title: 'Job and Task Analysis',
      RoutePath: '',
    },
    { Title: 'Positions', RoutePath: '' },
    { Title: 'Saftey Hazards', RoutePath: '' },
    { Title: 'Enabling Objectives', RoutePath: '' },
    { Title: 'Surveys', RoutePath: '' },
    { Title: 'JTA Quality Control', RoutePath: '' },
  ],
};

export const MenuWith2LevelChildren = Template.bind({});
MenuWith2LevelChildren.args = {
  Mode: 'write',
  Collapsed: false,
  Title: '1. § Transmission Operations',
  RoutePath: '',
  HasChildren: true,
  Children: [
    {
      Collapsed: false,
      Title: '1.1 test system Monitoring and Control',
      RoutePath: '',
      HasChildren: true,
      Children: [
        {
          Title: '1.1 1 Monitor the SAL for substation entry notations',
          RoutePath: '/analysis/jta/task-detail/',
          RouteParams: { taskId: 'xY' },
        },
        {
          Title:
            '1.1 2 Identify the equipment that has been affected by the forced outage',
          RoutePath: '/analysis/jta/task-detail/',
          RouteParams: { taskId: 'EP' },
        },
        {
          Title: '1.1 3 Identify the individual generating units base points',
          RoutePath: '/analysis/jta/task-detail/',
          RouteParams: { taskId: '3q' },
        },
      ],
    },
  ],
};
