import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { EmpDefaultComponent } from "./emp-default.component";
import * as data from '../../../../../../../assets/qtd-docs/clientSettings_notifications.json';
import { pascalToCamel } from "src/app/_Shared/Utils/PascalToCamel";
export default {
    title: 'QTD Components/EmailNotifications/EmpDefault',
    component: EmpDefaultComponent,
    decorators: [
      moduleMetadata({
        declarations: declarations,
        imports: imports,
        providers: storybookProviders(),
      }),
    ],
    argTypes: { notificationSettingsSaveSuccessEvent: { action: 'notificationSettingsSaveSuccessEvent' } },
  } as Meta;

  const Template: Story<EmpDefaultComponent> = (args: EmpDefaultComponent) => ({
    props: args,
  });
const clientSettingSeedData = pascalToCamel(data);
  const empDefaultValues = clientSettingSeedData[2];
  export const EMPTestRead = Template.bind({});
  EMPTestRead.args ={
    mode: "read",
    checked: false,
    empDefaultData: empDefaultValues.steps[0].template,
    modelBinderItems:  empDefaultValues.steps[0].model,
    timingText: clientSettingSeedData[2].timingText,
  }

  export const EMPTestWrite = Template.bind({});
  EMPTestWrite.args ={
    mode: "write",
    checked: false,
    empDefaultData: empDefaultValues.steps[0].template,
    modelBinderItems:  empDefaultValues.steps[0].model,
    timingText: clientSettingSeedData[2].timingText,
  }
