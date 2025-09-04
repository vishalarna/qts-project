import { componentWrapperDecorator, moduleMetadata } from '@storybook/angular';
import { CommonModule } from '@angular/common';
// also exported from '@storybook/angular' if you can deal with breaking changes in 6.1
import { Story, Meta } from '@storybook/angular/types-6-0';
import { MatSidenavModule } from '@angular/material/sidenav';

import { HttpClientModule } from '@angular/common/http';
import { RouterTestingModule } from '@angular/router/testing';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavBarLeftComponent } from './nav-bar-left.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MainNavBarComponent } from '../main-nav-bar/main-nav-bar.component';
import { JtaNavBarComponent } from '../jta-nav-bar/jta-nav-bar.component';

export default {
  title: 'QTD Components/Navigations/NavBarLeft',
  component: NavBarLeftComponent,
  decorators: [
    componentWrapperDecorator(NavBarLeftComponent),
    moduleMetadata({
      declarations: [
        MainNavBarComponent,
        JtaNavBarComponent,
        NavBarLeftComponent,
      ],
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

const MenuNavBarItems = [
  {
    title: 'Dashboard',
    icon: 'speed',
    path: '',
  },
  {
    title: 'Analysis',
    icon: 'rocket_launch',
    path: '',
    childMenu: [
      { title: 'Job and Task Analysis', path: '' },
      { title: 'Positions', path: '' },
      { title: 'Saftey Hazards', path: '' },
      { title: 'Enabling Objectives', path: '' },
      { title: 'Surveys', path: '' },
      { title: 'JTA Quality Control', path: '' },
    ],
  },
];

const Template: Story<NavBarLeftComponent> = (args: NavBarLeftComponent) => ({
  template: `<app-nav-bar-left>{{ngContent}} </app-nav-bar-left>`,
  props: { ...args },
});

const MainMenuComponent: Story<MainNavBarComponent> = (
  args: MainNavBarComponent
) => ({
  template: `<app-main-nav-bar [navList]="MenuNavBarItems"></app-main-nav-bar>`,
  props: { ...args },
});

const MainMenu = MainMenuComponent.bind({});
MainMenu.args = {};

export const Default = Template.bind({});
// Default.args = {
//    ngContent: MainMenu,
// };
