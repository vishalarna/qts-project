import { moduleMetadata } from '@storybook/angular';
import { CommonModule } from '@angular/common';
// also exported from '@storybook/angular' if you can deal with breaking changes in 6.1
import { Story, Meta } from '@storybook/angular/types-6-0';
import { HttpClientModule } from '@angular/common/http';
import { RouterTestingModule } from '@angular/router/testing';
import { BaseModule } from 'src/app/components/base/base.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MainNavBarComponent } from './main-nav-bar.component';

export default {
  title: 'QTD Components/Navigations/MainNavBar',
  component: MainNavBarComponent,
  decorators: [
    moduleMetadata({
      imports: [
        CommonModule,
        HttpClientModule,
        RouterTestingModule,
        LocalizeModule,
        BaseModule,
        BrowserAnimationsModule,
      ],
    }),
  ],
} as Meta;

const Template: Story<MainNavBarComponent> = (args: MainNavBarComponent) => ({
  props: args,
});

export const Default = Template.bind({});
Default.args = {
  navList: [
    {
      Title: 'Dashboard',
      IconName: 'speed',
      RoutePath: '/home/index',
    },
    {
      Title: 'Analysis',
      IconName: 'rocket_launch',
      RoutePath: '',
      Children: [
        {
          Title: 'Job and Task Analysis',
          RoutePath: '/analysis/jta/task-detail',
        },
        { Title: 'Positions', RoutePath: '' },
        { Title: 'Saftey Hazards', RoutePath: '' },
        { Title: 'Enabling Objectives', RoutePath: '' },
        { Title: 'Surveys', RoutePath: '' },
        { Title: 'JTA Quality Control', RoutePath: '' },
      ],
    },
    {
      Title: 'Design & Development',
      IconName: 'speed',

      RoutePath: '',
    },
    {
      Title: 'Implementation',
      IconName: 'rocket_launch',

      RoutePath: '',
      Children: [
        { Title: 'Employees', RoutePath: '/implementation/employees' },
        { Title: 'Task Qualification', RoutePath: '' },
        { Title: 'Initial Training Program', RoutePath: '' },
        { Title: 'Training Resources', RoutePath: '' },
        { Title: 'Instructions and Locations', RoutePath: '' },
        { Title: 'Schedules and Grades', RoutePath: '' },
        { Title: 'Self Registration', RoutePath: '' },
        { Title: 'Data Exchange', RoutePath: '' },
        { Title: 'ILA Delivery', RoutePath: '' },
      ],
    },
    {
      Title: 'Evaluation',
      IconName: 'insert_chart',

      RoutePath: '',
    },
  ],
  createList: () => {},
};
