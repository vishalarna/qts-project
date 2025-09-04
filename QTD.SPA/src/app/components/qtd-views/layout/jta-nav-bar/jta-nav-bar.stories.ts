import { moduleMetadata } from '@storybook/angular';
import { CommonModule } from '@angular/common';
// also exported from '@storybook/angular' if you can deal with breaking changes in 6.1
import { Story, Meta } from '@storybook/angular/types-6-0';
import { JtaNavBarComponent } from './jta-nav-bar.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { HttpClientModule } from '@angular/common/http';
import { RouterTestingModule } from '@angular/router/testing';
import { BaseModule } from 'src/app/components/base/base.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ButtonComponent } from 'src/app/components/base/button/button.component';
import { FormsModule } from '@angular/forms';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { SubdutyArea } from 'src/app/_DtoModels/SubdutyArea/SubdutyArea';

export default {
  title: 'QTD Components/Navigations/JtaNavBar',
  component: JtaNavBarComponent,
  decorators: [
    moduleMetadata({
      imports: [
        CommonModule,
        FormsModule,
        HttpClientModule,
        RouterTestingModule,
        LocalizeModule,
        BaseModule,
        MatSidenavModule,
        BrowserAnimationsModule,
      ],
    }),
  ],
  argTypes: { MenuAdded: { action: 'Menu Added' } },
} as Meta;

const Template: Story<JtaNavBarComponent> = (args: JtaNavBarComponent) => ({
  props: args,
});

const addDA = (a: any, self: any) => {
  return new Promise<void>((resolve) => {
    self.addNewDA = false;
    self._alert.successToast('DutyArea Added');
    self.MenuAdded.emit(a);
  });
};

const addSDA = (a: any, b: any, self: any) => {
  return new Promise<void>((resolve) => {
    self._alert.successToast('SubDutyArea Added');
    self.MenuAdded.emit(b);
  });
};

const addTask = (a: any, b: any, self: any) => {
  return new Promise<void>((resolve) => {
    self._alert.successToast('Task Added');
    self.MenuAdded.emit(b);
  });
};

export const Default = Template.bind({});
Default.args = {
  ngOnInit: () => {
    
  },
  getDutyAreas: () => {
    return Promise.resolve();
  },
  AddNewDutyArea: addDA,
  AddNewSubDutyArea: addSDA,
  AddNewTask: addTask,
};

export const JTAMenuWithDutyArea = Template.bind({});
JTAMenuWithDutyArea.args = {
  navList: [
    {
      Collapsed: false,
      Title: '1. § Transmission Operations',
      RoutePath: '',
      HasChildren: false,
    },
  ],
  getDutyAreas: () => {
    return Promise.resolve();
  },
  AddNewDutyArea: addDA,
  AddNewSubDutyArea: addSDA,
  AddNewTask: addTask,
};

export const JTAMenuWithDutyAndSubdutyArea = Template.bind({});
JTAMenuWithDutyAndSubdutyArea.args = {
  navList: [
    {
      Collapsed: false,
      Title: '1. § Transmission Operations',
      RoutePath: '',
      HasChildren: true,
      Children: [
        {
          Collapsed: false,
          Title: '1.1 test system Monitoring and Control',
          RoutePath: '',
          HasChildren: false,
        },
      ],
    },
  ],
  getDutyAreas: () => {
    return Promise.resolve();
  },
  AddNewDutyArea: addDA,
  AddNewSubDutyArea: addSDA,
  AddNewTask: addTask,
};

export const JTAMenuWithDutySubdutyAreaAndTask = Template.bind({});
JTAMenuWithDutySubdutyAreaAndTask.args = {
  navList: [
    {
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
              Title:
                '1.1 3 Identify the individual generating units base points',
              RoutePath: '/analysis/jta/task-detail/',
              RouteParams: { taskId: '3q' },
            },
          ],
        },
      ],
    },
  ],
  getDutyAreas: () => {
    return Promise.resolve();
  },
  AddNewDutyArea: addDA,
  AddNewSubDutyArea: addSDA,
  AddNewTask: addTask,
};
