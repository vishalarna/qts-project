import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { SimulatorScenariosOverviewComponent } from "./simulator-scenarios-overview.component";

export default {
    title: 'QTD Components/designanddevelopment/simulators-scenarios/simulator-scenarios-overview',
    component: SimulatorScenariosOverviewComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<SimulatorScenariosOverviewComponent> = (args: SimulatorScenariosOverviewComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
