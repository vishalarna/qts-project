import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelAddIlaComponent } from "./fly-panel-add-ila-ila.component";


export default {
    title: 'QTD Components/designanddevelopment/simulators-scenarios/simulator-scenarios-wizard/sim-scenarios-wizard-ila/fly-panel-add-ila-ila',
    component: FlyPanelAddIlaComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelAddIlaComponent> = (args: FlyPanelAddIlaComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
