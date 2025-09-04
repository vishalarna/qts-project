import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelAddPrerequisitesIlaComponent } from "./fly-panel-add-prerequisites-ila.component";

export default {
    title: 'QTD Components/designanddevelopment/simulators-scenarios/simulator-scenarios-wizard/sim-scenarios-wizard-ila/fly-panel-add-prerequisities-ila',
    component: FlyPanelAddPrerequisitesIlaComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelAddPrerequisitesIlaComponent> = (args: FlyPanelAddPrerequisitesIlaComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
