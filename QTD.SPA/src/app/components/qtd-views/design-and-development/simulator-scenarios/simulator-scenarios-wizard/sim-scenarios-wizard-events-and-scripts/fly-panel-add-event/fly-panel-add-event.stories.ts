import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelAddEventComponent } from "./fly-panel-add-event.component";

export default {
    title: 'QTD Components/designanddevelopment/simulators-scenarios/simulator-scenarios-wizard/sim-scenarios-wizard-events-and-scripts/fly-panel-add-event',
    component: FlyPanelAddEventComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelAddEventComponent> = (args: FlyPanelAddEventComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
