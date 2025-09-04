import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { SimScenariosWizardDetailsComponent } from "./sim-scenarios-wizard-details.component";

export default {
    title: 'QTD Components/designanddevelopment/simulators-scenarios/simulator-scenarios-wizard/simulator-scenarios-wizard-details',
    component: SimScenariosWizardDetailsComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<SimScenariosWizardDetailsComponent> = (args: SimScenariosWizardDetailsComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
