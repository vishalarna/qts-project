import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { SimulatorScenariosWizardComponent } from "./simulator-scenarios-wizard.component";

export default {
    title: 'QTD Components/designanddevelopment/simulators-scenarios/simulator-scenarios-wizard',
    component: SimulatorScenariosWizardComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<SimulatorScenariosWizardComponent> = (args: SimulatorScenariosWizardComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
