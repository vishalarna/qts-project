import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { SimScenariosWizardEventsAndScriptsComponent } from "./sim-scenarios-wizard-events-and-scripts.component";

export default {
    title: 'QTD Components/designanddevelopment/simulators-scenarios/simulator-scenarios-wizard/sim-scenarios-wizard-events-and-scripts',
    component: SimScenariosWizardEventsAndScriptsComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<SimScenariosWizardEventsAndScriptsComponent> = (args: SimScenariosWizardEventsAndScriptsComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
