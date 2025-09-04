import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { SimulatorScenariosComponent } from "./simulator-scenarios.component";

export default {
    title: 'QTD Components/designanddevelopment/simulators-scenarios',
    component: SimulatorScenariosComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<SimulatorScenariosComponent> = (args: SimulatorScenariosComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
