import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AppComponent } from "src/app/app.component";
import { declarations, imports, storybookProviders } from "src/app/app.module.meta";
import { PreviewAndPublishMetaILATestComponent } from "./preview-and-publish-meta-ila-test.component";

export default {
    title: 'QTD Components/ProviderAndILA/CreateMetaILAWizard/CreateMetaILATestWizard/PreviewAndPublishMetaILATest',
    component: PreviewAndPublishMetaILATestComponent,
    decorators: [
      moduleMetadata({
        declarations: [AppComponent],
        imports: imports,
        providers: storybookProviders(),
      }),
    ]
  } as Meta;

  const Template: Story<PreviewAndPublishMetaILATestComponent> = (args: PreviewAndPublishMetaILATestComponent) => ({
    props: args,
  });
  export const Default = Template.bind({});

  Default.args ={}
  