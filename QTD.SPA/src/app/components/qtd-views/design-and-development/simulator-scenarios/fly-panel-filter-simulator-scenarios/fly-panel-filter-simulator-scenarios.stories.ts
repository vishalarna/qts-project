import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelFilterSimulatorScenariosComponent } from "./fly-panel-filter-simulator-scenarios.component";

export default {
    title: 'QTD Components/designanddevelopment/simulators-scenarios/fly-panel-filter-simulator-scenarios',
    component: FlyPanelFilterSimulatorScenariosComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelFilterSimulatorScenariosComponent> = (args: FlyPanelFilterSimulatorScenariosComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
