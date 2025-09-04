import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { SimScenariosWizardSpecificationsComponent } from "./sim-scenarios-wizard-specifications.component";

export default {
    title: 'QTD Components/designanddevelopment/simulators-scenarios/simulator-scenarios-wizard/sim-scenarios-specifications',
    component: SimScenariosWizardSpecificationsComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<SimScenariosWizardSpecificationsComponent> = (args: SimScenariosWizardSpecificationsComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
