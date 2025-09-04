import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { SimScenariosWizardIlaComponent } from "./sim-scenarios-wizard-ila.component";

export default {
    title: 'QTD Components/designanddevelopment/simulators-scenarios/simulator-scenarios-wizard/sim-scenarios-wizard-ila',
    component: SimScenariosWizardIlaComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<SimScenariosWizardIlaComponent> = (args: SimScenariosWizardIlaComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
