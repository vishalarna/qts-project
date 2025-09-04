import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { CustomizeDashboardComponent } from "./customize-dashboard.component";
import * as dasboardsettingdata from '../../../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/dashboard_settings.json';
import *  as clientusersettingdata from '../../../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/clientUserSettings_dashboard.json';
import { pascalToCamel } from "src/app/_Shared/Utils/PascalToCamel";

export default {
  title: 'QTD Components/DataExchange/CustomizeDashboardComponent',
  component: CustomizeDashboardComponent,
  decorators: [
    moduleMetadata({
      declarations: declarations,
      imports: imports,
      providers: storybookProviders(),
    }),
  ],
  argTypes: { OnSaveClickedEvent: { action: 'SaveButtonClick' } },
} as Meta;

const Template: Story<CustomizeDashboardComponent> = (args: CustomizeDashboardComponent) => ({
  props: args,
});
export const Default = Template.bind({});
let clientdata = pascalToCamel(clientusersettingdata);
let dasboarddata = pascalToCamel(dasboardsettingdata);
clientdata.forEach(element => {
  element.settings = dasboarddata.filter(r => r.id === element.dashboardSettingId)[0];
})
Default.args = {
  ClientUserSettings_Dashboard: clientdata,
 
}
