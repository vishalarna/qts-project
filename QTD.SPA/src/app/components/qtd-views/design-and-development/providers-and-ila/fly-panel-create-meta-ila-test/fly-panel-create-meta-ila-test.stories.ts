import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { FlyPanelCreateMetaILATestComponent } from "./fly-panel-create-meta-ila-test.component";
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatStepperModule } from "@angular/material/stepper";
import { AddTestInformationModule } from "./fly-panel-create-meta-ila-test-components/add-test-information/add-test-information.module";
import { AddAndSequenceTestQuestionsModule } from "./fly-panel-create-meta-ila-test-components/add-and-sequence-test-questions/add-and-sequence-test-questions.module";



export default {
    title: 'QTD Components/ProviderAndILA/CreateMetaILAWizard/CreateMetaILATestWizard',
    component: FlyPanelCreateMetaILATestComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: [...imports,MatToolbarModule,MatStepperModule,AddTestInformationModule,AddAndSequenceTestQuestionsModule],
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<FlyPanelCreateMetaILATestComponent> = (args: FlyPanelCreateMetaILATestComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});

  Default.args ={}
  