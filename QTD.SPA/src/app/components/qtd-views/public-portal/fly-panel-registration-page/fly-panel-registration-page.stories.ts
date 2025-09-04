import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelRegistrationPageComponent } from "./fly-panel-registration-page.component";

export default {
    title: 'QTD Components/public-portal/fly-panel-registration-page',
    component: FlyPanelRegistrationPageComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelRegistrationPageComponent> = (args: FlyPanelRegistrationPageComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});