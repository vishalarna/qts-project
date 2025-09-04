import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { SimScenariosWizardCriteriaComponent } from "./sim-scenarios-wizard-criteria.component";

export default {
    title: 'QTD Components/designanddevelopment/simulators-scenarios/simulator-scenarios-wizard/simulator-scenarios-wizard-criteria',
    component: SimScenariosWizardCriteriaComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<SimScenariosWizardCriteriaComponent> = (args: SimScenariosWizardCriteriaComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
