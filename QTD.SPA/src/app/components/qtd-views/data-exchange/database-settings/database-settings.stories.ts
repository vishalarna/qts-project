import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { CustomizeDashboardComponent } from "./customize-dashboard/customize-dashboard.component";
import { DatabaseSettingsComponent } from "./database-settings.component";
import { LabelReplacementComponent } from "./label-replacement/label-replacement.component";

export default {
    title: 'QTD Components/DataExchange/Database Settings',
    component: DatabaseSettingsComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent, CustomizeDashboardComponent,LabelReplacementComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ],
  } as Meta;

  const Template: Story<DatabaseSettingsComponent> = (args: DatabaseSettingsComponent) => ({
    props: args,
  });

  export const Default = Template.bind({});
  