import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { ClassScheduleComponent } from "./class-schedule.component";

import {declarations, imports, storybookProviders } from '../../../../../../app.module.meta';
import * as data from '../../../../../../../assets/qtd-docs/clientSettings_notifications.json';
import { employeesTestData } from 'src/app/_Services/QTD/Employees/testData';
import { pascalToCamel } from 'src/app/_Shared/Utils/PascalToCamel';
export default {
    title: 'QTD Components/EmailNotifications/ClassSchedule',
    component: ClassScheduleComponent,
    decorators: [
      moduleMetadata({
        declarations: declarations,
        imports: imports,
        providers: storybookProviders(),
      }),
    ],
  } as Meta;
  const Template: Story<ClassScheduleComponent> = (args: ClassScheduleComponent) => ({
    props: args,
  });
  const clientSettingSeedData = pascalToCamel(data);
  const ClassScheduleValues = clientSettingSeedData[0].steps;
  export const Read = Template.bind({});
  Read.args = {
  };

  export const Write = Template.bind({});
  Write.args = {
  };
