import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { SimScenariosWizardLinkagesComponent } from "./sim-scenarios-wizard-linkages.component";


export default {
    title: 'QTD Components/designanddevelopment/simulators-scenarios/simulator-scenarios-wizard/sim-scenarios-wizard-linkages',
    component: SimScenariosWizardLinkagesComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<SimScenariosWizardLinkagesComponent> = (args: SimScenariosWizardLinkagesComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
