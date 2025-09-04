import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { AddTestInformationComponent } from "./add-test-information.component";
import { MatRadioModule } from "@angular/material/radio";



export default {
    title: 'QTD Components/ProviderAndILA/CreateMetaILAWizard/CreateMetaILATestWizard/AddTestInformation',
    component: AddTestInformationComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: [...imports,MatRadioModule],
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<AddTestInformationComponent> = (args: AddTestInformationComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});

  Default.args ={}
  