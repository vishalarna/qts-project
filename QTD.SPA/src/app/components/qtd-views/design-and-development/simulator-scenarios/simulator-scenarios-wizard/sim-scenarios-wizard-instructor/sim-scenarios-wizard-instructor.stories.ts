import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import {  imports, storybookProviders } from "src/app/app.module.meta";
import { SimScenariosWizardInstructorComponent } from "./sim-scenarios-wizard-instructor.component";

export default {
    title: 'QTD Components/designanddevelopment/simulators-scenarios/simulator-scenarios-wizard/simulator-scenarios-wizard-instructor',
    component: SimScenariosWizardInstructorComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<SimScenariosWizardInstructorComponent> = (args: SimScenariosWizardInstructorComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});
