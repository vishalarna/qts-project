import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { GeneralDatabaseSettingsComponent } from "./general-database-settings.component";
import * as generalSettingsData from '../../../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/clientSettings_GeneralSettings.json'
import { pascalToCamel } from "src/app/_Shared/Utils/PascalToCamel";
import { AppComponent } from "src/app/app.component";
import { CustomizeDashboardComponent } from "../customize-dashboard/customize-dashboard.component";

export default {
  title: 'QTD Components/DataExchange/GeneralDatabaseSettings',
  component: GeneralDatabaseSettingsComponent,
  decorators: [
    moduleMetadata({
      declarations: [AppComponent, CustomizeDashboardComponent],
      imports: imports,
      providers: storybookProviders(),
    }),
  ],
  argTypes: { OnSavedClickedEvent: { action: 'onSubmit' } },
} as Meta;

const Template: Story<GeneralDatabaseSettingsComponent> = (args: GeneralDatabaseSettingsComponent) => ({
  props: args,
});
export const Default = Template.bind({});

Default.args = {
  generalSettings: pascalToCamel(generalSettingsData),
}
