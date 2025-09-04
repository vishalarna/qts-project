import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelAddProceduresLinkagesComponent } from "./fly-panel-add-procedures-linkages.component";

export default {
    title: 'QTD Components/designanddevelopment/simulators-scenarios/simulator-scenarios-wizard/sim-scenario-wizard-linkages/fly-panel-add-procedures-linkages',
    component: FlyPanelAddProceduresLinkagesComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelAddProceduresLinkagesComponent> = (args: FlyPanelAddProceduresLinkagesComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
