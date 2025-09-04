import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelAddObjectivesLinkagesComponent } from "./fly-panel-add-objectives-linkages.component";


export default {
    title: 'QTD Components/designanddevelopment/simulators-scenarios/simulator-scenarios-wizard/sim-scenario-wizard-linkages/fly-panel-add-objectives-linkages',
    component: FlyPanelAddObjectivesLinkagesComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelAddObjectivesLinkagesComponent> = (args: FlyPanelAddObjectivesLinkagesComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
